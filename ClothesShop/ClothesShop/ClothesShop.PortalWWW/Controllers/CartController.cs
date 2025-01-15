using ClothesShop.Data.Data;
using ClothesShop.Data.Data.Shop;
using ClothesShop.PortalWWW.Helpers;
using ClothesShop.PortalWWW.Models.BusinessLogic;
using ClothesShop.PortalWWW.Models.Shop;
using ClothesShop.PortalWWW.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.PortalWWW.Controllers
{
    public class CartController : Controller
    {

        //private readonly ILogger<HomeController> _logger;
        //private readonly ClothesShopContext _context;

        //public CartController(ILogger<HomeController> logger, ClothesShopContext context)
        //{
        //    _logger = logger;
        //    _context = context;
        //}
        private readonly ILogger<HomeController> _logger;
        private readonly ClothesShopContext _context;
        private readonly CommonDataService _commonDataService;

        //public CheckoutController(ILogger<HomeController> logger, ClothesShopContext context)
        //{
        //    _logger = logger;
        //    _context = context;
        //}

        public CartController(ILogger<HomeController> logger, ClothesShopContext context,
            CommonDataService commonDataService)
        {
            _logger = logger;
            _context = context;
            _commonDataService = commonDataService;
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
            CartService cartService = new CartService(this._context, this.HttpContext);
            var dataForCart = new DataForCart//view model przekazuje dane do widoku
            {
                CartItems = await cartService.GetCartItems(),
                TotalPrice = await cartService.GetTotal()
            };

            var item = await _context.MenuItems.FindAsync(id);
           // return View(item);
            return View(dataForCart);

        }
        public async Task<IActionResult> AddToCart(int id)
        {
            CartService cartService = new CartService(this._context, this.HttpContext);
            cartService.AddToCart(await _context.Product.FindAsync(id));
            return RedirectToAction("Index");
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
