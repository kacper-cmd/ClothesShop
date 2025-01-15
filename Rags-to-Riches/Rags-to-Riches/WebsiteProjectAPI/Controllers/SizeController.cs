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
    public class SizeController : ControllerBase
    {
        private readonly RagsToRichesDbContext _context;

        public SizeController(RagsToRichesDbContext context)
        {
            _context = context;
        }

        // GET: api/Size
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SizeForView>>> GetSizes()
        {
            var sizes = await _context.Sizes.Include(s => s.Products).ToListAsync();
            var sizesForView = sizes.Select(size => new SizeForView
            {
                SizesId = size.SizesId,
                Size1 = size.Size1,
                //Products = size.Products.Select(p => new ProductForView
                //{
                //    ProductId = p.ProductId,
                //    Description = p.Description,
                //    Cost = p.Cost,
                //    IsActive = p.IsActive,
                //    SizesId = p.SizesId,
                //    CategoriesId = p.CategoriesId,
                //    SizeName = p.Sizes?.Size1,
                //    CategoryName = p.Categories?.Name,
                //    Images = p.Images.Select(i => new ImageForView
                //    {
                //        // Set properties for ImageForView
                //    }).ToList(),
                //    OrderProducts = p.OrderProducts.Select(op => new OrderProductForView
                //    {
                //        // Set properties for OrderProductForView
                //    }).ToList()
                //}).ToList()
            });

            return Ok(sizesForView);
        }

        // GET: api/Size/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SizeForView>> GetSize(int id)
        {
            var size = await _context.Sizes.Include(s => s.Products).FirstOrDefaultAsync(s => s.SizesId == id);
            if (size == null)
            {
                return NotFound();
            }

            var sizeForView = new SizeForView
            {
                SizesId = size.SizesId,
                Size1 = size.Size1
                //Products = size.Products.Select(p => new ProductForView
                //{
                //    ProductId = p.ProductId,
                //    Description = p.Description,
                //    Cost = p.Cost,
                //    IsActive = p.IsActive,
                //    SizesId = p.SizesId,
                //    CategoriesId = p.CategoriesId,
                //    SizeName = p.Sizes?.Size1,
                //    CategoryName = p.Categories?.Name,
                //    Images = p.Images.Select(i => new ImageForView
                //    {
                //        // Set properties for ImageForView
                //    }).ToList(),
                //    OrderProducts = p.OrderProducts.Select(op => new OrderProductForView
                //    {
                //        // Set properties for OrderProductForView
                //    }).ToList()
                //}).ToList()
            };

            return Ok(sizeForView);
        }

        // PUT: api/Size/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSize(int id, SizeForView sizeForView)
        {
            if (id != sizeForView.SizesId)
            {
                return BadRequest();
            }

            var size = await _context.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            size.Size1 = sizeForView.Size1;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SizeExists(id))
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

        // POST: api/Size
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SizeForView>> PostSize(SizeForView sizeForView)
        {
            var size = new Size
            {
                SizesId = sizeForView.SizesId,
                Size1 = sizeForView.Size1
            };

            _context.Sizes.Add(size);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Check if a Size with the same Size1 already exists
                if (_context.Sizes.Any(s => s.Size1 == size.Size1))
                {
                    return Conflict("A Size with the same name already exists.");
                }
                else
                {
                    throw;
                }
            }

            // Update the SizesId property in sizeForView
           // sizeForView.SizesId = size.SizesId;

            return Ok(sizeForView);

        }

        // DELETE: api/Size/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSize(int id)
        {
            if (_context.Sizes == null)
            {
                return NotFound();
            }
            var size = await _context.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SizeExists(int id)
        {
            return (_context.Sizes?.Any(e => e.SizesId == id)).GetValueOrDefault();
        }
    }
}
