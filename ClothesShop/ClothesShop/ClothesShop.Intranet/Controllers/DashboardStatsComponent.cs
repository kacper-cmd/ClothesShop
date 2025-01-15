using ClothesShop.Data.Data;
using ClothesShop.Intranet.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.Intranet.Controllers
{
    public class DashboardStatsComponent : ViewComponent
    {
        private readonly ClothesShopContext _context;

        public DashboardStatsComponent(ClothesShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //DashboardStatsViewModel model = new DashboardStatsViewModel
            //{
            //    MoneyToday = "$53,000",
            //    MoneyPercentageChange = "+55%",
            //    OrdersToday = "2,300",
            //    OrdersPercentageChange = "+3%",
            //    NewClients = "+3,462",
            //    NewClientsPercentageChange = "-2%",
            //    SalesAmount = "$103,430",
            //    SalesPercentageChange = "+5%"
            //};
            DashboardStatsViewModel model = new DashboardStatsViewModel();
            DateTime today = DateTime.Now.Date;
            int ordersToday = _context.OrderProduct
                .Where(op => op.Order.OrderDate.Date == today)
                .Count();

            model.OrdersToday = ordersToday.ToString();
            decimal moneyToday = (decimal)_context.OrderProduct
                .Where(op => op.Order.OrderDate.Date == today)
                .Sum(op => op.TotalPrice);

            model.MoneyToday = moneyToday.ToString("C");

   
            var recentUsers = _context.User
            .Include(u => u.Orders)
            .Where(u => u.Orders.Any(o => o.OrderDate >= DateTime.Now.AddDays(-7)))
            .OrderByDescending(u => u.UserId)
            .Take(3)
            .ToList();


            ViewBag.RecentUsers = recentUsers;//jak to zrobic w view modelu



            decimal previousDayMoney = (decimal)_context.OrderProduct
                .Where(op => op.Order.OrderDate.Date == today.AddDays(-1))
                .Sum(op => op.TotalPrice);

            decimal previousDayOrders = _context.OrderProduct
                .Where(op => op.Order.OrderDate.Date == today.AddDays(-1))
                .Count();

            if (previousDayMoney != 0)
            {
                decimal moneyPercentageChange = ((moneyToday - previousDayMoney) / previousDayMoney) * 100;
                model.MoneyPercentageChange = moneyPercentageChange.ToString("+#.##;-#.##;0") + "%";
            }
            else
            {
                model.MoneyPercentageChange = "N/A";
            }

            if (previousDayOrders != 0)
            {
                decimal ordersPercentageChange = ((ordersToday - previousDayOrders) / previousDayOrders) * 100;
                model.OrdersPercentageChange = ordersPercentageChange.ToString("+#.##;-#.##;0") + "%";
            }
            else
            {
                model.OrdersPercentageChange = "N/A";
            }



            int currentYear = today.Year;
            int previousYear = currentYear - 1;

            DateTime currentYearStart = new DateTime(currentYear, 1, 1);
            DateTime currentYearEnd = new DateTime(currentYear, 12, 31);

            DateTime previousYearStart = new DateTime(previousYear, 1, 1);
            DateTime previousYearEnd = new DateTime(previousYear, 12, 31);

            // Retrieve the users who have orders in the previous year
            var previousYearUsers = _context.User
                .Where(u =>
                    u.Orders.Any(o =>
                        o.OrderDate >= previousYearStart &&
                        o.OrderDate <= previousYearEnd
                    )
                )
                .ToList();

            // Retrieve the users who have orders in the current year
            var currentYearUsers = _context.User
                .Where(u =>
                    u.Orders.Any(o =>
                        o.OrderDate >= currentYearStart &&
                        o.OrderDate <= currentYearEnd
                    )
                )
                .ToList();
            int changedUsersCount = previousYearUsers.Except(currentYearUsers).Count();
            ViewBag.ChangedUsersCount = changedUsersCount;


            return View("DashboardStatsComponent", model);

        }
    }
}
