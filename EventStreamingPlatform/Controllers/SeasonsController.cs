using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventStreamingPlatform.Data;
using EventStreamingPlatform.Models;
using EventStreamingPlatform.Migrations;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EventStreamingPlatform.Controllers
{
    public class SeasonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeasonsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("api/{controller}")]
        [HttpGet]
        public IActionResult GetAll()
        {

            var season =  _context.Seasons
                .Include(c => c.Serie)
                .Include(e => e.Episode)
                 .ToList();

            return Json(new { data = season });
        }
        // GET: Seasons
        public async Task<IActionResult> Index(string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["EpisodeSortParam"] = sortOrder == "episode" ? "episodeDesc" : "episode";
           

            if (searchString != null)
            {
                pageNumber = 1;

            }

            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var seasons = from a in _context.Seasons select a;

            seasons = _context.Seasons
              .Include(c => c.Episode)
              .Include(c => c.Serie)
              .AsNoTracking();


            if (!String.IsNullOrEmpty(searchString))
            {
                seasons = seasons.Where(a => a.Name.Contains(searchString) );
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    seasons = seasons.OrderByDescending(a => a.Name);
                    break;

                case "episode":
                    seasons = seasons.OrderBy(a => a.Episode);
                    break;

                case "episodeDesc":
                    seasons = seasons.OrderByDescending(a => a.Episode);
                    break;

           


                default:
                    seasons = seasons.OrderBy(a => a.Name);
                    break;
            }



            int pageSize = 3;
            return View(await PaginatedList<Season>.CreateAsync(seasons.AsNoTracking(), pageNumber ?? 1, pageSize));


            //return View(await _context.Seasons.Include(e=>e.Episode).ToListAsync());
        }

        // GET: Seasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Seasons == null)
            {
                return NotFound();
            }

            var season = await _context.Seasons
                
                .Include(c=>c.Serie)
                .Include(e => e.Episode)    
                .FirstOrDefaultAsync(m => m.SeasonId == id);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
        }

        // GET: Seasons/Create
        public IActionResult Create()
        {
            PopulateCountryDropDownList();
            return View();
        }

        // POST: Seasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("SeasonId,Name,Description,SerieId")] Season season)
        {
            if (ModelState.IsValid)
            {
                _context.Add(season);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCountryDropDownList(season.SerieId);
            return View(season);
        }

        // GET: Seasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Seasons == null)
            {
                return NotFound();
            }

            var season = await _context.Seasons.FindAsync(id);
            if (season == null)
            {
                return NotFound();
            }
            PopulateCountryDropDownList(season.SerieId);
            return View(season);
        }
        private void PopulateCountryDropDownList(object selectedCountry = null)
        {
            var countriesQuery = from d in _context.Series
                                 orderby d.Title
                                 select d;
            ViewBag.SerieId = new SelectList(countriesQuery.AsNoTracking(), "SerieId", "Title", selectedCountry);
        }
        // POST: Seasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("SeasonId,Name,Description,SerieId")] Season season)
        {
            if (id != season.SeasonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(season);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeasonExists(season.SeasonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(season);
        }
        [Authorize(Roles = "Admin")]
        // GET: Seasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Seasons == null)
            {
                return NotFound();
            }

            var season = await _context.Seasons
                .FirstOrDefaultAsync(m => m.SeasonId == id);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
        }

        // POST: Seasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Seasons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Seasons'  is null.");
            }
            var season = await _context.Seasons.FindAsync(id);
            if (season != null)
            {
                _context.Seasons.Remove(season);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeasonExists(int id)
        {
          return _context.Seasons.Any(e => e.SeasonId == id);
        }
    }
}
