using ClothesShop.Data.Data;
using ClothesShop.Data.Data.Shop;
using ClothesShop.PortalWWW.Helpers;
using ClothesShop.PortalWWW.Models.BusinessLogic;
using ClothesShop.PortalWWW.Models.Shop;
using ClothesShop.PortalWWW.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ClothesShop.PortalWWW.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ClothesShopContext _context;
        private readonly CommonDataService _commonDataService;
        private readonly IEmailService _emailService;

        public CheckoutController(ILogger<HomeController> logger, ClothesShopContext context,
            CommonDataService commonDataService, IEmailService emailService)
        {
            _logger = logger;
            _context = context;
            _commonDataService = commonDataService;
            _emailService = emailService;
        }
        [AuthenticationFilter]
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.ModelMenuItem = await _commonDataService.GetMenuItemsAsync();
            ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
            ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();
            if (id == null)
            {
                id = _context.MenuItems.First().IdWebsiteLinks;
            }
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatus, "OrderStatusId", "OrderStatusId");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password");

            CartService cartService = new CartService(this._context, this.HttpContext);
            var dataForCart = new DataForCheckout
            {
                CartItems = await cartService.GetCartItems(),
                TotalPrice = await cartService.GetTotal()
            };

            //  var item = await _context.MenuItems.FindAsync(id);
            return View(dataForCart);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DataForCheckout order)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(order);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                CartService cartService = new CartService(this._context, this.HttpContext);

                order.CartItems = await cartService.GetCartItems();
                decimal totalPrice = await cartService.GetTotal();

                var username = HttpContext.Session.GetString("username");
                var user = await _context.User.FirstOrDefaultAsync(u => u.Username == username);

                if (user != null)
                {
                    var newOrder = new Order
                    {
                        OrderDate = DateTime.Now,
                        CustomerName = order.CustomerName,
                        Street = order.Street,
                        City = order.City,
                        State = order.State,
                        ZipCode = order.ZipCode,
                        User = user,
                        OrderStatusId = 1
                    };
                    _context.Add(newOrder);
                    await _context.SaveChangesAsync();
                    foreach (var cartItem in order.CartItems)
                    {
                        var orderProduct = new OrderProduct
                        {
                            //Order = newOrder,
                            OrderId = newOrder.OrderId,
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,
                            Discount = 0,
                            TotalPrice = cartItem.Quantity * cartItem.Product.Price,// TotalPrice = totalPrice,//
                            IdSize = 1,
                        };

                        _context.Add(orderProduct);
                    }
                    await _context.SaveChangesAsync();

                    var userCartItems = await cartService.GetCartItems();

                    var emailDto = new EmailDto
                    {
                        To = user.Email ?? "",
                        Subject = "Order Confirmation",
                        Body = GenerateOrderConfirmationHtml(newOrder)
                    };

                    _emailService.SendEmail(emailDto);


                    // Remove the cart items from the context
                    _context.CartItems.RemoveRange(userCartItems);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Shop");
                }
                else
                {
                    TempData["ErrorMessage"] = "User not found in the database.";
                    return RedirectToAction(nameof(Index));
                }
            }



            //   ViewData["OrderStatusId"] = new SelectList(_context.OrderStatus, "OrderStatusId", "OrderStatusId", order.OrderStatusId);
            // ViewData["UserId"] = new SelectList(_context.User, "UserId", "Password", order.UserId);
            return RedirectToAction("Index", "Shop");
        }
        [HttpPost]
        public IActionResult SendEmail(EmailDto request)
        {
            _emailService.SendEmail(request);
            return RedirectToAction("Index", "Shop");
        }
        private string GenerateOrderConfirmationHtml(Order order)
        {

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<html>");
            stringBuilder.AppendLine("<body>");
            stringBuilder.AppendLine("<h1>Order Confirmation</h1>");

            stringBuilder.AppendLine("<h2>Customer Information</h2>");
            stringBuilder.AppendLine($"<p><strong>Name:</strong> {order.CustomerName}</p>");
            stringBuilder.AppendLine($"<p><strong>Street:</strong> {order.Street}</p>");
            stringBuilder.AppendLine($"<p><strong>City:</strong> {order.City}</p>");
            stringBuilder.AppendLine($"<p><strong>State:</strong> {order.State}</p>");
            stringBuilder.AppendLine($"<p><strong>Zip Code:</strong> {order.ZipCode}</p>");
            string OrderDate = order.OrderDate.ToString("dd-MM-yyyy");
            stringBuilder.AppendLine($"<p><strong>Order Date:</strong> {OrderDate}</p>");

            stringBuilder.AppendLine("<h2>Order Details</h2>");
            stringBuilder.AppendLine("<table>");

            stringBuilder.AppendLine("<tr><th>Product</th><th>Quantity</th><th>Price</th></tr>");
            if (order.OrderProducts != null)
            {
                foreach (var orderProduct in order.OrderProducts)
                {
                    stringBuilder.AppendLine("<tr>");
                    string productName = orderProduct.Product?.Name ?? "Unname product";
                    stringBuilder.AppendLine($"<td>{productName}</td>");
                    stringBuilder.AppendLine($"<td>{orderProduct.Quantity}</td>");
                    stringBuilder.AppendLine($"<td>{orderProduct.TotalPrice}</td>");
                    stringBuilder.AppendLine("</tr>");
                }
            }
            else
            {
                stringBuilder.AppendLine("<tr>");
                stringBuilder.AppendLine("<td colspan=\"3\">No order products available.</td>");
                stringBuilder.AppendLine("</tr>");
            }
            stringBuilder.AppendLine("</table>");
            stringBuilder.AppendLine("</body>");
            stringBuilder.AppendLine("</html>");

            return stringBuilder.ToString();
        }
        //public async Task<IActionResult> Index(int? id)
        //{

        //    ViewBag.ModelMenuItem =
        //      await (
        //            from menu in _context.MenuItems
        //            orderby menu.Order
        //            select menu
        //        ).ToListAsync();
        //    ViewBag.ModelBanner =
        //   await (
        //         from banner in _context.Banners

        //         select banner
        //     ).ToListAsync();
        //    ViewBag.ModelFooter =
        // await (
        //       from footer in _context.Footers

        //       select footer
        //   ).ToListAsync();

        //    if (id == null)
        //    {
        //        id = _context.MenuItems.First().IdWebsiteLinks;
        //    }
        //    var item = await _context.MenuItems.FindAsync(id);
        //    return View(item);
        //}

    }
}
