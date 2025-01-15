using ClothesShop.Data.Data;
using ClothesShop.Data.Data.Shop;
using ClothesShop.Intranet.Models;
using ClothesShop.Intranet.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClothesShop.Intranet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;



        private readonly ClothesShopContext _context;

        public HomeController(ILogger<HomeController> logger, ClothesShopContext context)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {

            var latestOrders = await _context.Order
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToListAsync();

            ViewBag.LatestOrders = latestOrders.Select(item => new OrderForView()
            {
                // Sales = item.OrderProducts.Sum(op => op.TotalPrice),
                Sales = (decimal)item.OrderProducts.Sum(op => op.Product.Price * op.Quantity),
                Value = (decimal)item.OrderProducts.Sum(op => op.Quantity),
                City = item.OrderProducts.Select(op => op.Order.City).FirstOrDefault()
            });


            var categories2 = _context.Category
            .Include(c => c.Products)
            .Select(c => new CategoryStatsVM()
            {
                Name = c.Name,
                productCount = c.Products.Count,
                productSoldCount = (int)c.Products.SelectMany(p => p.OrderProducts).Sum(op => op.Quantity)
            })
            .ToList();

            ViewBag.Categories2 = categories2;

            //var categories = _context.Category
            //.Include(c => c.Products) 
            //.Take(5)
            //.ToList();

            // ViewBag.Categories = categories2;      

            SalesOverviewViewModel salesOverview = new SalesOverviewViewModel();
            salesOverview.TotalSales = CalculateTotalSales();
            salesOverview.SalesIncreasePercentage = CalculateSalesIncreasePercentage();
            ViewBag.SalesOverview = salesOverview;

            return View();
        }
        private decimal CalculateTotalSales()
        {
            decimal totalSales = (decimal)_context.OrderProduct.Sum(op => op.TotalPrice);
            return totalSales;
        }

        private decimal CalculateSalesIncreasePercentage()
        {
            decimal previousYearTotalSales = (decimal)_context.OrderProduct
                .Where(op => op.Order.OrderDate.Year == DateTime.Now.Year - 1)
                .Sum(op => op.TotalPrice);

            decimal currentYearTotalSales = (decimal)_context.OrderProduct
                .Where(op => op.Order.OrderDate.Year == DateTime.Now.Year)
                .Sum(op => op.TotalPrice);

            //     decimal previousYearTotalSales = _context.Order
            //.Where(o => o.OrderDate.Year == DateTime.Now.Year - 1)
            //.SelectMany(o => o.OrderProducts)
            //.Sum(op => op.TotalPrice);

            //     decimal currentYearTotalSales = _context.Order
            //         .Where(o => o.OrderDate.Year == DateTime.Now.Year)
            //         .SelectMany(o => o.OrderProducts)
            //         .Sum(op => op.TotalPrice);

            decimal salesIncreasePercentage = 0;
            if (previousYearTotalSales != 0)
            {
                salesIncreasePercentage = (currentYearTotalSales - previousYearTotalSales) / previousYearTotalSales * 100;
            }

            return salesIncreasePercentage;
        }


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