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
    public class LDConcentrationUnitController : Controller
    {
        private readonly PatientsContext _context;

        public LDConcentrationUnitController(PatientsContext context)
        {
            _context = context;
        }

        // GET: LDConcentrationUnit
        // This get method will retrive the data from table and convert the data into a list using ToListAsync() method.
        public async Task<IActionResult> Index()
        {
            return View(await _context.ConcentrationUnit.ToListAsync());
        }

        // GET: LDConcentrationUnit/Details/5
        // The Details() action will display one single information. Using if statement, if the id is null, program will return NotFound().
        // If the context not return null, the program will return the  context from the model.
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnit
                .FirstOrDefaultAsync(m => m.ConcentrationCode == id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }

            return View(concentrationUnit);
        }

        // GET: LDConcentrationUnit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LDConcentrationUnit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // This Create() Post method passing 1 parameter, which is the binding attributes from the model. This will create a new entry.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConcentrationCode")] ConcentrationUnit concentrationUnit)
        {
            // Using if statement to check if the model attribtes are valid
            if (ModelState.IsValid)
            {
                // If attributes are valid, the data will be added to context
                _context.Add(concentrationUnit);
                await _context.SaveChangesAsync(); // Saves changes to the context
                return RedirectToAction(nameof(Index)); // Once saved, return to the Index view
            }
            return View(concentrationUnit);
        }

        // GET: LDConcentrationUnit/Edit/5
        public async Task<IActionResult> Edit(string id) // Passing in the string id parameter
        {
            if (id == null) // Check if the id is null, if it is null, return not found.
            {
                return NotFound();
            }

            // Use the id to find the context in the model. If it is null, return not found. If not, return the context.
            var concentrationUnit = await _context.ConcentrationUnit.FindAsync(id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }
            return View(concentrationUnit);
        }

        // POST: LDConcentrationUnit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // This Edit post method is passing in two parameters. The first one is the selected string id and the second on the binding attributes of the model.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ConcentrationCode")] ConcentrationUnit concentrationUnit)
        {
            // Check if id equals the one from context. If not, return not found.
            if (id != concentrationUnit.ConcentrationCode)
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
                    _context.Update(concentrationUnit);
                    await _context.SaveChangesAsync();
                }
                // Catch the exception if the update in the database throws an exception.
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcentrationUnitExists(concentrationUnit.ConcentrationCode)) // If the ConcentrationUnit does not exist in the database, return not found.
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Return to the view Index
            }
            return View(concentrationUnit); // Return the data context 
        }

        // GET: LDConcentrationUnit/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            // Check if the id exists in the Country database. If not, return not found.
            if (id == null)
            {
                return NotFound(); //If it is null, return not found
            }
            // Assign a variable from context 
            var concentrationUnit = await _context.ConcentrationUnit
                .FirstOrDefaultAsync(m => m.ConcentrationCode == id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }
            // If it is true, return concentrationUnit view
            return View(concentrationUnit);
        }

        // POST: LDConcentrationUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) //Post delete method passing a string id as a parameter
        {
            // Find the id and remove it from the context and database, then save. Return to index
            var concentrationUnit = await _context.ConcentrationUnit.FindAsync(id); 
            _context.ConcentrationUnit.Remove(concentrationUnit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Private method to check if the id exists
        private bool ConcentrationUnitExists(string id)
        {
            return _context.ConcentrationUnit.Any(e => e.ConcentrationCode == id);
        }
    }
}
