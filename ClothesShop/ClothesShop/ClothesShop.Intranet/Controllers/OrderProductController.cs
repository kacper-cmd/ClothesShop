using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClothesShop.Data.Data;
using ClothesShop.Data.Data.Shop;

namespace ClothesShop.Intranet.Controllers
{
    public class OrderProductController : Controller
    {
        private readonly ClothesShopContext _context;

        public OrderProductController(ClothesShopContext context)
        {
            _context = context;
        }

        // GET: OrderProduct
        public async Task<IActionResult> Index()
        {
            var clothesShopContext = _context.OrderProduct.Include(o => o.Order).Include(o => o.Product).Include(o => o.Size);
            return View(await clothesShopContext.ToListAsync());
        }

        // GET: OrderProduct/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderProduct == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProduct
                .Include(o => o.Order)
                .Include(o => o.Product)
                .Include(o => o.Size)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            return View(orderProduct);
        }

        // GET: OrderProduct/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "City");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            ViewData["IdSize"] = new SelectList(_context.Sizes, "Id", "Id");
            return View();
        }

        // POST: OrderProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailId,OrderId,ProductId,Quantity,Discount,IdSize,TotalPrice")] OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "City", orderProduct.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", orderProduct.ProductId);
            ViewData["IdSize"] = new SelectList(_context.Sizes, "Id", "Id", orderProduct.IdSize);
            return View(orderProduct);
        }

        // GET: OrderProduct/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderProduct == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProduct.FindAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "City", orderProduct.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", orderProduct.ProductId);
            ViewData["IdSize"] = new SelectList(_context.Sizes, "Id", "Id", orderProduct.IdSize);
            return View(orderProduct);
        }

        // POST: OrderProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,OrderId,ProductId,Quantity,Discount,IdSize,TotalPrice")] OrderProduct orderProduct)
        {
            if (id != orderProduct.OrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderProductExists(orderProduct.OrderDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "City", orderProduct.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", orderProduct.ProductId);
            ViewData["IdSize"] = new SelectList(_context.Sizes, "Id", "Id", orderProduct.IdSize);
            return View(orderProduct);
        }

        // GET: OrderProduct/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderProduct == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProduct
                .Include(o => o.Order)
                .Include(o => o.Product)
                .Include(o => o.Size)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            return View(orderProduct);
        }

        // POST: OrderProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderProduct == null)
            {
                return Problem("Entity set 'ClothesShopContext.OrderProduct'  is null.");
            }
            var orderProduct = await _context.OrderProduct.FindAsync(id);
            if (orderProduct != null)
            {
                _context.OrderProduct.Remove(orderProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderProductExists(int id)
        {
          return (_context.OrderProduct?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }
    }
}
