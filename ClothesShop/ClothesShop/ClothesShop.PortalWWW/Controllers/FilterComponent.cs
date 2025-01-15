using ClothesShop.Data.Data;
using ClothesShop.PortalWWW.Services.Common;
using ClothesShop.PortalWWW.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.PortalWWW.Controllers
{
    public class FilterComponent : ViewComponent
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ClothesShopContext _context;
        private readonly CommonDataService _commonDataService;


        public FilterComponent(ILogger<HomeController> logger, ClothesShopContext context,
            CommonDataService commonDataService)
        {
            _logger = logger;
            _context = context;
            _commonDataService = commonDataService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new FilterViewModel();

            // Retrieve category options from the database
            var categories = await _context.Category.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");

            // Retrieve color options from the database
            var colors = await _context.Product.Select(p => p.Color).Distinct().ToListAsync();
            ViewBag.Colors = new SelectList(colors);
            decimal minPrice = _context.Product.Min(p => p.Price);
            decimal maxPrice = _context.Product.Max(p => p.Price);

            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            return View("FilterComponent", model);
        }
    }
}