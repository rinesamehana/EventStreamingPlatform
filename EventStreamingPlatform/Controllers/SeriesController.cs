using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventStreamingPlatform.Data;
using EventStreamingPlatform.Models;
using EventStreamingPlatform.Models.StreamingViewModel;
using EventStreamingPlatform.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EventStreamingPlatform.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("api/{controller}")]
        [HttpGet]
        public IActionResult GetAll()
        {

            var series = _context.Series.Include(c => c.Company)
                  .Include(c => c.Language)
                  .Include(i => i.SerieGenres)
                    .ThenInclude(i => i.Genre)
                        .ThenInclude(i => i.Recomandation)
                    .Include(c => c.SerieActors)
                        .ThenInclude(i => i.Actor)
                     .Include(c => c.SerieMainActors)
                        .ThenInclude(i => i.Actor)
                .Include(i => i.Seasons)
                        .ThenInclude(i => i.Episode)
                 .ToList();

            return Json(new { data = series });
        }
        // GET: Series
        public async Task<IActionResult> Index(
            int? id,
            int? genreId,
            int? actorId,
            int? seasonId,
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
            //ViewData["DurationSortParm"] = sortOrder == "Duration" ? "Duration_desc" : "Duration";
            ViewData["GenresSortParm"] = sortOrder == "Genre" ? "Genre_desc" : "Genre";
         
            //ViewData["RealiseDateSortParm"] = sortOrder == "RealiseDate" ? "RealiseDate_desc" : "RealiseDate";
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
            var films = new List<Serie>();

            bool officeAssignment = false;
            bool courseAssignment = false;


            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Title";
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
                        films = await _context.Series
                            .Include(a=>a.Seasons)
                        .Include(c => c.Company)
                        .Include(c => c.Language)
                        .Include(i => i.SerieGenres)
                        .ThenInclude(i => i.Genre)
                        .ThenInclude(i => i.Recomandation)
                         .Include(c => c.SerieActors)
                        .ThenInclude(i => i.Actor)
                        .Include(c => c.SerieMainActors)
                        .ThenInclude(i => i.Actor)
                        .OrderBy(i => i.Title)
                        .Where(s => s.Title.Contains(searchString))

                        .OrderByDescending(e => EF.Property<object>(e.Company, sortOrder))
                        .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        films = await _context.Series
                            .Include(a => a.Seasons)
                       .Include(c => c.Company)
                       .Include(c => c.Language)
                       .Include(i => i.SerieGenres)
                       .ThenInclude(i => i.Genre)
                       .ThenInclude(i => i.Recomandation)
                        .Include(c => c.SerieActors)
                       .ThenInclude(i => i.Actor)
                       .Include(c => c.SerieMainActors)
                       .ThenInclude(i => i.Actor)
                       .OrderBy(i => i.Title)
                       .Where(s => s.Title.Contains(searchString))

                       .ToListAsync();

                        films.OrderByDescending(e => EF.Property<object>(e.SerieGenres.AsQueryable().Include(i => i.Genre), sortOrder));
                    }
                    else
                    {
                        films = await _context.Series
                            .Include(a => a.Seasons)
                      .Include(c => c.Company)
                      .Include(c => c.Language)
                      .Include(i => i.SerieGenres)
                      .ThenInclude(i => i.Genre)
                      .ThenInclude(i => i.Recomandation)
                       .Include(c => c.SerieActors)
                      .ThenInclude(i => i.Actor)
                      .Include(c => c.SerieMainActors)
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

                        films = await _context.Series
                            .Include(a => a.Seasons)
                       .Include(c => c.Company)
                       .Include(c => c.Language)
                       .Include(i => i.SerieGenres)
                       .ThenInclude(i => i.Genre)
                       .ThenInclude(i => i.Recomandation)
                        .Include(c => c.SerieActors)
                       .ThenInclude(i => i.Actor)
                       .Include(c => c.SerieMainActors)
                       .ThenInclude(i => i.Actor)
                       .OrderBy(i => i.Title)
                        .Where(s => s.Title.Contains(searchString))

                        .OrderBy(e => EF.Property<object>(e.Title, sortOrder))
                         .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        films = await _context.Series.Include(a => a.Seasons)
                    .Include(c => c.Company)
                    .Include(c => c.Language)
                    .Include(i => i.SerieGenres)
                    .ThenInclude(i => i.Genre)
                    .ThenInclude(i => i.Recomandation)
                     .Include(c => c.SerieActors)
                    .ThenInclude(i => i.Actor)
                    .Include(c => c.SerieMainActors)
                    .ThenInclude(i => i.Actor)
                    .OrderBy(i => i.Title)
                     .Where(s => s.Title.Contains(searchString))


                      .ToListAsync();

                        films.OrderBy(e => EF.Property<object>(e.SerieGenres.AsQueryable().Include(i => i.Genre), sortOrder));
                    }
                    else
                    {
                        films = await _context.Series.Include(a => a.Seasons)
                    .Include(c => c.Company)
                    .Include(c => c.Language)
                    .Include(i => i.SerieGenres)
                    .ThenInclude(i => i.Genre)
                    .ThenInclude(i => i.Recomandation)
                     .Include(c => c.SerieActors)
                    .ThenInclude(i => i.Actor)
                    .Include(c => c.SerieMainActors)
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
                        films = await _context.Series.Include(a => a.Seasons)
                    .Include(c => c.Company)
                    .Include(c => c.Language)
                    .Include(i => i.SerieGenres)
                    .ThenInclude(i => i.Genre)
                    .ThenInclude(i => i.Recomandation)
                     .Include(c => c.SerieActors)
                    .ThenInclude(i => i.Actor)
                    .Include(c => c.SerieMainActors)
                    .ThenInclude(i => i.Actor)
                        .OrderByDescending(e => EF.Property<object>(e.Company, sortOrder))
                        .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        films = await _context.Series.Include(a => a.Seasons)
                    .Include(c => c.Company)
                    .Include(c => c.Language)
                    .Include(i => i.SerieGenres)
                    .ThenInclude(i => i.Genre)
                    .ThenInclude(i => i.Recomandation)
                     .Include(c => c.SerieActors)
                    .ThenInclude(i => i.Actor)
                    .Include(c => c.SerieMainActors)
                    .ThenInclude(i => i.Actor)
                            .ToListAsync();

                        films.OrderByDescending(e => EF.Property<object>(e.SerieGenres.AsQueryable().Include(i => i.Genre), sortOrder));
                    }
                    else
                    {
                        films = await _context.Series.Include(a => a.Seasons)
                   .Include(c => c.Company)
                   .Include(c => c.Language)
                   .Include(i => i.SerieGenres)
                   .ThenInclude(i => i.Genre)
                   .ThenInclude(i => i.Recomandation)
                    .Include(c => c.SerieActors)
                   .ThenInclude(i => i.Actor)
                   .Include(c => c.SerieMainActors)
                   .ThenInclude(i => i.Actor)
                       .OrderByDescending(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
                else
                {
                    if (officeAssignment)
                    {
                        films = await _context.Series.Include(a => a.Seasons)
                   .Include(c => c.Company)
                   .Include(c => c.Language)
                   .Include(i => i.SerieGenres)
                   .ThenInclude(i => i.Genre)
                   .ThenInclude(i => i.Recomandation)
                    .Include(c => c.SerieActors)
                   .ThenInclude(i => i.Actor)
                   .Include(c => c.SerieMainActors)
                   .ThenInclude(i => i.Actor)
                       .OrderBy(e => EF.Property<object>(e.Company, sortOrder))
                        .ToListAsync();
                    }
                    else if (courseAssignment)
                    {
                        films = await _context.Series.Include(a => a.Seasons)
                    .Include(c => c.Company)
                    .Include(c => c.Language)
                    .Include(i => i.SerieGenres)
                    .ThenInclude(i => i.Genre)
                    .ThenInclude(i => i.Recomandation)
                     .Include(c => c.SerieActors)
                    .ThenInclude(i => i.Actor)
                    .Include(c => c.SerieMainActors)
                    .ThenInclude(i => i.Actor)
                        .ToListAsync();

                        films.OrderBy(e => EF.Property<object>(e.SerieGenres.AsQueryable().Include(i => i.Genre), sortOrder));
                    }
                    else
                    {
                        films = await _context.Series.Include(a => a.Seasons)
                     .Include(c => c.Company)
                     .Include(c => c.Language)
                     .Include(i => i.SerieGenres)
                     .ThenInclude(i => i.Genre)
                     .ThenInclude(i => i.Recomandation)
                      .Include(c => c.SerieActors)
                     .ThenInclude(i => i.Actor)
                     .Include(c => c.SerieMainActors)
                     .ThenInclude(i => i.Actor)
                         .OrderBy(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
            }

            int pageSize = 3;
            var viewModel = SerieIndexData<Serie>.Create(films, pageNumber ?? 1, pageSize, id);
            if (id != null)
            {
                ViewData["SerieId"] = id.Value;
                var selectedSerie = viewModel.Seriess.Where(x => x.SerieId == id).Single();
                ViewData["Serie"] = selectedSerie.Title;
                await _context.Entry(selectedSerie).Collection(x => x.Seasons).LoadAsync();
                foreach (Season enrollment in selectedSerie.Seasons)
                {
                    await _context.Entry(enrollment).Reference(x => x.Serie).LoadAsync();
                }
                viewModel.Seasons = selectedSerie.Seasons;
            }
            if (seasonId != null)
            {
                ViewData["SeasonId"] = seasonId.Value;
                var selectedSeason = viewModel.Seasons.Where(x => x.SeasonId == seasonId).Single();
                ViewData["Season"] = selectedSeason.Name;
                await _context.Entry(selectedSeason).Collection(x => x.Episode).LoadAsync();
                foreach (Episode enrollment in selectedSeason.Episode)
                {
                    await _context.Entry(enrollment).Reference(x => x.Season).LoadAsync();
                }
                viewModel.Episodes = selectedSeason.Episode;
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

        // GET: Series/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _context.Series
                .Include(c => c.Company)
                 .Include(c => c.Language)
                .FirstOrDefaultAsync(m => m.SerieId == id);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        // GET: Series/Create
        public IActionResult Create()
        {
            var serie = new Serie();
            
            serie.SerieGenres = new List<SerieGenre>();
            serie.SerieActors = new List<SerieActor>();
            serie.SerieMainActors = new List<SerieMainActor>();
          
            PopulateCompnayDropDownList(serie);
            PopulateLanguageDropDownList(serie);
            PopulateAssignedGenreData(serie);
            PopulateAssignedActorData(serie);
            PopulateAssignedMainActorData(serie);
            return View();
        }

        // POST: Series/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Title, Description,Director, PhotoLink ,CompanyId, LanguageId")] Serie serie, string[] selectedGenres, string[] selectedActors, string[] selectedMainActors)
        {
          
            if (selectedGenres != null)
            {
                serie.SerieGenres = new List<SerieGenre>();
                foreach (var genre in selectedGenres)
                {
                    var genreToAdd = new SerieGenre { SerieId = serie.SerieId, GenreId = int.Parse(genre) };
                    serie.SerieGenres.Add(genreToAdd);
                }
            }
            if (selectedActors != null)
            {
                serie.SerieActors = new List<SerieActor>();
                foreach (var actor in selectedActors)
                {
                    var actorToAdd = new SerieActor { SerieId = serie.SerieId, ActorId = int.Parse(actor) };
                    serie.SerieActors.Add(actorToAdd);
                }
            }
            if (selectedMainActors != null)
            {
                serie.SerieMainActors = new List<SerieMainActor>();
                foreach (var actor in selectedMainActors)
                {
                    var actorToAdd = new SerieMainActor { SerieId = serie.SerieId, ActorId = int.Parse(actor) };
                    serie.SerieMainActors.Add(actorToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(serie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            PopulateLanguageDropDownList(serie.LanguageId);
            PopulateCompnayDropDownList(serie.CompanyId);
            PopulateAssignedGenreData(serie);
            PopulateAssignedActorData(serie);
            PopulateAssignedMainActorData(serie);
            return View(serie);
        }

        // GET: Series/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _context.Series
                .Include(i => i.SerieActors).ThenInclude(i => i.Actor)
                .Include(i => i.SerieGenres).ThenInclude(i => i.Genre)
                .Include(i => i.SerieMainActors).ThenInclude(i => i.Actor)
                
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SerieId == id);

            if (serie == null)
            {
                return NotFound();
            }
           
            PopulateLanguageDropDownList(serie.LanguageId);
            PopulateCompnayDropDownList(serie.CompanyId);
            PopulateAssignedGenreData(serie);
            PopulateAssignedActorData(serie);
            PopulateAssignedMainActorData(serie);
            return View(serie);
        }

       
        private void PopulateAssignedGenreData(Serie serie)
        {
            var allGenres = _context.Genres;
            var serieGenres = new HashSet<int>(serie.SerieGenres.Select(c => c.GenreId));
            var viewModel = new List<AssignedGenreData>();
            foreach (var genre in allGenres)
            {
                viewModel.Add(new AssignedGenreData
                {
                    GenreId = genre.GenreId,
                    Name = genre.Name,
                    Assigned = serieGenres.Contains(genre.GenreId)
                });
            }
            ViewData["Genres"] = viewModel;
        }
        private void PopulateAssignedActorData(Serie serie)
        {
            var allActors = _context.Actors;
            var serieActors = new HashSet<int>(serie.SerieActors.Select(c => c.ActorId));
            var viewModel = new List<AssignedActorData>();
            foreach (var actor in allActors)
            {
                viewModel.Add(new AssignedActorData
                {
                    ActorId = actor.ActorId,
                    Name = actor.Name,
                    Assigned = serieActors.Contains(actor.ActorId)
                });
            }
            ViewData["Actors"] = viewModel;
        }
        private void PopulateAssignedMainActorData(Serie serie)
        {
            var allActors = _context.Actors;
            var serieActors = new HashSet<int>(serie.SerieMainActors.Select(c => c.ActorId));
            var viewModel = new List<AssignedMainActorData>();
            foreach (var actor in allActors)
            {
                viewModel.Add(new AssignedMainActorData
                {
                    ActorId = actor.ActorId,
                    Name = actor.Name,
                    Assigned = serieActors.Contains(actor.ActorId)
                });
            }
            ViewData["Actorss"] = viewModel;
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, string[] selectedGenres, string[] selectedActors, string[] selectedMainActors)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serieToUpdate = await _context.Series
                .Include(i => i.SerieActors)
                    .ThenInclude(i => i.Actor)
                .Include(i => i.SerieMainActors)
                    .ThenInclude(i => i.Actor)
                .Include(i => i.SerieGenres)
                    .ThenInclude(i => i.Genre)
                .FirstOrDefaultAsync(m => m.SerieId == id);

            if (await TryUpdateModelAsync<Serie>(
                serieToUpdate,
                "",
                i => i.Title, i => i.Description, i => i.Director, i => i.PhotoLink, c => c.CompanyId, c => c.LanguageId))
            {

                UpdateSerieSeason(selectedGenres, selectedActors, selectedMainActors, serieToUpdate);
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
            PopulateLanguageDropDownList(serieToUpdate.LanguageId);
            PopulateCompnayDropDownList(serieToUpdate.CompanyId);  
            UpdateSerieSeason( selectedGenres, selectedActors, selectedMainActors, serieToUpdate);
           
            return View(serieToUpdate);
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
        private void UpdateSerieSeason(string[] selectedGenres, string[] selectedActors, string[] selectedMainActors, Serie serieToUpdate)
        {
            if (selectedGenres == null)
            {
              
                serieToUpdate.SerieGenres = new List<SerieGenre>();
                serieToUpdate.SerieActors = new List<SerieActor>();
                serieToUpdate.SerieMainActors = new List<SerieMainActor>();
                return;
            }
            var selectedGenresHS = new HashSet<string>(selectedGenres);
            var selectedActorsHS = new HashSet<string>(selectedActors);
            var selectedMainActorsHS = new HashSet<string>(selectedMainActors);
            var serieGenres = new HashSet<int>(serieToUpdate.SerieGenres.Select(c => c.Genre.GenreId));
            var serieActors = new HashSet<int>(serieToUpdate.SerieActors.Select(c => c.Actor.ActorId));
            var serieMainActors = new HashSet<int>(serieToUpdate.SerieMainActors.Select(c => c.Actor.ActorId));
           
           

           
            foreach (var genre in _context.Genres)
            {
                if (selectedGenresHS.Contains(genre.GenreId.ToString()))
                {
                    if (!serieGenres.Contains(genre.GenreId))
                    {
                        serieToUpdate.SerieGenres.Add(new SerieGenre { SerieId = serieToUpdate.SerieId, GenreId = genre.GenreId });
                    }
                }
                else
                {
                    if (serieGenres.Contains(genre.GenreId))
                    {
                        SerieGenre genreToRemove = serieToUpdate.SerieGenres.FirstOrDefault(i => i.GenreId == genre.GenreId);
                        _context.Remove(genreToRemove);
                    }
                }
            }
            foreach (var actor in _context.Actors)
            {
                if (selectedActorsHS.Contains(actor.ActorId.ToString()))
                {
                    if (!serieActors.Contains(actor.ActorId))
                    {
                        serieToUpdate.SerieActors.Add(new SerieActor { SerieId = serieToUpdate.SerieId, ActorId = actor.ActorId });

                    }
                }
                else
                {
                    if (serieActors.Contains(actor.ActorId))
                    {
                        SerieActor actorToRemove = serieToUpdate.SerieActors.FirstOrDefault(i => i.ActorId == actor.ActorId);

                        _context.Remove(actorToRemove);
                    }
                }
            }
            foreach (var actor in _context.Actors)
            {
                if (selectedMainActorsHS.Contains(actor.ActorId.ToString()))
                {
                    if (!serieMainActors.Contains(actor.ActorId))
                    {
                        serieToUpdate.SerieMainActors.Add(new SerieMainActor { SerieId = serieToUpdate.SerieId, ActorId = actor.ActorId });

                    }
                }
                else
                {
                    if (serieMainActors.Contains(actor.ActorId))
                    {
                        SerieMainActor actorToRemove = serieToUpdate.SerieMainActors.FirstOrDefault(i => i.ActorId == actor.ActorId);

                        _context.Remove(actorToRemove);
                    }
                }
            }
        }

        // GET: Series/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _context.Series
                 .Include(c => c.Language)
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.SerieId == id);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }
    
        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Serie serie = await _context.Series
      
                 .Include(i => i.SerieGenres)
                .Include(i => i.SerieActors)
                .Include(i => i.SerieMainActors)
                .SingleAsync(i => i.SerieId == id);

           

            _context.Series.Remove(serie);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}