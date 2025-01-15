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
    public class OrderProductsController : ControllerBase
    {
        private readonly RagsToRichesDbContext _context;

        public OrderProductsController(RagsToRichesDbContext context)
        {
            _context = context;
        }

        private static OrderProductForView OrderProductForViewFromOrderProduct(OrderProduct op)
        {
            return new OrderProductForView()
            {
                OrderId = op.OrderId,
                ProductId = op.ProductId,
                Quantity = op.Quantity,
                Price = op.Price,
                OrderProductsId = op.OrderProductsId,
            };
        }

        // GET: api/OrderProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProductForView>>> GetOrderProducts()
        {
            if (_context.OrderProducts == null)
            {
                return NotFound();
            }
            return (await _context.OrderProducts.ToListAsync())
                .Select(op => OrderProductForViewFromOrderProduct(op)).ToList();
        }

        // GET: api/OrderProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProductForView>> GetOrderProduct(int id)
        {
            if (_context.OrderProducts == null)
            {
                return NotFound();
            }
            var orderProduct = await _context.OrderProducts.FindAsync(id);

            if (orderProduct == null)
            {
                return NotFound();
            }

            return OrderProductForViewFromOrderProduct(orderProduct);
        }

        // PUT: api/OrderProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderProduct(int id, OrderProductForView orderProduct)
        {
            if (id != orderProduct.OrderProductsId)
            {
                return BadRequest();
            }

            var orderProductToEdit = await _context.OrderProducts.FindAsync(id);
            orderProductToEdit.OrderId = orderProduct.OrderId;
            orderProductToEdit.ProductId = orderProduct.ProductId;
            orderProductToEdit.Quantity = orderProduct.Quantity;
            orderProductToEdit.Price = orderProduct.Price;
            orderProductToEdit.OrderProductsId = orderProduct.OrderProductsId;

            _context.Entry(orderProductToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(id))
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

        // POST: api/OrderProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderProductForView>> PostOrderProduct(OrderProductForView orderProduct)
        {
            if (_context.OrderProducts == null)
            {
                return Problem("Entity set 'RagsToRichesDbContext.OrderProducts'  is null.");
            }

            var orderProductToAdd = new OrderProduct()
            {
                OrderId = orderProduct.OrderId,
                ProductId = orderProduct.ProductId,
                Quantity = orderProduct.Quantity,
                Price = orderProduct.Price,
               OrderProductsId = orderProduct.OrderProductsId,
            };

            _context.OrderProducts.Add(orderProductToAdd);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderProductExists(orderProduct.OrderProductsId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok(orderProduct);
            //return CreatedAtAction("GetOrderProduct", new { id = orderProduct.OrderProductsId }, orderProduct);
        }

        // DELETE: api/OrderProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderProduct(int id)
        {
            if (_context.OrderProducts == null)
            {
                return NotFound();
            }
            var orderProduct = await _context.OrderProducts.FindAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderProductExists(int id)
        {
            return (_context.OrderProducts?.Any(e => e.OrderProductsId == id)).GetValueOrDefault();
        }
    }
}