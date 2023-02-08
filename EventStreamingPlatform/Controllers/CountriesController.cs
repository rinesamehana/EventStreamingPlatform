
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventStreamingPlatform.Data;
using EventStreamingPlatform.Models;
using EventStreamingPlatform.Models.StreamingViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace EventStreamingPlatform.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("api/{controller}")]
        [HttpGet]
        public IActionResult GetAll()
        {

            var countries = _context.Countries
                .Include(i => i.CountryLanguages)
                    .ThenInclude(i => i.Language)
                 .ToList();

            return Json(new { data = countries });
        }
        // GET: Films
        public async Task<IActionResult> Index(int? id, int? languageId,
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CountryNameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["CountryPopulationSortParm"] = sortOrder == "Popullation" ? "Popullation_desc" : "Popullation";
            ViewData["CountryAbbreviationSortParm"] = sortOrder == "Abbreviation" ? "Abbreviation_desc" : "Abbreviation";
            ViewData["CountryIsoCodeSortParm"] = sortOrder == "ISO_Code" ? "ISO_Code_desc" : "ISO_Code";
            ViewData["LanguageSortParm"] = sortOrder == "LanguageName" ? "LanguageName_desc" : "LanguageName";


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var countries = new List<Country>();

      
            bool languages = false;
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Name";
            }
            else if(string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Popullation";
            }
            else if(string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Abbreviation";
            }
            else if(string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "ISO_Code";
            }

            else if (sortOrder.Equals("LanguageName_desc") || sortOrder.Equals("LanguageName"))
            {
                languages = true;
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
                    
                    if (languages)
                    {
                        countries = await _context.Countries
                        .Include(i => i.CountryLanguages)
                            .ThenInclude(i => i.Language)
                        .Where(s => s.Name.Contains(searchString))
                       
                        .ToListAsync();

                        countries.OrderByDescending(e => EF.Property<object>(e.CountryLanguages.AsQueryable().Include(i => i.Language), sortOrder));
                    }
                    else
                    {
                        countries = await _context.Countries
                        .Include(i => i.CountryLanguages)
                            .ThenInclude(i => i.Language)
                        .Where(s => s.Name.Contains(searchString))
                        
                        .OrderByDescending(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
                else
                {
                    if (languages)
                    {
                        countries = await _context.Countries
                        .Include(i => i.CountryLanguages)
                            .ThenInclude(i => i.Language)
                        .Where(s => s.Name.Contains(searchString))
                        
                        .ToListAsync();

                        countries.OrderBy(e => EF.Property<object>(e.CountryLanguages.AsQueryable().Include(i => i.Language), sortOrder));
                    }
                    else
                    {
                        countries = await _context.Countries
                        .Include(i => i.CountryLanguages)
                            .ThenInclude(i => i.Language)
                        .Where(s => s.Name.Contains(searchString))
                 
                        .OrderBy(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
            }
            else
            {
                if (descending)
                {
                    if (languages)
                    {
                        countries = await _context.Countries
                            .Include(i => i.CountryLanguages)
                                .ThenInclude(i => i.Language)
                            .ToListAsync();

                        countries.OrderByDescending(e => EF.Property<object>(e.CountryLanguages.AsQueryable().Include(i => i.Language), sortOrder));
                    }
                    else
                    {
                        countries = await _context.Countries
                        .Include(i => i.CountryLanguages)
                            .ThenInclude(i => i.Language)
                        .OrderByDescending(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
                else
                {
                    if (languages)
                    {
                        countries = await _context.Countries
                        .Include(i => i.CountryLanguages)
                                .ThenInclude(i => i.Language)
                        .ToListAsync();

                        countries.OrderBy(e => EF.Property<object>(e.CountryLanguages.AsQueryable().Include(i => i.Language), sortOrder));
                    }
                    else
                    {
                        countries = await _context.Countries
                        .Include(i => i.CountryLanguages)
                                .ThenInclude(i => i.Language)
                        .OrderBy(e => EF.Property<object>(e, sortOrder))
                        .ToListAsync();
                    }
                }
            }

            int pageSize = 3;
            var viewModel = PagedCountryIndexData<Country>.Create(countries, pageNumber ?? 1, pageSize, id);

            if (id != null)
            {
                ViewData["CountryId"] = id.Value;
                Country country = viewModel.Countries.Where(
                    i => i.CountryId == id.Value).Single();
                viewModel.Languages = country.CountryLanguages.Select(s => s.Language);
                //ViewData["Country"] = country.Name;
            }

            if (languageId != null && languageId!=null)
            {
                ViewData["LanguageId"] = languageId.Value;
                var selectedLanguage = viewModel.Languages.Where(x => x.LanguageId == languageId).Single();

                //ViewData["Language"] = selectedLanguage.LanguageName;

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

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            var country = new Country();
            country.CountryLanguages = new List<CountryLanguage>();
            PopulateAssignedLanguageData(country);
            return View();
        }

        // POST: Films/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Abbreviation, Popullation, ISO_Code")] Country country, string[] selectedLanguages)
        {
            if (selectedLanguages != null)
            {
                country.CountryLanguages = new List<CountryLanguage>();
                foreach (var language in selectedLanguages)
                {
                    var languageToAdd = new CountryLanguage { CountryId = country.CountryId, LanguageId = int.Parse(language) };
                    country.CountryLanguages.Add(languageToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateAssignedLanguageData(country);
            return View(country);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(i => i.CountryLanguages).ThenInclude(i => i.Language)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CountryId == id);

            if (country == null)
            {
                return NotFound();
            }

            PopulateAssignedLanguageData(country);
            return View(country);
        }

        private void PopulateAssignedLanguageData(Country country)
        {
            var allLanguages = _context.Languages;
            var filmLanguages = new HashSet<int>(country.CountryLanguages.Select(c => c.LanguageId));
            var viewModel = new List<LanguageAssignedData>();
            foreach (var language in allLanguages)
            {
                viewModel.Add(new LanguageAssignedData
                {
                    LanguageId = language.LanguageId,
                    LanguageName = language.LanguageName,
                    Assigned = filmLanguages.Contains(language.LanguageId)
                });
            }
            ViewData["Languages"] = viewModel;
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedLanguages)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countriesToUpdate = await _context.Countries
                .Include(i => i.CountryLanguages)
                .ThenInclude(i => i.Language)
                .FirstOrDefaultAsync(m => m.CountryId == id);

            if (await TryUpdateModelAsync<Country>(
                countriesToUpdate,
                "",
                i => i.Name, c => c.Abbreviation, c => c.Popullation, c => c.ISO_Code))
            {

                UpdateCountryLanguage(selectedLanguages, countriesToUpdate);
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
            UpdateCountryLanguage(selectedLanguages, countriesToUpdate);
            PopulateAssignedLanguageData(countriesToUpdate);
            return View(countriesToUpdate);
        }



        private void UpdateCountryLanguage(string[] selectedLanguages, Country countriesToUpdate)
        {
            if (selectedLanguages == null)
            {
                countriesToUpdate.CountryLanguages = new List<CountryLanguage>();
                return;
            }

            var selectedLanguageHS = new HashSet<string>(selectedLanguages);
            var countryLanguages = new HashSet<int>(countriesToUpdate.CountryLanguages.Select(c => c.Language.LanguageId));

            foreach (var language in _context.Languages)
            {
                if (selectedLanguageHS.Contains(language.LanguageId.ToString()))
                {
                    if (!countryLanguages.Contains(language.LanguageId))
                    {
                        countriesToUpdate.CountryLanguages.Add(new CountryLanguage { CountryId = countriesToUpdate.CountryId, LanguageId = language.LanguageId });
                    }
                }
                else
                {
                    if (countryLanguages.Contains(language.LanguageId))
                    {
                        CountryLanguage languageToRemove = countriesToUpdate.CountryLanguages.FirstOrDefault(i => i.LanguageId == language.LanguageId);
                        _context.Remove(languageToRemove);
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

            var contry = await _context.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (contry == null)
            {
                return NotFound();
            }

            return View(contry);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Country contry = await _context.Countries
                .Include(i => i.CountryLanguages)
                .SingleAsync(i => i.CountryId == id);


            _context.Countries.Remove(contry);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
