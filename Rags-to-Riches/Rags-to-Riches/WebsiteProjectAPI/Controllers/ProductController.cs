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
    public class ProductController : ControllerBase
    {
        private readonly RagsToRichesDbContext _context;

        public ProductController(RagsToRichesDbContext context)
        {
            _context = context;
        }

        private static ProductForView ProductForViewFromProduct(Product p)
        {
            return new ProductForView()
            {
                ProductId = p.ProductId,
                Description = p.Description,
                Cost = p.Cost,
                IsActive = p.IsActive,
                SizesId = p.SizesId,
                CategoriesId = p.CategoriesId,
                SizeName = p.Sizes.Size1,
                CategoryName = p.Categories?.Name ?? "No Category name",
                Images = p.Images.Select(i => new ImageForView()
                {
                    ImageId = i.ImageId,
                    ProductId = i.ProductId,
                    Image1 = i.Image1
                }).ToList(),
                OrderProducts = p.OrderProducts.Select(op => new OrderProductForView()
                {
                    OrderId = op.OrderId,
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    Price = op.Price,
                    OrderProductsId = op.OrderProductsId,
                }).ToList()
            };
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductForView>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return (await _context.Products.Include(c => c.Categories)
                .Include(s => s.Sizes)
                .Include(i => i.Images).ToListAsync())
                .Select(p => ProductForViewFromProduct(p)).ToList();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductForView>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Include(c => c.Categories)
                .Include(s => s.Sizes)
                .Include(i => i.Images).FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return ProductForViewFromProduct(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductForView product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var productToEdit = await _context.Products.FindAsync(id);
            productToEdit.ProductId = product.ProductId;
            productToEdit.Description = product.Description;
            productToEdit.Cost = product.Cost;
            productToEdit.IsActive = product.IsActive;
            productToEdit.SizesId = product.SizesId ?? 1;
            productToEdit.CategoriesId = product.CategoriesId;


            _context.Entry(productToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductForView>> PostProduct(ProductForView product)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'RagsToRichesDbContext.Products'  is null.");
            }
            var productToAdd = new Product()
            {
                ProductId = product.ProductId,
                Description = product.Description,
                Cost = product.Cost,
                IsActive = product.IsActive,
                SizesId = product.SizesId ?? 1,
                CategoriesId = product.CategoriesId,
            };

            _context.Products.Add(productToAdd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok(product);
            //return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
