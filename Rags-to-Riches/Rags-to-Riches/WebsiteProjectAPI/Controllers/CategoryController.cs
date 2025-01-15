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
    public class CategoryController : ControllerBase
    {
        private readonly RagsToRichesDbContext _context;

        public CategoryController(RagsToRichesDbContext context)
        {
            _context = context;
        }
        private static CategoryForView CategoryForViewFromCategory(Category c)
        {
            return new CategoryForView
            {
                CategoriesId = c.CategoriesId,
                Name = c.Name,
                IsActive = c.IsActive,
                /*Products = c.Products.Select(p => new ProductForView()
                {
                    Description = p.Description,
                    Cost = p.Cost,
                    SizesId = p.SizesId,
                    CategoriesId = p.CategoriesId,
                    SizeName = p.Sizes.Size1,
                    CategoryName = p.Categories?.Name ?? "No Category",
                    Images = p.Images.Select(i => new ImageForView()
                    {
                        ImageId = i.ImageId,
                        ProductId = i.ProductId,
                        Image1 = i.Image1
                    }).ToList()
                }).ToList(),*/
            };
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryForView>>> GetCategories()
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            return (await _context.Categories.Include(p => p.Products)
                .ThenInclude(i => i.Images)
                /*.Include(p => p.Products).ThenInclude(s => s.Sizes)*/.ToListAsync())
                .Select(c => CategoryForViewFromCategory(c)).Where(c=>c.IsActive == true).ToList();
            /*return (await _context.Categories.ToListAsync())
                .Select(c => CategoryForViewFromCategory(c)).ToList();*/
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryForView>> GetCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.Include(p => p.Products)
                .ThenInclude(i => i.Images)
                .Include(p => p.Products).ThenInclude(s => s.Sizes).FirstOrDefaultAsync(c => c.CategoriesId == id);

            if (category == null)
            {
                return NotFound();
            }

            return CategoryForViewFromCategory(category);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryForView category)
        {
            if (id != category.CategoriesId)
            {
                return BadRequest();
            }

            var categoryToEdit = await _context.Categories.FindAsync(id);
            categoryToEdit.CategoriesId = category.CategoriesId;
            categoryToEdit.Name = category.Name;
            categoryToEdit.IsActive = category.IsActive;
            /* categoryToEdit.Products = category.Products.Select(p => new Product()
                 {
                     //???
                 }).ToList();*/

            _context.Entry(categoryToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryForView>> PostCategory(CategoryForView category)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'RagsToRichesDbContext.Categories'  is null.");
            }

            var categoryToAdd = new Category()
            {
                CategoriesId = category.CategoriesId,
                Name = category.Name,
                IsActive = category.IsActive
            };

            _context.Categories.Add(categoryToAdd);
            await _context.SaveChangesAsync();

            return Ok(category);
            //return CreatedAtAction("GetCategory", new { id = category.CategoriesId }, category); //CreatedAtAction(actionName, routeValues, content value)
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoriesId == id)).GetValueOrDefault();
        }
    }
}
