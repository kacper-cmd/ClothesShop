using ClothesShop.Data.Data;
using ClothesShop.Data.Data.CMS;
using ClothesShop.Data.Data.Shop;
using ClothesShop.PortalWWW.Models;
using ClothesShop.PortalWWW.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ClothesShop.PortalWWW.ViewModel;
using ClothesShop.PortalWWW.Helpers;

namespace ClothesShop.PortalWWW.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ClothesShopContext _context;
        private readonly CommonDataService _commonDataService;

        public HomeController(ILogger<HomeController> logger, ClothesShopContext context, CommonDataService commonDataService)
        {
            _logger = logger;
            _context = context;
            _commonDataService = commonDataService;

        }
        [Route("Index")]
        [AuthenticationFilter]
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.ModelMenuItem = await _commonDataService.GetMenuItemsAsync();
            ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
            ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();
            ViewBag.username = HttpContext.Session.GetString("username");

            if (id == null)
            {
                id = _context.MenuItems.First().IdWebsiteLinks;
            }

            var item = await _context.MenuItems.FindAsync(id);



            // Retrieve the best-selling products based on the order details
            var bestSellingProducts = _context.OrderProduct
                .GroupBy(op => op.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(op => op.Quantity)
                })
                .OrderByDescending(p => p.TotalQuantity)
                .Take(4) // Adjust the number as per your requirement
                .ToList();

            // Create a list of related products to store the best-selling products
            var relatedProducts = new List<Product>();

            // Iterate over the best-selling products and retrieve the corresponding product details
            foreach (var bestSellingProduct in bestSellingProducts)
            {
                var product = _context.Product.Include(p => p.Category).FirstOrDefault(p => p.ProductId == bestSellingProduct.ProductId);

                // Add the related product to the list
                relatedProducts.Add(product);
            }

            // Pass the list of related products to the view using ViewBag
            ViewBag.BestSellers = relatedProducts;




            return View(item);

        }
       

        //public async Task<IActionResult> Index(int? id)
        //{


        //    ViewBag.ModelMenuItem =
        //      await  (
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}