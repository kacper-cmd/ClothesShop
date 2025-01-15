using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class CartItemController : ControllerBase
    {
        private readonly RagsToRichesDbContext _context;

        public CartItemController(RagsToRichesDbContext context)
        {
            _context = context;
        }

        // GET: api/CartItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemForView>>> GetCartItems()
        {
          if (_context.CartItems == null)
          {
              return NotFound();
          }

          return (await _context.CartItems.Include(p => p.Product).ThenInclude(i=>i.Images)
              .Include(d=>d.Product).ThenInclude(s=>s.Sizes)
              .Include( p=> p.Product)
              .ThenInclude(p=>p.Categories).ToListAsync()).Select(
              ci => CartItemForViewFromCartItem(ci)).ToList();
              
        }
        //ctrl r+m
        private static CartItemForView CartItemForViewFromCartItem(CartItem ci)
        {
            return new CartItemForView
            {
                CartId = ci.CartId,
                SessionId = ci.SessionId,
                ProductId = ci.ProductId,
                Discount = ci.Discount,
                TotalPrice = ci.TotalPrice,
                //Product = new ProductForView()
                //{
                //    CategoriesId = ci.Product.CategoriesId,
                //    CategoryName = ci.Product.Categories.Name,
                //    Cost = ci.Product.Cost,
                //    Images = ci.Product.Images.Select(i => new ImageForView()
                //    {
                //        ImageId = i.ImageId,
                //        Image1 = i.Image1,
                //        ProductId = i.ProductId
                //    }).ToList(),
                //    Description = ci.Product.Description,
                //    SizeName = ci.Product.Sizes.Size1 
                //}
            };
        }

        // GET: api/CartItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItemForView>> GetCartItem(int id)
        {
          if (_context.CartItems == null)
          {
              return NotFound();
          }
            var cartItem = await _context.CartItems.Include(p => p.Product).ThenInclude(i => i.Images)
                .Include(p => p.Product)
                .ThenInclude(p => p.Categories).
                FirstOrDefaultAsync(ci => ci.CartId ==id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return CartItemForViewFromCartItem(cartItem);
        }

        // PUT: api/CartItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItem(int id, CartItemForView cartItem)
        {
            if (id != cartItem.CartId)
            {
                return BadRequest();
            }
            var cartItemToEdit = await _context.CartItems.FindAsync(id);
            
                cartItemToEdit.CartId = cartItem.CartId;
                cartItemToEdit.ProductId = cartItem.ProductId;
                cartItemToEdit.Discount = cartItem.Discount;
                cartItemToEdit.SessionId = cartItem.SessionId;
                cartItemToEdit.TotalPrice = cartItem.TotalPrice;

            _context.Entry(cartItemToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemExists(id))
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

        // POST: api/CartItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartItemForView>> PostCartItem(CartItemForView cartItem)
        {
          if (_context.CartItems == null)
          {
              return Problem("Entity set 'RagsToRichesDbContext.CartItems'  is null.");
          }

          var cartItemToAdd = new CartItem
          {
              CartId  = cartItem.CartId,
              ProductId = cartItem.ProductId,
              Discount = cartItem.Discount,
              SessionId = cartItem.SessionId,
              TotalPrice = cartItem.TotalPrice

          };
               
          
          
            _context.CartItems.Add(cartItemToAdd);
            await _context.SaveChangesAsync();

            return Ok(cartItem);
        }

        // DELETE: api/CartItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            if (_context.CartItems == null)
            {
                return NotFound();
            }
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartItemExists(int id)
        {
            return (_context.CartItems?.Any(e => e.CartId == id)).GetValueOrDefault();
        }
    }
}
