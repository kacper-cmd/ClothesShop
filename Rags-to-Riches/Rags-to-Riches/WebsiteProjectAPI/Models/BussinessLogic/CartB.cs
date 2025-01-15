using Data;
using Microsoft.EntityFrameworkCore;
using WebsiteProjectAPI.Util;

namespace WebsiteProjectAPI.Models.BussinessLogic
{
    public class CartB
    {
        private readonly RagsToRichesDbContext _context;

        public CartB(RagsToRichesDbContext context)
        {
            _context = context;
        }

        public virtual async Task AddToCart(CartItem cartPosition)
        {
            try
            {
                var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(cartit =>
                    cartit.SessionId == cartPosition.SessionId
                    && (cartit.ProductId == cartPosition.ProductId));
                _context.CartItems.Add(cartPosition);

                //if (existingCartItem != null)
                //{
                //    existingCartItem.Quantity += cartPosition.Quantity;
                //    _context.Entry(existingCartItem).State = EntityState.Modified;
                //}
                //else
                //{
                //    _context.CartPosition.Add(cartPosition);
                //}
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public virtual async Task<Order> MakeOrderFromCurrentCart(string CurrentUserId)
        {
            try
            {

                var currentCartItems = await GetCartPositions(CurrentUserId);

                if (currentCartItems == null || currentCartItems.Count() == 0)
                {
                    throw new Exception("Cart is empty for you bad boi");
                }
                var user = new User
                {
                    IsActive = true,
                    Password = "123",
                    AddressesId = 1,
                    //  UserRolesId = 1,
                    Username = "test",
                    //Addresses = new Address
                    //{
                    //    City = "test",
                    //    HomeNumber = 1,
                    //    Country = "test",
                    //    Street = "test",
                    //    ZipCode = "test"
                    //}                  
                };
                _context.Add(user);
                await _context.SaveChangesAsync();
                var order = new Order
                {
                    UserId = 0,
                    OrderDate = DateTime.Now,
                    DeliveryDate = DateTime.Now.AddDays(7),
                    OrderStatusId = 1,
                    // OrderProducts = currentCartItems,
                    TotalCost = await CalculateCartTotal(currentCartItems)
                };

                _context.Add(order);
                await _context.SaveChangesAsync();
                await ConvertCartItemsToOrder(currentCartItems, order);
                // await DeleteCartPositions(currentCartItems);
                return order;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public virtual async Task DeleteCartPosition(int cartPositionId)
        {
            var positionToDelete = await _context.CartItems.FindAsync(cartPositionId);
            if (positionToDelete != null)
            {
                _context.Remove(positionToDelete);
                await _context.SaveChangesAsync();
            }
        }
        public virtual async Task DeleteCartPositions(List<CartItem> currentCartItems)
        {
            foreach (var item in currentCartItems)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            await _context.SaveChangesAsync();
        }
        public virtual async Task ConvertCartItemsToOrder(List<CartItem> currentCartItems, Order order)
        {
            order.OrderProducts = new List<OrderProduct>();
            currentCartItems.ForEach(
                item =>
                {
                    var pos = new OrderProduct { OrderId = order.OrderId }
                    .CopyProperties(item);
                    pos.OrderProductsId = 0;
                    _context.Add(pos);
                    order.OrderProducts.Add(pos);
                });
            await _context.SaveChangesAsync();
        }
        public virtual async Task<decimal> CalculateCartTotal(string CurrentUserId)
        {
            var existingItems = GetCartPositions(CurrentUserId);
            return await CalculateCartTotal(await existingItems);
        }
        public virtual async Task<List<CartItem>> GetCartPositions(string CurrentUserId)
        {
            return
                await
                _context
                .CartItems
                .Include(cart => cart.Product)
                .Where(cp => cp.SessionId == CurrentUserId).ToListAsync();
        }
        public virtual async Task<decimal> CalculateCartTotal(IEnumerable<CartItem> existingItems)
        {
            var price = existingItems
                 .Where(cart => cart.ProductId != null)
                 .Select(cart => cart.Product.Cost * cart.TotalPrice).Sum() ?? 0;

            return price;
        }
    }
}
