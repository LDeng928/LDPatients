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
    public class LDCountryController : Controller
    {
        private readonly PatientsContext _context;

        public LDCountryController(PatientsContext context)
        {
            _context = context;
        }

        // GET: LDCountry
        // This get method will retrive the data from table country and convert the data into a list using ToListAsync() method.
        public async Task<IActionResult> Index()
        {
            return View(await _context.Country.ToListAsync());
        }

        // GET: LDCountry/Details/5
        // The Details() action will display one single country information. Using if statement, if the id is null, program will return NotFound().
        // If the context in country not return null, the program will return the country context from the Country model.
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country
                .FirstOrDefaultAsync(m => m.CountryCode == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: LDCountry/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LDCountry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // This Create() Post method passing 2 parameters, the first one is the binding attributes from the Country model, which are "CountryCode,Name,PostalPattern,PhonePattern,FederalSalesTax".
        // The second parameter is the Country model, which create a new entry.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryCode,Name,PostalPattern,PhonePattern,FederalSalesTax")] Country country)
        {
            // Using if statement to check if the model attribtes are valid
            if (ModelState.IsValid)
            {
                // If attributes are valid, the data will be added to context
                _context.Add(country);
                await _context.SaveChangesAsync(); // Saves changes to the context
                return RedirectToAction(nameof(Index)); // Once saved, return to the Index view
            }
            return View(country);
        }

        // GET: LDCountry/Edit/5
        public async Task<IActionResult> Edit(string id) // Passing in the string id parameter
        {
            if (id == null) // Check if the id is null, if it is null, return not found.
            {
                return NotFound();
            }
            // Use the id to find the context in the Country model. If it is null, return not found. If not, return the country context.
            var country = await _context.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: LDCountry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // This Edit post method is passing in two parameters. The first one is the selected string id and the second on the binding attributes of the Country model.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CountryCode,Name,PostalPattern,PhonePattern,FederalSalesTax")] Country country)
        {
            // Check if id equals the CountryCode from context. If not, return not found.
            if (id != country.CountryCode)
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
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                // Catch the exception if the update in the database throws an exception. 
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryCode)) // If the country code does not exist in the database, return not found.
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
            return View(country); // Return the data context 
        }

        // GET: LDCountry/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            // Check if the id exists in the Country database. If not, return not found. 
            if (id == null)
            {
                return NotFound();
            }
            // Assign a variable from context 
            var country = await _context.Country.FirstOrDefaultAsync(m => m.CountryCode == id);
            if (country == null)
            {
                return NotFound(); //If it is null, return not found
            }
            // If it is true, return country view
            return View(country);
        }

        // POST: LDCountry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) //Post delete method passing a string id as a parameter
        {
            // Find the id and remove it from the context and database, then save. Return to index
            var country = await _context.Country.FindAsync(id);
            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Private method to check if the id exists
        private bool CountryExists(string id)
        {
            return _context.Country.Any(e => e.CountryCode == id);
        }
    }
}
