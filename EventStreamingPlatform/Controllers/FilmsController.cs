using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventStreamingPlatform.Data;
using EventStreamingPlatform.Models;
using EventStreamingPlatform.Models.StreamingViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace EventStreamingPlatform.Controllers
{

    public class FilmsController : Controller
    {


        private readonly ApplicationDbContext _context;

        public FilmsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("api/{controller}")]
        [HttpGet]


        public IActionResult GetAll()
        {

            var actors = _context.Films
                 .Include(c => c.Language)
                .Include(c => c.Company)
                 
                  .Include(i => i.FilmGenres)
                    .ThenInclude(i => i.Genre)
                        .ThenInclude(i => i.Recomandation)
                    .Include(c => c.FilmActors)
                        .ThenInclude(i => i.Actor)
                     .Include(c => c.FilmMainActors)
                        .ThenInclude(i => i.Actor).ToList();

            return Json(new { data = actors });
        }
        // GET: Films

        [HttpGet]
        public async Task<IActionResult> Index(
            int? id,
            int? genreId,
            int? actorId,
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            //var viewModel = new FilmIndexData();
            //viewModel.Films = await _context.Films
            //      .Include(c => c.Company)
            //      .Include(c => c.Language)
            //      .Include(i => i.FilmGenres)
            //        .ThenInclude(i => i.Genre)
            //            .ThenInclude(i => i.Recomandation)
            //        .Include(c => c.FilmActors)
            //            .ThenInclude(i => i.Actor)
            //         .Include(c => c.FilmMainActors)
            //            .ThenInclude(i => i.Actor)
            //      .OrderBy(i => i.Title)
            //      .ToListAsync();


            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = sortOrder == "Title" ? "Title_desc" : "Title";
            ViewData["CompanySortParm"] = sortOrder == "CompanyName" ? "CompanyName_desc" : "CompanyName";
            ViewData["DurationSortParm"] = sortOrder == "Duration" ? "Duration_desc" : "Duration";
            ViewData["GenresSortParm"] = sortOrder == "Genre" ? "Genre_desc" : "Genre";
            ViewData["RatingSortParm"] = sortOrder == "Rating" ? "Rating_desc" : "Rating";
            ViewData["RealiseDateSortParm"] = sortOrder == "RealiseDate" ? "RealiseDate_desc" : "RealiseDate";
            ViewData["DirectorSortParm"] = sortOrder == "Director" ? "Director_desc" : "Director";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var films = new List<Film>();

            bool officeAssignment = false;
            bool courseAssignment = false;


            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Title";
            }

            else if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Duration";
            }
            else if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Rating";
            }
            else if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "RealiseDate";
            }
            else if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Director";
            }
            else if (sortOrder.Equals("CompanyName_desc") || sortOrder.Equals("CompanyName"))
            {
                officeAssignment = true;
            }
            else if (sortOrder.Equals("Genre_desc") || sortOrder.Equals("Genre"))
            {
                courseAssignment = true;
            }

            Debug.WriteLine(sortOrder);

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                if (descending)
                {
                    if (officeAssignment)
                    {
                        films = await _context.Films
                        .Include(c => c.Company)
                        .Include(c => c.Language)
                        .Include(i => i.FilmGenres)
                        .ThenInclude(i => i.Genre)
                        .ThenInclude(i => i.Recomandation)
                         .Include(c => c.FilmActors)
                        .ThenInclude(i => i.Actor)
                        .Include(c => c.FilmMainActors)
                        .ThenInclude(i => i.Actor)
                        .OrderBy(i => i.Title)
                        .Where(s => s.Title.Contains(searchString))
                   
                        .OrderByDescending(e => EF.Property<object>(e.Company, sortOrder))
                        .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        films = await _context.Films
                       .Include(c => c.Company)
                       .Include(c => c.Language)
                       .Include(i => i.FilmGenres)
                       .ThenInclude(i => i.Genre)
                       .ThenInclude(i => i.Recomandation)
                        .Include(c => c.FilmActors)
                       .ThenInclude(i => i.Actor)
                       .Include(c => c.FilmMainActors)
                       .ThenInclude(i => i.Actor)
                       .OrderBy(i => i.Title)
                       .Where(s => s.Title.Contains(searchString))
              
                       .ToListAsync();

                        films.OrderByDescending(e => EF.Property<object>(e.FilmGenres.AsQueryable().Include(i => i.Genre), sortOrder));
                    }
                    else
                    {
                        films = await _context.Films
                      .Include(c => c.Company)
                      .Include(c => c.Language)
                      .Include(i => i.FilmGenres)
                      .ThenInclude(i => i.Genre)
                      .ThenInclude(i => i.Recomandation)
                       .Include(c => c.FilmActors)
                      .ThenInclude(i => i.Actor)
                      .Include(c => c.FilmMainActors)
                      .ThenInclude(i => i.Actor)
                      .OrderBy(i => i.Title)
                       .Where(s => s.Title.Contains(searchString))
                      
                       .OrderByDescending(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
                else
                {
                    if (officeAssignment)
                    {

                        films = await _context.Films
                       .Include(c => c.Company)
                       .Include(c => c.Language)
                       .Include(i => i.FilmGenres)
                       .ThenInclude(i => i.Genre)
                       .ThenInclude(i => i.Recomandation)
                        .Include(c => c.FilmActors)
                       .ThenInclude(i => i.Actor)
                       .Include(c => c.FilmMainActors)
                       .ThenInclude(i => i.Actor)
                       .OrderBy(i => i.Title)
                        .Where(s => s.Title.Contains(searchString))
                        
                        .OrderBy(e => EF.Property<object>(e.Title, sortOrder))
                         .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        films = await _context.Films
                    .Include(c => c.Company)
                    .Include(c => c.Language)
                    .Include(i => i.FilmGenres)
                    .ThenInclude(i => i.Genre)
                    .ThenInclude(i => i.Recomandation)
                     .Include(c => c.FilmActors)
                    .ThenInclude(i => i.Actor)
                    .Include(c => c.FilmMainActors)
                    .ThenInclude(i => i.Actor)
                    .OrderBy(i => i.Title)
                     .Where(s => s.Title.Contains(searchString))
                     

                      .ToListAsync();

                        films.OrderBy(e => EF.Property<object>(e.FilmGenres.AsQueryable().Include(i => i.Genre), sortOrder));
                    }
                    else
                    {
                        films = await _context.Films
                    .Include(c => c.Company)
                    .Include(c => c.Language)
                    .Include(i => i.FilmGenres)
                    .ThenInclude(i => i.Genre)
                    .ThenInclude(i => i.Recomandation)
                     .Include(c => c.FilmActors)
                    .ThenInclude(i => i.Actor)
                    .Include(c => c.FilmMainActors)
                    .ThenInclude(i => i.Actor)
                    .OrderBy(i => i.Title)
                     .Where(s => s.Title.Contains(searchString))
                       

                        .OrderBy(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
            }
            else
            {
                if (descending)
                {
                    if (officeAssignment)
                    {
                        films = await _context.Films
                    .Include(c => c.Company)
                    .Include(c => c.Language)
                    .Include(i => i.FilmGenres)
                    .ThenInclude(i => i.Genre)
                    .ThenInclude(i => i.Recomandation)
                     .Include(c => c.FilmActors)
                    .ThenInclude(i => i.Actor)
                    .Include(c => c.FilmMainActors)
                    .ThenInclude(i => i.Actor)
                        .OrderByDescending(e => EF.Property<object>(e.Company, sortOrder))
                        .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        films = await _context.Films
                    .Include(c => c.Company)
                    .Include(c => c.Language)
                    .Include(i => i.FilmGenres)
                    .ThenInclude(i => i.Genre)
                    .ThenInclude(i => i.Recomandation)
                     .Include(c => c.FilmActors)
                    .ThenInclude(i => i.Actor)
                    .Include(c => c.FilmMainActors)
                    .ThenInclude(i => i.Actor)
                            .ToListAsync();

                        films.OrderByDescending(e => EF.Property<object>(e.FilmGenres.AsQueryable().Include(i => i.Genre), sortOrder));
                    }
                    else
                    {
                        films = await _context.Films
                   .Include(c => c.Company)
                   .Include(c => c.Language)
                   .Include(i => i.FilmGenres)
                   .ThenInclude(i => i.Genre)
                   .ThenInclude(i => i.Recomandation)
                    .Include(c => c.FilmActors)
                   .ThenInclude(i => i.Actor)
                   .Include(c => c.FilmMainActors)
                   .ThenInclude(i => i.Actor)
                       .OrderByDescending(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
                else
                {
                    if (officeAssignment)
                    {
                        films = await _context.Films
                   .Include(c => c.Company)
                   .Include(c => c.Language)
                   .Include(i => i.FilmGenres)
                   .ThenInclude(i => i.Genre)
                   .ThenInclude(i => i.Recomandation)
                    .Include(c => c.FilmActors)
                   .ThenInclude(i => i.Actor)
                   .Include(c => c.FilmMainActors)
                   .ThenInclude(i => i.Actor)
                       .OrderBy(e => EF.Property<object>(e.Company, sortOrder))
                        .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        films = await _context.Films
                    .Include(c => c.Company)
                    .Include(c => c.Language)
                    .Include(i => i.FilmGenres)
                    .ThenInclude(i => i.Genre)
                    .ThenInclude(i => i.Recomandation)
                     .Include(c => c.FilmActors)
                    .ThenInclude(i => i.Actor)
                    .Include(c => c.FilmMainActors)
                    .ThenInclude(i => i.Actor)
                        .ToListAsync();

                        films.OrderBy(e => EF.Property<object>(e.FilmGenres.AsQueryable().Include(i => i.Genre), sortOrder));
                    }
                    else
                    {
                        films = await _context.Films
                     .Include(c => c.Company)
                     .Include(c => c.Language)
                     .Include(i => i.FilmGenres)
                     .ThenInclude(i => i.Genre)
                     .ThenInclude(i => i.Recomandation)
                      .Include(c => c.FilmActors)
                     .ThenInclude(i => i.Actor)
                     .Include(c => c.FilmMainActors)
                     .ThenInclude(i => i.Actor)
                         .OrderBy(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
            }

            int pageSize = 3;
            var viewModel = PagedFilmIndexData<Film>.Create(films, pageNumber ?? 1, pageSize, id);








            if (id != null)
            {
                ViewData["FilmId"] = id.Value;
                Film film = viewModel.Films.Single(
                    i => i.ID == id.Value);
                viewModel.Genres = film.FilmGenres.Select(s => s.Genre);
                //ViewData["Film"] = film.Title;
            }

            if (genreId != null)
            {
                ViewData["GenreId"] = genreId.Value;
                var selectedGenre = viewModel.Genres.Where(x => x.GenreId == genreId).Single();
                ViewData["Genre"] = selectedGenre.Name;

            }
            if (actorId != null)
            {
                ViewData["ActorId"] = actorId.Value;
                var selectedActor = viewModel.Actors.Where(x => x.ActorId == actorId).Single();
                ViewData["Actor"] = selectedActor.Name;

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
                .Include(c => c.Company)
                 .Include(c => c.Language)
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
            film.FilmActors = new List<FilmActor>();
            film.FilmMainActors = new List<FilmMainActor>();
            PopulateCompnayDropDownList();
            PopulateLanguageDropDownList();
            PopulateAssignedGenreData(film);
            PopulateAssignedActorData(film);
            PopulateAssignedMainActorData(film);
            return View();
        }

        // POST: Films/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, Description,Duration,Rating , VideoLink, PhotoLink, RealiseDate ,Director, CompanyId, LanguageId")] Film film, string[] selectedGenres, string[] selectedActors, string[] selectedMainActors)
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
            if (selectedActors != null)
            {
                film.FilmActors = new List<FilmActor>();
                foreach (var actor in selectedActors)
                {
                    var actorToAdd = new FilmActor { FilmId = film.ID, ActorId = int.Parse(actor) };
                    film.FilmActors.Add(actorToAdd);
                }
            }
            if (selectedMainActors != null)
            {
                film.FilmMainActors = new List<FilmMainActor>();
                foreach (var actor in selectedMainActors)
                {
                    var actorToAdd = new FilmMainActor { FilmId = film.ID, ActorId = int.Parse(actor) };
                    film.FilmMainActors.Add(actorToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateLanguageDropDownList(film.LanguageId);
            PopulateCompnayDropDownList(film.CompanyId);
            PopulateAssignedGenreData(film);
            PopulateAssignedActorData(film);
            PopulateAssignedMainActorData(film);
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
                .Include(i => i.FilmActors).ThenInclude(i => i.Actor)
                .Include(i => i.FilmGenres).ThenInclude(i => i.Genre)
                .Include(i => i.FilmMainActors).ThenInclude(i => i.Actor)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (film == null)
            {
                return NotFound();
            }
            PopulateLanguageDropDownList(film.LanguageId);
            PopulateCompnayDropDownList(film.CompanyId);
            PopulateAssignedGenreData(film);
            PopulateAssignedActorData(film);
            PopulateAssignedMainActorData(film);
            return View(film);
        }

        private void PopulateAssignedGenreData(Film film)
        {
            var allGenres = _context.Genres;
            var filmGenres = new HashSet<int>(film.FilmGenres.Select(c => c.GenreId));
            var viewModel = new List<AssignedGenreData>();
            foreach (var genre in allGenres)
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
        private void PopulateAssignedActorData(Film film)
        {
            var allActors = _context.Actors;
            var filmActors = new HashSet<int>(film.FilmActors.Select(c => c.ActorId));
            var viewModel = new List<AssignedActorData>();
            foreach (var actor in allActors)
            {
                viewModel.Add(new AssignedActorData
                {
                    ActorId = actor.ActorId,
                    Name = actor.Name,
                    Assigned = filmActors.Contains(actor.ActorId)
                });
            }
            ViewData["Actors"] = viewModel;
        }
        private void PopulateAssignedMainActorData(Film film)
        {
            var allActors = _context.Actors;
            var filmActors = new HashSet<int>(film.FilmMainActors.Select(c => c.ActorId));
            var viewModel = new List<AssignedMainActorData>();
            foreach (var actor in allActors)
            {
                viewModel.Add(new AssignedMainActorData
                {
                    ActorId = actor.ActorId,
                    Name = actor.Name,
                    Assigned = filmActors.Contains(actor.ActorId)
                });
            }
            ViewData["Actorss"] = viewModel;
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedGenres, string[] selectedActors, string[] selectedMainActors)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmToUpdate = await _context.Films
                .Include(i => i.FilmActors)
                .ThenInclude(i => i.Actor)
                .Include(i => i.FilmMainActors)
                .ThenInclude(i => i.Actor)
                .Include(i => i.FilmGenres)
                    .ThenInclude(i => i.Genre)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Film>(
                filmToUpdate,
                "",
                i => i.Title, c => c.Description, ch => ch.Duration, ch => ch.Rating, ch => ch.VideoLink, ch => ch.PhotoLink, ch => ch.RealiseDate, c => c.Director, c => c.CompanyId, c => c.LanguageId))
            {

                UpdateFilmGenre(selectedGenres, selectedActors, selectedMainActors, filmToUpdate);
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
            PopulateLanguageDropDownList(filmToUpdate.LanguageId);
            PopulateCompnayDropDownList(filmToUpdate.CompanyId);
            UpdateFilmGenre(selectedGenres, selectedActors, selectedMainActors, filmToUpdate);
            PopulateAssignedGenreData(filmToUpdate);
            return View(filmToUpdate);
        }

        private void PopulateCompnayDropDownList(object selectedCompany = null)
        {
            var companyQuery = from d in _context.Company
                               orderby d.CompanyName
                               select d;
            ViewBag.CompanyId = new SelectList(companyQuery.AsNoTracking(), "CompanyId", "CompanyName", selectedCompany);
        }

        private void PopulateLanguageDropDownList(object selectedLanguage = null)
        {
            var languageQuery = from d in _context.Languages
                                orderby d.LanguageName
                                select d;
            ViewBag.LanguageId = new SelectList(languageQuery.AsNoTracking(), "LanguageId", "LanguageName", selectedLanguage);
        }

        private void UpdateFilmGenre(string[] selectedGenres, string[] selectedActors, string[] selectedMainActors, Film filmToUpdate)
        {
            if (selectedGenres == null || selectedActors == null || selectedMainActors == null)
            {
                filmToUpdate.FilmGenres = new List<FilmGenre>();
                filmToUpdate.FilmActors = new List<FilmActor>();
                filmToUpdate.FilmMainActors = new List<FilmMainActor>();
                return;
            }

            var selectedGenresHS = new HashSet<string>(selectedGenres);
            var selectedActorsHS = new HashSet<string>(selectedActors);
            var selectedMainActorsHS = new HashSet<string>(selectedMainActors);
            var filmGenres = new HashSet<int>(filmToUpdate.FilmGenres.Select(c => c.Genre.GenreId));
            var filmActors = new HashSet<int>(filmToUpdate.FilmActors.Select(c => c.Actor.ActorId));
            var filmMainActors = new HashSet<int>(filmToUpdate.FilmMainActors.Select(c => c.Actor.ActorId));
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
            foreach (var actor in _context.Actors)
            {
                if (selectedActorsHS.Contains(actor.ActorId.ToString()))
                {
                    if (!filmActors.Contains(actor.ActorId))
                    {
                        filmToUpdate.FilmActors.Add(new FilmActor { FilmId = filmToUpdate.ID, ActorId = actor.ActorId });

                    }
                }
                else
                {
                    if (filmActors.Contains(actor.ActorId))
                    {
                        FilmActor actorToRemove = filmToUpdate.FilmActors.FirstOrDefault(i => i.ActorId == actor.ActorId);

                        _context.Remove(actorToRemove);
                    }
                }
            }
            foreach (var actor in _context.Actors)
            {
                if (selectedMainActorsHS.Contains(actor.ActorId.ToString()))
                {
                    if (!filmMainActors.Contains(actor.ActorId))
                    {
                        filmToUpdate.FilmMainActors.Add(new FilmMainActor { FilmId = filmToUpdate.ID, ActorId = actor.ActorId });

                    }
                }
                else
                {
                    if (filmMainActors.Contains(actor.ActorId))
                    {
                        FilmMainActor actorToRemove = filmToUpdate.FilmMainActors.FirstOrDefault(i => i.ActorId == actor.ActorId);

                        _context.Remove(actorToRemove);
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
                .Include(c => c.Language)
                .Include(c => c.Company)
                .AsNoTracking()
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
                .Include(i => i.FilmActors)
                .Include(i => i.FilmMainActors)
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
