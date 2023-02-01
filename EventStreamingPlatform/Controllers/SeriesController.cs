using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventStreamingPlatform.Data;
using EventStreamingPlatform.Models;
using EventStreamingPlatform.Models.StreamingViewModel;

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

            var series = _context.Series
                .Include(i => i.SerieSeasons)
                    .ThenInclude(i => i.Season)
                        .ThenInclude(i => i.Episode)
                 .ToList();

            return Json(new { data = series });
        }
        // GET: Series
        public async Task<IActionResult> Index(int? id, int? seasonId)
        {
            var viewModel = new SerieIndexData();
            viewModel.Seriess = await _context.Series
                  .Include(i => i.SerieSeasons)
                    .ThenInclude(i => i.Season)
                        .ThenInclude(i=>i.Episode)
                  .OrderBy(i => i.Title)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["SeriesId"] = id.Value;
                Serie serie = viewModel.Seriess.Single(
                    i => i.SerieId == id.Value);
                viewModel.Seasons = serie.SerieSeasons.Select(s => s.Season);
                ViewData["Series"] = serie.Title;
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
                 .Include(e => e.SerieSeasons)
                 .ThenInclude(e => e.Season)
                 .ThenInclude(e=>e.Episode)
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
            serie.SerieSeasons = new List<SerieSeason>();
            PopulateAssignedSeasonData(serie);
            return View();
        }

        // POST: Series/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, Description")] Serie serie, string[] selectedSeasons)
        {
            if (selectedSeasons != null)
            {
                serie.SerieSeasons = new List<SerieSeason>();
                foreach (var season in selectedSeasons)
                {
                    var seasonToAdd = new SerieSeason { SerieId = serie.SerieId, SeasonId = int.Parse(season) };
                    serie.SerieSeasons.Add(seasonToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(serie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedSeasonData(serie);
            return View(serie);
        }

        // GET: Series/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _context.Series

                .Include(i => i.SerieSeasons).ThenInclude(i => i.Season)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SerieId == id);

            if (serie == null)
            {
                return NotFound();
            }
            PopulateAssignedSeasonData(serie);
            return View(serie);
        }

        private void PopulateAssignedSeasonData(Serie serie)
        {
            var allSeries = _context.Seasons;
            var serieSeasons = new HashSet<int>(serie.SerieSeasons.Select(c => c.SeasonId));
            var viewModel = new List<AssignedSeasonData>();
            foreach (var season in allSeries)
            {
                viewModel.Add(new AssignedSeasonData
                {
                    SeasonId = season.SeasonId,
                    Name = season.Name,
                    Assigned = serieSeasons.Contains(season.SeasonId)
                });
            }
            ViewData["Seasons"] = viewModel;
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedSeasons)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serieToUpdate = await _context.Series

                .Include(i => i.SerieSeasons)
                    .ThenInclude(i => i.Season)
                .FirstOrDefaultAsync(m => m.SerieId == id);

            if (await TryUpdateModelAsync<Serie>(
                serieToUpdate,
                "",
                i => i.Title, i => i.Description))
            {

                UpdateSerieSeason(selectedSeasons, serieToUpdate);
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
            UpdateSerieSeason(selectedSeasons, serieToUpdate);
            PopulateAssignedSeasonData(serieToUpdate);
            return View(serieToUpdate);
        }

        private void UpdateSerieSeason(string[] selectedSeasons, Serie serieToUpdate)
        {
            if (selectedSeasons == null)
            {
                serieToUpdate.SerieSeasons = new List<SerieSeason>();
                return;
            }

            var selectedSeasonsHS = new HashSet<string>(selectedSeasons);
            var serieSeason = new HashSet<int>(serieToUpdate.SerieSeasons.Select(c => c.Season.SeasonId));

            foreach (var season in _context.Seasons)
            {
                if (selectedSeasonsHS.Contains(season.SeasonId.ToString()))
                {
                    if (!serieSeason.Contains(season.SeasonId))
                    {
                        serieToUpdate.SerieSeasons.Add(new SerieSeason { SerieId = serieToUpdate.SerieId, SeasonId = season.SeasonId });
                    }
                }
                else
                {
                    if (serieSeason.Contains(season.SeasonId))
                    {
                        SerieSeason seasonToRemove = serieToUpdate.SerieSeasons.FirstOrDefault(i => i.SeasonId == season.SeasonId);
                        _context.Remove(seasonToRemove);
                    }
                }
            }
        }

        // GET: Series/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _context.Series
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
                .Include(i => i.SerieSeasons)
                .SingleAsync(i => i.SerieId == id);

            var recomandations = await _context.Episodes
                .Where(d => d.SerieId == id)
                .ToListAsync();
            recomandations.ForEach(d => d.SerieId = null);

            _context.Series.Remove(serie);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}