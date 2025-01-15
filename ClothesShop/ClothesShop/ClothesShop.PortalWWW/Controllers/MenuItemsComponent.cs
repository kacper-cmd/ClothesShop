using ClothesShop.Data.Data;
using ClothesShop.PortalWWW.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.PortalWWW.Controllers
{
    public class MenuItemsComponent : ViewComponent
    {

  

        private readonly ILogger<HomeController> _logger;
        private readonly ClothesShopContext _context;
        private readonly CommonDataService _commonDataService;


        public MenuItemsComponent(ILogger<HomeController> logger, ClothesShopContext context,
            CommonDataService commonDataService)
        {
            _logger = logger;
            _context = context;
            _commonDataService = commonDataService;
        }
       
        public async Task<IViewComponentResult> InvokeAsync()
        {
          
            return View("MenuItemsComponent", await _context.MenuItems.ToListAsync());
        }
    }
}
