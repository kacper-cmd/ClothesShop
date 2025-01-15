using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using WebsiteProjectAPI.ViewModel;

namespace WebsiteProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly RagsToRichesDbContext _context;

        public AddressesController(RagsToRichesDbContext context)
        {
            _context = context;
        }
        private static AddressForView AddressForViewFromAddress(Address a)
        {
            return new AddressForView
            {
                HomeNumber = a.HomeNumber,
                Street = a.Street,
                City = a.City,
                ZipCode = a.ZipCode,
                Country = a.Country,
                AddressesId = a.AddressesId
            };
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressForView>>> GetAddresses()
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }
            return (await _context.Addresses.ToListAsync())
                .Select(a => AddressForViewFromAddress(a)).ToList();
        }



        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressForView>> GetAddress(int id)
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.AddressesId == id);

            if (address == null)
            {
                return NotFound();
            }

            return AddressForViewFromAddress(address);
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, AddressForView address)
        {
            if (id != address.AddressesId)
            {
                return BadRequest();
            }

            var addressToEdit = await _context.Addresses.FindAsync(id);
            addressToEdit.HomeNumber = address.HomeNumber;
            addressToEdit.Street = address.Street;
            addressToEdit.City = address.City;
            addressToEdit.ZipCode = address.ZipCode;
            addressToEdit.Country = address.Country;
            //addressToEdit.AddressesId = id;

            _context.Entry(addressToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddressForView>> PostAddress(AddressForView address)
        {
            if (_context.Addresses == null)
            {
                return Problem("Entity set 'RagsToRichesDbContext.Addresses'  is null.");
            }

            var addressToAdd = new Address
            {
                HomeNumber = address.HomeNumber,
                Street = address.Street,
                City = address.City,
                ZipCode = address.ZipCode,
                Country = address.Country,
                AddressesId = address.AddressesId
            };

            _context.Addresses.Add(addressToAdd);
            await _context.SaveChangesAsync();

            return Ok(address); //produce 200 response
            /*try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AddressExists(address.AddressesId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAddress", new { id = address.AddressesId }, address);*/
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Addresses?.Any(e => e.AddressesId == id)).GetValueOrDefault();
        }
    }
}
