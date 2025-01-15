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
    public class OrdersController : ControllerBase
    {
        private readonly RagsToRichesDbContext _context;

        public OrdersController(RagsToRichesDbContext context)
        {
            _context = context;
        }


        //convert 
        private static OrderForView OrderForViewFromOrder(Order o)
        {
            try
            {
                return new OrderForView
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    TotalCost = o.TotalCost,
                    OrderDate = o.OrderDate,
                    DeliveryDate = o.DeliveryDate ?? DateTime.Now,
                    //OrderProducts = o.OrderProducts.Select(op => new OrderProductForView() //transform each OrderProduct into OrderProductForView
                    //{
                    //    OrderId = op.OrderId,
                    //    ProductId = op.ProductId,
                    //    Quantity = op.Quantity,
                    //    Price = op.Price,
                    //    OrderProductsId = op.OrderProductsId
                    //}).ToList(),
                    OrderStatusId = o.OrderStatusId,
                    OrderStatusName = o.OrderStatus?.Status ?? "N/A",

                };
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderForView>>> GetOrders()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            /*op is a parameter representing an instance of the OrderProduct entity, 
              and op.OrderProducts refers to the OrderProducts navigation property of the Orders entity.*/
            return (await _context.Orders.Include(op => op.OrderProducts) //database query to retrieve all Orders and then convert them into OrderForView
                .ToListAsync()) //.Include(u => u.User).ToListAsync()) load the related User entity for each queried Order
                .Select(o => OrderForViewFromOrder(o)).ToList(); //select all the orders and convert them
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderForView>> GetOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.Include(op => op.OrderProducts) //include related entities
                .FirstOrDefaultAsync(o => o.OrderId == id); //search the _context.Orders entity to find the first one with matching Ids

            if (order == null)
            {
                return NotFound();
            }

            //return order;
            return OrderForViewFromOrder(order); //convert to OrderForView
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderForView order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var orderToEdit = await _context.Orders.FindAsync(id);
            orderToEdit.UserId = order.UserId;
            orderToEdit.TotalCost = order.TotalCost;
            orderToEdit.OrderDate = order.OrderDate;
            orderToEdit.DeliveryDate = order.DeliveryDate;
            //orderToEdit.OrderProducts = order.OrderProducts;
            orderToEdit.OrderStatusId = order.OrderStatusId;

            _context.Entry(orderToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderForView>> PostOrder(OrderForView order)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'RagsToRichesDbContext.Orders'  is null.");
            }
            var orderToAdd = new Order
            {
                UserId = order.UserId,
                OrderId = order.OrderId,
                TotalCost = order.TotalCost,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                OrderStatusId = order.OrderStatusId
                //OrderProducts = order.OrderProducts.
            };

            _context.Orders.Add(orderToAdd);


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
