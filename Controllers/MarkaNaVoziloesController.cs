using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VoziloKT.Models;

namespace VoziloKT.Controllers
{
    public class MarkaNaVoziloesController : Controller
    {
        private readonly voziloDBContext _context;

        public MarkaNaVoziloesController(voziloDBContext context)
        {
            _context = context;
        }

        // GET: MarkaNaVoziloes
        //This method is used to display the vehicle models
        public async Task<IActionResult> Index(string sortByModel,string filterByModel)
        {
            ViewData["ModelSortParm"] = String.IsNullOrEmpty(sortByModel) ? "model_desc" : "";
            ViewData["CurrentFilter"] = filterByModel;
            var markaNaVozilo = from s in _context.MarkaNaVozilo
                    select s;
            //Depending on the sortByModel and filterByModel values the return statement collects the data for the view
            if (!String.IsNullOrEmpty(filterByModel))
            {
                markaNaVozilo = markaNaVozilo.Where(s => s.VehicleModel.Contains(filterByModel));
                                       
            }
            if (sortByModel == "model_desc")
                markaNaVozilo = markaNaVozilo.OrderByDescending(s => s.VehicleModel);

            
            return View(await markaNaVozilo.AsNoTracking().ToListAsync());
           
        }

        // GET: MarkaNaVoziloes/Details/5
        //Shows details for the selected record
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var markaNaVozilo = await _context.MarkaNaVozilo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (markaNaVozilo == null)
            {
                return NotFound();
            }

            return View(markaNaVozilo);
        }

        // GET: MarkaNaVoziloes/Create
        //Opens view for creating new vehicle model
        public IActionResult Create()
        {
            return View();
        }

        // POST: MarkaNaVoziloes/Create
        //Adds new vehicle model using validation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleModel")] MarkaNaVozilo markaNaVozilo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(markaNaVozilo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(markaNaVozilo);
        }

        // GET: MarkaNaVoziloes/Edit/5
        //Opens view for editing the selected record
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var markaNaVozilo = await _context.MarkaNaVozilo.FindAsync(id);
            if (markaNaVozilo == null)
            {
                return NotFound();
            }
            return View(markaNaVozilo);
        }

        // POST: MarkaNaVoziloes/Edit/5
        //Makes changes to the selected record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleModel")] MarkaNaVozilo markaNaVozilo)
        {
            if (id != markaNaVozilo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(markaNaVozilo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarkaNaVoziloExists(markaNaVozilo.Id))
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
            return View(markaNaVozilo);
        }

        // GET: MarkaNaVoziloes/Delete/5
        //Opens view for deleting the selected record
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var markaNaVozilo = await _context.MarkaNaVozilo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (markaNaVozilo == null)
            {
                return NotFound();
            }

            return View(markaNaVozilo);
        }

        // POST: MarkaNaVoziloes/Delete/5
        //Deletes the selected record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var markaNaVozilo = await _context.MarkaNaVozilo.FindAsync(id);
            _context.MarkaNaVozilo.Remove(markaNaVozilo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //This method is used to check in that model of vehicle exists
        private bool MarkaNaVoziloExists(int id)
        {
            return _context.MarkaNaVozilo.Any(e => e.Id == id);
        }
    }
}
