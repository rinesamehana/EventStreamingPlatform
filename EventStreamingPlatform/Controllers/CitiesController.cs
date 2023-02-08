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
    [Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("api/{controller}")]
        [HttpGet]
        public IActionResult GetAll()
        {

            var cities = _context.Cities
                 .Include(c => c.Country)
                 .ToList();

            return Json(new { data = cities });
        }
        // GET: Cities
        public async Task<IActionResult> Index(string sortOrder,
              string currentFilter,
              string searchString,
              int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["CountrySortParam"] = sortOrder == "country" ? "countryDesc" : "country";

            if (searchString != null)
            {
                pageNumber = 1;

            }

            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var cities = from a in _context.Cities select a;

            cities = _context.Cities

                .Include(c => c.Country)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                cities = cities.Where(a => a.Name.Contains(searchString) || a.Country.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    cities = cities.OrderByDescending(a => a.Name);
                    break;

                case "country":
                    cities = cities.OrderBy(a => a.Country);
                    break;

                case "countryDesc":
                    cities = cities.OrderByDescending(a => a.Country);
                    break;



                default:
                    cities = cities.OrderBy(a => a.Name);
                    break;
            }






            int pageSize = 3;
            return View(await PaginatedList<City>.CreateAsync(cities.AsNoTracking(), pageNumber ?? 1, pageSize));


            //return View(await genres.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(i => i.Country)
                .FirstOrDefaultAsync(m => m.CityId == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            PopulateCountryDropDownList();
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityId,Name, CountryId")] City city)
        {
            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCountryDropDownList(city.CountryId);
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            PopulateCountryDropDownList(city.CountryId);
            return View(city);
        }

        // POST: Cities/Edit/5
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

            var cityToUpdate = await _context.Cities
                .FirstOrDefaultAsync(c => c.CityId == id);

            if (await TryUpdateModelAsync<City>(cityToUpdate,
                "",
                c => c.Name, c => c.CountryId))
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
            PopulateCountryDropDownList(cityToUpdate.CountryId);
            return View(cityToUpdate);
        }

        private void PopulateCountryDropDownList(object selectedCountry = null)
        {
            var countriesQuery = from d in _context.Countries
                               orderby d.Name
                               select d;
            ViewBag.CountryId = new SelectList(countriesQuery.AsNoTracking(), "CountryId", "Name", selectedCountry);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.CityId == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cities'  is null.");
            }
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitiesExists(int id)
        {
            return _context.Cities.Any(e => e.CityId == id);
        }
    }
}
