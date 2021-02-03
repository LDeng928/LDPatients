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
    public class LDDispensingUnitController : Controller
    {
        private readonly PatientsContext _context;

        public LDDispensingUnitController(PatientsContext context)
        {
            _context = context;
        }

        // GET: LDDispensingUnit
        // This get method will retrive the data from table and convert the data into a list using ToListAsync() method.
        public async Task<IActionResult> Index()
        {
            return View(await _context.DispensingUnit.ToListAsync());
        }

        // GET: LDDispensingUnit/Details/5
        // The Details() action will display one single information. Using if statement, if the id is null, program will return NotFound().
        // If the context not return null, the program will return the  context from the model.
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispensingUnit = await _context.DispensingUnit
                .FirstOrDefaultAsync(m => m.DispensingCode == id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }

            return View(dispensingUnit);
        }

        // GET: LDDispensingUnit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LDDispensingUnit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        // This Create() Post method passing 1 parameter, which is the binding attributes from the model. This will create a new entry.
        public async Task<IActionResult> Create([Bind("DispensingCode")] DispensingUnit dispensingUnit)         
        {
            // Using if statement to check if the model attribtes are valid
            if (ModelState.IsValid)
            {
                // If attributes are valid, the data will be added to context and be saved.
                _context.Add(dispensingUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Once saved, return to the Index action
            }
            return View(dispensingUnit);
        }

        // GET: LDDispensingUnit/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) // Check if the id is null, if it is null, return not found.
            {
                return NotFound();
            }
            // Use the id to find the context in the model. If it is null, return not found. If not, return the context.
            var dispensingUnit = await _context.DispensingUnit.FindAsync(id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }
            return View(dispensingUnit);
        }

        // POST: LDDispensingUnit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // This Edit post method is passing in two parameters. The first one is the selected string id and the second on the binding attributes of the model.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DispensingCode")] DispensingUnit dispensingUnit)
        {
            // Check if id equals the one from context. If not, return not found.
            if (id != dispensingUnit.DispensingCode)
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
                    _context.Update(dispensingUnit);
                    await _context.SaveChangesAsync();
                }
                // Catch the exception if the update in the database throws an exception.
                catch (DbUpdateConcurrencyException)
                {
                    if (!DispensingUnitExists(dispensingUnit.DispensingCode)) // If the dispensingUnit does not exist in the database, return not found.
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
            return View(dispensingUnit); // Return the data context 
        }

        // GET: LDDispensingUnit/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            // Check if the id exists in the DispensingUnit table. If not, return not found.
            if (id == null)
            {
                return NotFound(); //If it is null, return not found
            }
            // Assign a variable from context 
            var dispensingUnit = await _context.DispensingUnit
                .FirstOrDefaultAsync(m => m.DispensingCode == id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }
            // If it is true, return dispensingUnit view
            return View(dispensingUnit);
        }

        // POST: LDDispensingUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) //Post delete method passing a string id as a parameter
        {
            // Find the id and remove it from the context and database, then save. Return to action index
            var dispensingUnit = await _context.DispensingUnit.FindAsync(id);
            _context.DispensingUnit.Remove(dispensingUnit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Private method to check if the id exists
        private bool DispensingUnitExists(string id)
        {
            return _context.DispensingUnit.Any(e => e.DispensingCode == id);
        }
    }
}
