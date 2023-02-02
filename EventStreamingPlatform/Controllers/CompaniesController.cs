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

namespace EventStreamingPlatform.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("api/{controller}")]
        [HttpGet]
        public IActionResult GetAll()
        {

            var companies = _context.Company
                
                 .ToList();

            return Json(new { data = companies });
        }
        // GET: Companies
        public async Task<IActionResult> Index(string sortOrder,
             string currentFilter,
             string searchString,
             int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["CompanydescSortParam"] = sortOrder == "companydesc" ? "companydescDesc" : "companydesc";
            ViewData["FoundedSortParam"] = sortOrder == "founded" ? "foundedDesc" : "founded";

            if (searchString != null)
            {
                pageNumber = 1;

            }

            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var companies = from a in _context.Company select a;

            companies = _context.Company
               .Include(c => c.Country)
               .Include(c=>c.City)
               .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                companies = companies.Where(a => a.CompanyName.Contains(searchString) || a.CompanyDesc.Contains(searchString) );
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    companies = companies.OrderByDescending(a => a.CompanyName);
                    break;

                case "founded":
                    companies = companies.OrderBy(a => a.CreatedDate);
                    break;

                case "foundedDesc":
                    companies = companies.OrderByDescending(a => a.CreatedDate);
                    break;

                case "companydesc":
                    companies = companies.OrderBy(a => a.CompanyDesc);
                    break;

                case "companydescDesc":
                    companies = companies.OrderByDescending(a => a.CompanyDesc);
                    break;

                default:
                    companies = companies.OrderBy(a => a.CompanyName);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Company>.CreateAsync(companies.AsNoTracking(), pageNumber ?? 1, pageSize));

            // return View(await _context.Companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Company == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .Include(c => c.Country)
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            PopulateCountryDropDownList();
            PopulateCityDropDownList();
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,CompanyName,CreatedDate,CompanyDesc,CountryId,CityId")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCountryDropDownList(company.CountryId);
            PopulateCityDropDownList(company.CityId);
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Company == null)
            {
                return NotFound();
            }

            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            PopulateCountryDropDownList(company.CountryId);
            PopulateCityDropDownList(company.CityId);
            return View(company);
        }

        // POST: Companies/Edit/5
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

            var companyToUpdate = await _context.Company
                .FirstOrDefaultAsync(c => c.CompanyId == id);

            if (await TryUpdateModelAsync<Company>(companyToUpdate,
                "",
                c => c.CompanyName,c=>c.CompanyDesc, c=>c.CreatedDate, c=>c.CityId,c => c.CountryId))
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
            PopulateCountryDropDownList(companyToUpdate.CountryId);
            PopulateCityDropDownList(companyToUpdate.CityId);
            return View(companyToUpdate);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Company == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .Include(c => c.Country)
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }
        private void PopulateCountryDropDownList(object selectedCountry = null)
        {
            var countriesQuery = from d in _context.Countries
                                 orderby d.Name
                                 select d;
            ViewBag.CountryId = new SelectList(countriesQuery.AsNoTracking(), "CountryId", "Name", selectedCountry);
        }
        private void PopulateCityDropDownList(object selectedCity = null)
        {
            var citiesQuery = from d in _context.Cities
                                 orderby d.Name
                                 select d;
            ViewBag.CityId = new SelectList(citiesQuery.AsNoTracking(), "CityId", "Name", selectedCity);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Company == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Companies'  is null.");
            }
            var company = await _context.Company.FindAsync(id);
            if (company != null)
            {
                _context.Company.Remove(company);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
          return _context.Company.Any(e => e.CompanyId == id);
        }
    }
}
