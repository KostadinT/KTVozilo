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
    public class DataForVehiclesController : Controller
    {
        private readonly voziloDBContext _context;

        public DataForVehiclesController(voziloDBContext context)
        {
            _context = context;
        }

        // GET: DataForVehicles
        //This method is used to display and manipulate data from the list of vehicles
        public async Task<IActionResult> Index(string sortByModel, string filterByModel)
        {
            ViewData["ModelSortParm"] = String.IsNullOrEmpty(sortByModel) ? "model_desc" : "";
            ViewData["CurrentFilter"] = filterByModel;
            //Depending on the sortByModel and filterByModel values the return statement collects the data for the view
            var voziloDBContext = from s in _context.DataForVehicles.Include(d => d.VehicleModelNavigation) select s;
            if (!String.IsNullOrEmpty(filterByModel))
            {
                voziloDBContext = voziloDBContext.Where(s => s.VehicleModelNavigation.VehicleModel.Contains(filterByModel));

            }
            if (sortByModel == "model_desc")
                voziloDBContext= voziloDBContext.OrderByDescending(s => s.VehicleModelNavigation.VehicleModel);

            return View(await voziloDBContext.AsNoTracking().ToListAsync());
        }

        //GET: DataForVehicles/Print
        public async Task<IActionResult> Print()
        {
            //selects all data from DataForVehicles table
            var voziloDBContext = _context.DataForVehicles.Include(d => d.VehicleModelNavigation);
            return View(await voziloDBContext.ToListAsync());
        }

        // GET: DataForVehicles/Details/5
        //Shows details for the selected record
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataForVehicles = await _context.DataForVehicles
                .Include(d => d.VehicleModelNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataForVehicles == null)
            {
                return NotFound();
            }

            return View(dataForVehicles);
        }

        // GET: DataForVehicles/Create
        //Opens view for input data for new data for a vehicle
        public IActionResult Create()
        {
            ViewData["VehicleModel"] = new SelectList(_context.MarkaNaVozilo, "VehicleModel", "VehicleModel");
            return View();
        }

        // POST: DataForVehicles/Create
        //Creates new data for a vehicle

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LicensePlateNumber,Vin,VehicleModel")] DataForVehicles dataForVehicles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataForVehicles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleModel"] = new SelectList(_context.MarkaNaVozilo, "VehicleModel", "VehicleModel", dataForVehicles.VehicleModel);
            return View(dataForVehicles);
        }

        // GET: DataForVehicles/Edit/5
        //Opens view for editing the selected record
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataForVehicles = await _context.DataForVehicles.FindAsync(id);
            if (dataForVehicles == null)
            {
                return NotFound();
            }
            ViewData["VehicleModel"] = new SelectList(_context.MarkaNaVozilo, "VehicleModel", "VehicleModel", dataForVehicles.VehicleModel);
            return View(dataForVehicles);
        }

        // POST: DataForVehicles/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Makes changes to the selected record
        public async Task<IActionResult> Edit(int id, [Bind("Id,LicensePlateNumber,Vin,VehicleModel")] DataForVehicles dataForVehicles)
        {
            if (id != dataForVehicles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataForVehicles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataForVehiclesExists(dataForVehicles.Id))
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
            ViewData["VehicleModel"] = new SelectList(_context.MarkaNaVozilo, "VehicleModel", "VehicleModel", dataForVehicles.VehicleModel);
            return View(dataForVehicles);
        }

        // GET: DataForVehicles/Delete/5
        //Opens view for deleting the selected record
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataForVehicles = await _context.DataForVehicles
                .Include(d => d.VehicleModelNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataForVehicles == null)
            {
                return NotFound();
            }

            return View(dataForVehicles);
        }

        // POST: DataForVehicles/Delete/5
        //Deletes the selected record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataForVehicles = await _context.DataForVehicles.FindAsync(id);
            _context.DataForVehicles.Remove(dataForVehicles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Checks if such vehicle exists
        private bool DataForVehiclesExists(int id)
        {
            return _context.DataForVehicles.Any(e => e.Id == id);
        }
    }
}
