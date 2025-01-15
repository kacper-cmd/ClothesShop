using ClothesShop.Data.Data;
using ClothesShop.Data.Data.CMS;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.PortalWWW.Services.Common
{
  
    public class CommonDataService
    {
       
        private readonly ClothesShopContext _context;

        public CommonDataService(ClothesShopContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetMenuItemsAsync()
        {
            return await (
                from menu in _context.MenuItems
                orderby menu.Order
                select menu
            ).ToListAsync();
        }

        public async Task<List<Banner>> GetBannersAsync()
        {
            return await _context.Banners.ToListAsync();
        }

        public async Task<List<Footer>> GetFootersAsync()
        {
            return await _context.Footers.ToListAsync();
        }
    }

}
