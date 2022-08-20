using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Data;

// Controller is what recieves the request, then runs a process, and finally returns a response with data from said process

namespace HotelListing.Controllers
{
    [Route("api/[controller]")] // This our api route
    [ApiController] // Affix the 'attribute' ApiController 
    public class CountriesController : ControllerBase  // Our 'CountriesController' inherits from the 'ControllerBase' 
    {
        // This creates a private field/variable that should be equated to whatever we are 'injecting'
        // This way, we don't have to declare a new instance of the db context in each new class. Instead it can simply be 'injected'
        private readonly HotelListingDbContext _context; // 

        // Here, we have the 'CountriesController' 'constructor' and we pass it in our datatype and give it a name, which in this case is the db context
        public CountriesController(HotelListingDbContext context)  
        {
            _context = context;  // Right out of the box, we are 'injecting' our database 'context' directly into the controller. This comes from the context setup in the Program.cs file
        }

        // The below section is referred to as an 'Action' - the part of the code that actually performs the process dictated by the controller

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
