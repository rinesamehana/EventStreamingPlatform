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
    public class RecomandationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecomandationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recomandations
        public async Task<IActionResult> Index()
        {
            var appContext = _context.Recomandations.Include(d => d.Filmm);
            return View(await appContext.ToListAsync());
        }

        // GET: Recomandations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string query = "SELECT * FROM Recomandation WHERE RecomandationId = {0}";
            var recomadation = await _context.Recomandations
                .FromSqlRaw(query, id)
                .Include(d => d.Filmm)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (recomadation == null)
            {
                return NotFound();
            }

            return View(recomadation);
        }

        // GET: Recomandations/Create
        public IActionResult Create()
        {
            ViewData["FilmId"] = new SelectList(_context.Films, "ID", "Title");
            return View();
        }

        // POST: Recomandations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecomandationId,Name,Age,Desc,FilmId,RowVersion")] Recomandation recomandation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recomandation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmId"] = new SelectList(_context.Films, "ID", "Title", recomandation.FilmId);
            return View(recomandation);
        }

        // GET: Recomandations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomandation = await _context.Recomandations
                .Include(i => i.Filmm)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RecomandationId == id);

            if (recomandation == null)
            {
                return NotFound();
            }
            ViewData["FilmId"] = new SelectList(_context.Films, "ID", "Title", recomandation.FilmId);
            return View(recomandation);
        }

        // POST: Recomandations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, byte[] rowVersion)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomandationToUpdate = await _context.Recomandations.Include(i => i.Filmm).FirstOrDefaultAsync(m => m.RecomandationId == id);

            if (recomandationToUpdate == null)
            {
                Recomandation deletedRecomandation = new Recomandation();
                await TryUpdateModelAsync(deletedRecomandation);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The recomandation was deleted by another user.");
                ViewData["FilmId"] = new SelectList(_context.Films, "ID", "Title", deletedRecomandation.FilmId);
                return View(deletedRecomandation);
            }

            _context.Entry(recomandationToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<Recomandation>(
                recomandationToUpdate,
                "",
                s => s.Name, s => s.Age, s => s.Desc, s => s.FilmId))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Recomandation)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The recomandation was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Recomandation)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}");
                        }
                        if (databaseValues.Age != clientValues.Age)
                        {
                            ModelState.AddModelError("Age", $"Current value: {databaseValues.Age:c}");
                        }
                        if (databaseValues.Desc != clientValues.Desc)
                        {
                            ModelState.AddModelError("Desc", $"Current value: {databaseValues.Desc:d}");
                        }
                        if (databaseValues.FilmId != clientValues.FilmId)
                        {
                            Film databaseFilm = await _context.Films.FirstOrDefaultAsync(i => i.ID == databaseValues.FilmId);
                            ModelState.AddModelError("FilmId", $"Current value: {databaseFilm?.Title}");
                        }

                        ModelState.AddModelError(string.Empty, @"The record you attempted to edit 
                                was modified by another user after you got the original value. The 
                                edit operation was canceled and the current values in the database 
                                have been displayed. If you still want to edit this record, click 
                                the Save button again. Otherwise click the Back to List hyperlink.");
                        recomandationToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            ViewData["FilmId"] = new SelectList(_context.Films, "ID", "Title", recomandationToUpdate.FilmId);
            return View(recomandationToUpdate);
        }

        // GET: Recomandations/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomandation = await _context.Recomandations
                .Include(d => d.Filmm)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RecomandationId == id);
            if (recomandation == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ConcurrencyErrorMessage"] = @"The record you attempted to delete 
                        was modified by another user after you got the original values. 
                        The delete operation was canceled and the current values in the 
                        database have been displayed. If you still want to delete this 
                        record, click the Delete button again. Otherwise 
                        click the Back to List hyperlink.";
            }

            return View(recomandation);
        }
        // POST: Recomandations/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Recomandation recomandation)
        {
            try
            {
                if (await _context.Recomandations.AnyAsync(m => m.RecomandationId == recomandation.RecomandationId))
                {
                    _context.Recomandations.Remove(recomandation);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { concurrencyError = true, id = recomandation.RecomandationId });
            }
        }

        private bool RecomandatioExists(int id)
        {
            return _context.Recomandations.Any(e => e.RecomandationId == id);
        }
    }
}