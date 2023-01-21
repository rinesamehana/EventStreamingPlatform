using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventStreamingPlatform.Data;
using EventStreamingPlatform.Models;
using EventStreamingPlatform.Models.StreamingViewModel;

namespace EventStreamingPlatform.Controllers
{
    public class FilmsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index(int? id, int? genreId)
        {
            var viewModel = new FilmIndexData();
            viewModel.Films = await _context.Films
                  
                  .Include(i => i.FilmGenres)
                    .ThenInclude(i => i.Genre)
                        .ThenInclude(i => i.Recomandation)
                  .OrderBy(i => i.Title)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["FilmId"] = id.Value;
                Film film = viewModel.Films.Single(
                    i => i.ID == id.Value);
                viewModel.Genres = film.FilmGenres.Select(s => s.Genre);
                ViewData["Film"] = film.Title;
            }

            if (genreId != null)
            {
                ViewData["GenreId"] = genreId.Value;
                var selectedGenre = viewModel.Genres.Where(x => x.GenreId == genreId).Single();
                ViewData["Genre"] = selectedGenre.Name;
                
            }

            return View(viewModel);
        }

        // GET: FilmS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .FirstOrDefaultAsync(m => m.ID == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            var film = new Film();
            film.FilmGenres = new List<FilmGenre>();
            PopulateAssignedGenreData(film);
            return View();
        }

        // POST: Films/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title")] Film film, string[] selectedGenres)
        {
            if (selectedGenres != null)
            {
                film.FilmGenres = new List<FilmGenre>();
                foreach (var genre in selectedGenres)
                {
                    var genreToAdd = new FilmGenre { FilmId = film.ID, GenreId = int.Parse(genre) };
                    film.FilmGenres.Add(genreToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedGenreData(film);
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                
                .Include(i=>i.FilmGenres).ThenInclude(i=>i.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (film == null)
            {
                return NotFound();
            }
            PopulateAssignedGenreData(film);
            return View(film);
        }

        private void PopulateAssignedGenreData(Film film)
        {
            var allGenres = _context.Genres;
            var filmGenres = new HashSet<int>(film.FilmGenres.Select(c=>c.GenreId));
            var viewModel = new List<AssignedGenreData>();
            foreach(var genre in allGenres)
            {
                viewModel.Add(new AssignedGenreData
                {
                    GenreId = genre.GenreId,
                    Name = genre.Name,
                    Assigned = filmGenres.Contains(genre.GenreId)
                });
            }
            ViewData["Genres"] = viewModel;
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedGenres)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmToUpdate = await _context.Films
                
                .Include(i => i.FilmGenres)
                    .ThenInclude(i => i.Genre)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Film>(
                filmToUpdate,
                "",
                i => i.Title))
            {

                UpdateFilmGenre(selectedGenres, filmToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", @"Unable to save changes. 
                                                   Try again, and if the problem persists, 
                                                   see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateFilmGenre(selectedGenres, filmToUpdate);
            PopulateAssignedGenreData(filmToUpdate);
            return View(filmToUpdate);
        }

        private void UpdateFilmGenre(string[] selectedGenres, Film filmToUpdate)
        {
            if (selectedGenres == null)
            {
                filmToUpdate.FilmGenres = new List<FilmGenre>();
                return;
            }

            var selectedGenresHS = new HashSet<string>(selectedGenres);
            var filmGenres = new HashSet<int>(filmToUpdate.FilmGenres.Select(c => c.Genre.GenreId));

            foreach (var genre in _context.Genres)
            {
                if (selectedGenresHS.Contains(genre.GenreId.ToString()))
                {
                    if (!filmGenres.Contains(genre.GenreId))
                    {
                        filmToUpdate.FilmGenres.Add(new FilmGenre { FilmId = filmToUpdate.ID, GenreId = genre.GenreId });
                    }
                }
                else
                {
                    if (filmGenres.Contains(genre.GenreId))
                    {
                        FilmGenre genreToRemove = filmToUpdate.FilmGenres.FirstOrDefault(i => i.GenreId == genre.GenreId);
                        _context.Remove(genreToRemove);
                    }
                }
            }
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .FirstOrDefaultAsync(m => m.ID == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Film film = await _context.Films
                .Include(i => i.FilmGenres)
                .SingleAsync(i => i.ID == id);

            var recomandations = await _context.Recomandations
                .Where(d => d.FilmId == id)
                .ToListAsync();
            recomandations.ForEach(d => d.FilmId = null);

            _context.Films.Remove(film);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
