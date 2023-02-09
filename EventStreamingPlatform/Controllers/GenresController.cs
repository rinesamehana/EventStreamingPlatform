using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventStreamingPlatform.Data;
using EventStreamingPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EventStreamingPlatform.Controllers
{
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("api/{controller}")]
        [HttpGet]
        public IActionResult GetAll()
        {

            var genres = _context.Genres
                .Include(c => c.Recomandation)
                 .ToList();

            return Json(new { data = genres });
        }
        // GET: Genres
        public async Task<IActionResult> Index(string sortOrder,
             string currentFilter,
             string searchString,
             int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["RecomandationSortParam"] = sortOrder == "recomandation" ? "recomandationDesc" : "recomandation";
            ViewData["GenreIDSortParam"] = sortOrder == "genreid" ? "genreidDesc" : "genreid";

            if (searchString != null)
            {
                pageNumber = 1;

            }

            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var genres = from a in _context.Genres select a;

            genres = _context.Genres
                .Include(c => c.Recomandation)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                genres = genres.Where(a => a.Name.Contains(searchString) ||  a.Recomandation.Name.Contains(searchString) );
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    genres = genres.OrderByDescending(a => a.Name);
                    break;

                case "recomandation":
                    genres = genres.OrderBy(a => a.Recomandation);
                    break;

                case "recomandationDesc":
                    genres = genres.OrderByDescending(a => a.Recomandation);
                    break;

                case "genreid":
                    genres = genres.OrderBy(a => a.GenreId);
                    break;

                case "genreidDesc":
                    genres = genres.OrderByDescending(a => a.GenreId);
                    break;

                default:
                    genres = genres.OrderBy(a => a.Name);
                    break;
            }

            


          

            int pageSize = 3;
            return View(await PaginatedList<Genre>.CreateAsync(genres.AsNoTracking(), pageNumber ?? 1, pageSize));


            //return View(await genres.ToListAsync());
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .Include(c => c.Recomandation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.GenreId == id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            PopulateRecomandationDropDownList();
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("GenreId,Name,RecomandationId")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateRecomandationDropDownList(genre.RecomandationId);
            return View(genre);
        }

        // GET: Genres/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }
            PopulateRecomandationDropDownList(genre.RecomandationId);
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreToUpdate = await _context.Genres
                .FirstOrDefaultAsync(c => c.GenreId == id);

            if (await TryUpdateModelAsync<Genre>(genreToUpdate,
                "",
                c => c.Name, c => c.RecomandationId))
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
            PopulateRecomandationDropDownList(genreToUpdate.RecomandationId);
            return View(genreToUpdate);
        }

        private void PopulateRecomandationDropDownList(object selectedRecomandation = null)
        {
            var recomandationsQuery = from d in _context.Recomandations
                                   orderby d.Name
                                   select d;
            ViewBag.RecomandationId = new SelectList(recomandationsQuery.AsNoTracking(), "RecomandationId", "Name", selectedRecomandation);
        }

        // GET: Genres/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .Include(c => c.Recomandation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.GenreId == id);
        }
    }
}
