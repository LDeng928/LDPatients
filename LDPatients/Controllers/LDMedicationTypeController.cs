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
        // This get method will retrive the data from table and convert the data into a list using ToListAsync() method.
        public async Task<IActionResult> Index()
        {
            return View(await _context.MedicationType.ToListAsync());
        }

        // GET: LDMedicationType/Details/5
        // The Details() action will display one single information. Using if statement, if the id is null, program will return NotFound().
        // If the context not return null, the program will return the  context from the model.
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

        // This Create() Post method passing 1 parameter, which is the binding attributes from the model. This will create a new entry.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            // Using if statement to check if the model attribtes are valid
            if (ModelState.IsValid)
            {
                // If attributes are valid, the data will be added to context and be saved.
                _context.Add(medicationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Once saved, return to the Index action
            }
            return View(medicationType);
        }

        // GET: LDMedicationType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) // Check if the id is null, if it is null, return not found.
            {
                return NotFound();
            }
            // Use the id to find the context in the model. If it is null, return not found. If not, return the context.
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

        // This Edit post method is passing in two parameters. The first one is the selected int id and the second on the binding attributes of the model.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            // Check if id equals the one from context. If not, return not found.
            if (id != medicationType.MedicationTypeId)
            {
                return NotFound();
            }
            // Check if the model state is valid. Are all the attributes valid.
            if (ModelState.IsValid)
            {
                // If valid, save the changes and update the database.
                // Use try and catch 
                try
                {
                    _context.Update(medicationType);
                    await _context.SaveChangesAsync();
                }
                // Catch the exception if the update in the database throws an exception.
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationTypeExists(medicationType.MedicationTypeId)) // If the medicationType does not exist in the database, return not found.
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Return to the action Index
            }
            return View(medicationType); // Return the data context 
        } 

        // GET: LDMedicationType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the id exists in the MedicationType table. If not, return not found.
            if (id == null)
            {
                return NotFound(); //If it is null, return not found
            }
            // Assign a variable from context 
            var medicationType = await _context.MedicationType
                .FirstOrDefaultAsync(m => m.MedicationTypeId == id);
            if (medicationType == null)
            {
                return NotFound();
            }
            // If it is true, return dispensingUnit view
            return View(medicationType);
        }

        // POST: LDMedicationType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)  //Post delete method passing a int id as a parameter
        {
            // Find the id and remove it from the context and database, then save. Return to action index
            var medicationType = await _context.MedicationType.FindAsync(id);
            _context.MedicationType.Remove(medicationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Private method to check if the id exists
        private bool MedicationTypeExists(int id)
        {
            return _context.MedicationType.Any(e => e.MedicationTypeId == id);
        }
    }
}
