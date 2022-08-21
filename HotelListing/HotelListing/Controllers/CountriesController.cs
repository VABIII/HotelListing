using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Data;
using Microsoft.AspNetCore.Authorization;
using HotelListing.DTOs.CountryDTO;
using AutoMapper;

// Controller is what recieves the request, then runs a process, and finally returns a response with data from said process

namespace HotelListing.Controllers
{
    [Route("api/[controller]")] // This our api route
    [ApiController] // Affix the 'attribute' ApiController 
    public class CountriesController : ControllerBase  // Our 'CountriesController' inherits from the 'ControllerBase' 
    {
        // This creates a private field/variable that should be equated to whatever we are 'injecting'
        // This way, we don't have to declare a new instance of the db context in each new class. Instead it can simply be 'injected'
        private readonly HotelListingDbContext _context; // The leading underscore, _ , denotes a private field/variable
        private readonly IMapper _mapper;

        // Here, we have the 'CountriesController' 'constructor' and we pass it in our datatype and give it a name, which in this case is the db context
        public CountriesController(HotelListingDbContext context, IMapper _mapper)
        {
            _context = context;  // Right out of the box, we are 'injecting' our database 'context' directly into the controller. This comes from the context setup in the Program.cs file
            this._mapper = _mapper; // This allows us to inject our data type mapper into our file
        }

        // The below section is referred to as an 'Action' - the part of the code that actually performs the process dictated by the controller

        // GET: api/Countries 
        [HttpGet] // Defines the HTTP CRUD method required to access this endpoint
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries() // The func, GetCountries, has a return type of IEnumerable, a 'collection' type, which will return an array of all the countries even it is just one. If there are zero countries then it will return an empty array, [].
        {
            // We are returning the query results of a SQL query targeting the table 'Countries'
            // This is the same as "SELECT * FROM [Countries]"
            return await _context.Countries.ToListAsync();

            // You could also wrap the response in the Ok() func which will have response status of 200
            // var countries = await _context.Countries.ToListAsync(); - Not necessarily required, but conforms with C# conventions
            //return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id); // The FindAsync() func finds the entity with the given primary key values. If an entity with the given PK values
                                                                  // is being tracked by the context, then it is return immediately with making db call. Otherwise, a query is sent to db, and if found it   
            if (country == null)                                  // then attached to the context and returned
            {
                
                return NotFound(); // Creates NotFoundResult that carries a 404 error
            }

            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AllowAnonymous]
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

        /* [HttpPost]
         [AllowAnonymous]
         public async Task<ActionResult<Country>> PostCountry(Country country)  // This method is an 'Action' which returns an 'ActionReult' object of type 'Country' and accepts a 
         {                                                                      // parameter called 'country' of type 'Country' 
             _context.Countries.Add(country);  // Since '_context' contains a copy of our db context, this will go to the 'Country' table and add our parameter 'country' to the table
             await _context.SaveChangesAsync(); // We then save the changes we just made

             // Returns a CreatedAtActionResult. The first parameter, 'GetCountry', is the url to retrieve this new record with that id and data
             // The response header will contain the full URL for the endpoint that can be used to retrieve the newly add country
             return CreatedAtAction("GetCountry", new { id = country.Id }, country);
         }*/

        /*        // This is the method used without using an autoMapper that maps between different data types created in the 'MapConfig' class
                [HttpPost]
                [AllowAnonymous]
                public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO createCountry)  
                {
                    var country = new Country
                    {
                        Name = createCountry.Name,
                        ShortName = createCountry.ShortName,
                    };

                    _context.Countries.Add(country);  
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetCountry", new { id = country.Id }, country);
                }
        */

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO createCountryDTO)
        {
            var country = _mapper.Map<Country>(createCountryDTO); // Creates an object named 'country' of type 'Country' and maps the data from the 'createCountryDTO' onto itself

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
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
