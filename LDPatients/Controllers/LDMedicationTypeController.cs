using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LDPatients.Models;

namespace LDPatients.Controllers
{
    public class LDMedicationTypeController : Controller
    {
        private readonly PatientsContext _context;

        public LDMedicationTypeController(PatientsContext context)
        {
            _context = context;
        }

        // GET: LDMedicationType
        public async Task<IActionResult> Index()
        {
            return View(await _context.MedicationType.ToListAsync());
        }

        // GET: LDMedicationType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationType
                .FirstOrDefaultAsync(m => m.MedicationTypeId == id);
            if (medicationType == null)
            {
                return NotFound();
            }

            return View(medicationType);
        }

        // GET: LDMedicationType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LDMedicationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicationType);
        }

        // GET: LDMedicationType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationType.FindAsync(id);
            if (medicationType == null)
            {
                return NotFound();
            }
            return View(medicationType);
        }

        // POST: LDMedicationType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            if (id != medicationType.MedicationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationTypeExists(medicationType.MedicationTypeId))
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
            return View(medicationType);
        }

        // GET: LDMedicationType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationType
                .FirstOrDefaultAsync(m => m.MedicationTypeId == id);
            if (medicationType == null)
            {
                return NotFound();
            }

            return View(medicationType);
        }

        // POST: LDMedicationType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicationType = await _context.MedicationType.FindAsync(id);
            _context.MedicationType.Remove(medicationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicationTypeExists(int id)
        {
            return _context.MedicationType.Any(e => e.MedicationTypeId == id);
        }
    }
}
