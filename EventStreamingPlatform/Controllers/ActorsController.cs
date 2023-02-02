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
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("api/{controller}")]
        [HttpGet]
        public IActionResult GetAll()
        {

            var actors = _context.Actors.Include(c => c.Gender)
                 .Include(c => c.Country)
                 .Include(c => c.City).ToList();

            return Json(new { data = actors });
        }
        // GET: Actors
        public async Task<IActionResult> Index(string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["LastnameSortParam"] = sortOrder == "lastname" ? "lastnameDesc" : "lastname";
            ViewData["AgeSortParam"] = sortOrder == "age" ? "ageDesc" : "age";
            ViewData["GenderSortParam"] = sortOrder == "gender" ? "genderDesc" : "gender";
            ViewData["CountrySortParam"] = sortOrder == "country" ? "countryDesc" : "country";
            ViewData["CitySortParam"] = sortOrder == "city" ? "cityDesc" : "city";

            if (searchString != null)
            {
                pageNumber = 1;

            }

            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var actors = from a in _context.Actors select a;

            actors = _context.Actors
                .Include(c => c.Gender)
                 .Include(c => c.Country)
                 .Include(c => c.City)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                actors = actors.Where(a => a.Name.Contains(searchString) || a.LastName.Contains(searchString) ||
                                       a.Gender.Name.Contains(searchString) || a.Country.Name.Contains(searchString) ||
                                       a.City.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    actors = actors.OrderByDescending(a => a.Name);
                    break;

                case "lastname":
                    actors = actors.OrderBy(a => a.LastName);
                    break;

                case "lastnameDesc":
                    actors = actors.OrderByDescending(a => a.LastName);
                    break;

                case "age":
                    actors = actors.OrderBy(a => a.Age);
                    break;

                case "ageDesc":
                    actors = actors.OrderByDescending(a => a.Age);
                    break;


                case "gender":
                    actors = actors.OrderBy(a => a.Gender);
                    break;

                case "genderDesc":
                    actors = actors.OrderByDescending(a => a.Gender);
                    break;

                case "country":
                    actors = actors.OrderBy(a => a.Country);
                    break;

                case "countryDesc":
                    actors = actors.OrderByDescending(a => a.Country);
                    break;

                case "city":
                    actors = actors.OrderBy(a => a.City);
                    break;

                case "cityDesc":
                    actors = actors.OrderByDescending(a => a.City);
                    break;


                default:
                    actors = actors.OrderBy(a => a.Name);
                    break;
            }






            int pageSize = 3;
            return View(await PaginatedList<Actor>.CreateAsync(actors.AsNoTracking(), pageNumber ?? 1, pageSize));


            //return View(await genres.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .Include(i=>i.Gender)
                 .Include(i => i.Country)
                 .Include(i => i.City)
                .FirstOrDefaultAsync(m => m.ActorId == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            
            PopulateGenderDropDownList();
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorId,Name,LastName,Age, GenderId, CountryId,CityId")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateGenderDropDownList(actor.GenderId, actor.CountryId, actor.CityId);
       
            return View(actor);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            PopulateGenderDropDownList(actor.GenderId, actor.CountryId, actor.CityId);
           
            return View(actor);
        }

        // POST: Actors/Edit/5
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

            var actorToUpdate = await _context.Actors
                .FirstOrDefaultAsync(c => c.ActorId == id);

            if (await TryUpdateModelAsync<Actor>(actorToUpdate,
                "",
                c => c.Name, c=>c.LastName , c => c.Age, c => c.GenderId, c => c.CountryId, c=>c.CityId))
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
            PopulateGenderDropDownList(actorToUpdate.GenderId, actorToUpdate.CountryId, actorToUpdate.CityId);
            return View(actorToUpdate);
        }

        private void PopulateGenderDropDownList(object selectedGender = null, object selectedCountry = null, object selectedCity = null)
        {
            var gendersQuery = from d in _context.Genders
                                      orderby d.Name
                                      select d;
            ViewBag.GenderId = new SelectList(gendersQuery.AsNoTracking(), "GenderId", "Name", selectedGender);

            var countriesQuery = from d in _context.Countries
                               orderby d.Name
                               select d;
            ViewBag.CountryId = new SelectList(countriesQuery.AsNoTracking(), "CountryId", "Name", selectedCountry);

            var citiesQuery = from d in _context.Cities
                              orderby d.Name
                                 select d;
            ViewBag.CityId = new SelectList(citiesQuery.AsNoTracking(), "CityId", "Name", selectedCity);
        }


        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .Include(c => c.Gender)
                .Include(c => c.Country)
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.ActorId == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Actors'  is null.");
            }
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
          return _context.Actors.Any(e => e.ActorId == id);
        }
    }
}
