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

        // GET: Genres
        public async Task<IActionResult> Index(string sortOrder,
             string currentFilter,
             string searchString,
             int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";


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
                episodes = episodes.Where(a => a.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    episodes = episodes.OrderByDescending(a => a.Name);
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
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "Title");
            PopulateRecomandationDropDownList();
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EpisodeId,Name,SeasonId,SerieId,RowVersion")] Episode genre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "Title", genre.SerieId);
            PopulateRecomandationDropDownList(genre.SeasonId);
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Episodes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EpisodeId == id);
            if (genre == null)
            {
                return NotFound();
            }
            PopulateRecomandationDropDownList(genre.SeasonId);
            return View(genre);
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

            var genreToUpdate = await _context.Episodes
                .FirstOrDefaultAsync(c => c.EpisodeId == id);

            if (await TryUpdateModelAsync<Episode>(genreToUpdate,
                "",
                c => c.Name, c => c.SeasonId))
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
            PopulateRecomandationDropDownList(genreToUpdate.SeasonId);
            return View(genreToUpdate);
        }

        private void PopulateRecomandationDropDownList(object selectedRecomandation = null)
        {
            var recomandationsQuery = from d in _context.Seasons
                                      orderby d.Name
                                      select d;
            ViewBag.SeasonId = new SelectList(recomandationsQuery.AsNoTracking(), "SeasonId", "Name", selectedRecomandation);
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
