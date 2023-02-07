using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventStreamingPlatform.Data;
using EventStreamingPlatform.Models;

namespace EventStreamingPlatform.Controllers
{
    public class EpisodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EpisodesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("api/{controller}")]
        [HttpGet]
        public IActionResult GetAll()
        {

            var episodes = _context.Episodes
                .Include(c => c.Season)
                .Include(c => c.Serie)
                 .ToList();

            return Json(new { data = episodes });
        }
        // GET: Genres
        public async Task<IActionResult> Index(string sortOrder,
             string currentFilter,
             string searchString,
             int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["DescriptionSortParam"] = sortOrder == "description" ? "descriptionDesc" : "description";
            ViewData["SeasonSortParam"] = sortOrder == "season" ? "seasonDesc" : "season";
            ViewData["SerieSortParam"] = sortOrder == "serie" ? "serieDesc" : "serie";


            if (searchString != null)
            {
                pageNumber = 1;

            }

            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var episodes = from a in _context.Episodes select a;

            episodes = _context.Episodes
           
                .Include(c => c.Season)
                .Include(c=>c.Serie)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                episodes = episodes.Where(a => a.Name.Contains(searchString) || a.Season.Name.Contains(searchString)
                                          || a.Serie.Title.Contains(searchString) || a.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    episodes = episodes.OrderByDescending(a => a.Name);
                    break;

                case "description":
                    episodes = episodes.OrderBy(a => a.Description);
                    break;

                case "descriptionDesc":
                    episodes = episodes.OrderByDescending(a => a.Description);
                    break;

                case "season":
                    episodes = episodes.OrderBy(a => a.Season);
                    break;

                case "seasonDesc":
                    episodes = episodes.OrderByDescending(a => a.Season);
                    break;

                case "serie":
                    episodes = episodes.OrderBy(a => a.Serie);
                    break;

                case "serieDesc":
                    episodes = episodes.OrderByDescending(a => a.Serie);
                    break;


                default:
                    episodes = episodes.OrderBy(a => a.Name);
                    break;
            }






            int pageSize = 3;
            return View(await PaginatedList<Episode>.CreateAsync(episodes.AsNoTracking(), pageNumber ?? 1, pageSize));


            //return View(await genres.ToListAsync());
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string query = "SELECT * FROM Episode WHERE EpisodeId = {0}";
            var episode = await _context.Episodes
                 .FromSqlRaw(query, id)
        .Include(t => t.Comments)
                .ThenInclude(c => c.Author)
                .Include(c => c.Season)
                .Include(c => c.Serie)
                .AsNoTracking()
                .FirstOrDefaultAsync();
           
            if (episode == null)
            {
                return NotFound();
            }

            return View(episode);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            
            PopulateSeasonDropDownList();
            PopulateSeriesDropDownList();
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EpisodeId,Name,Description,LastUpdatedDate,SeasonId,SerieId,RowVersion")] Episode episode)
        {
            if (ModelState.IsValid)
            {
                episode.LastUpdatedDate = DateTime.Now;
                _context.Add(episode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateSeasonDropDownList(episode.SeasonId);
            PopulateSeriesDropDownList(episode.SerieId);
            return View(episode);
        }
     
        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episode = await _context.Episodes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EpisodeId == id);
            if (episode == null)
            {
                return NotFound();
            }
            PopulateSeasonDropDownList(episode.SeasonId);
            PopulateSeriesDropDownList(episode.SerieId);
            return View(episode);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodeToUpdate = await _context.Episodes
                .FirstOrDefaultAsync(c => c.EpisodeId == id);

            if (await TryUpdateModelAsync<Episode>(episodeToUpdate,
                "",
                c => c.Name, c=>c.Description, c => c.SeasonId, d=>d.SerieId))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateSeasonDropDownList(episodeToUpdate.SeasonId);
            PopulateSeriesDropDownList(episodeToUpdate.SerieId);
            return View(episodeToUpdate);
        }

        private void PopulateSeasonDropDownList(object selectedSeason = null)
        {
            var seasonsQuery = from d in _context.Seasons
                                      orderby d.Name
                                      select d;
            ViewBag.SeasonId = new SelectList(seasonsQuery.AsNoTracking(), "SeasonId", "Description",  selectedSeason);
        }
        private void PopulateSeriesDropDownList(object selectedSerie = null)
        {
            var seriesQuery = from d in _context.Series
                                      orderby d.Title
                                      select d;
            ViewBag.SerieId = new SelectList(seriesQuery.AsNoTracking(), "SerieId", "Title", selectedSerie);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Episodes
                .Include(c => c.Season)
                .Include(c => c.Serie)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EpisodeId == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await _context.Episodes.FindAsync(id);
            _context.Episodes.Remove(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(int id)
        {
            return _context.Episodes.Any(e => e.EpisodeId == id);
        }
    }
}
