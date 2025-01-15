using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsideProjectMobile.Service.Reference;
using WebsiteProjectMobile.Helpers;
using WebsiteProjectMobile.Models;
using Xamarin.Forms;

namespace WebsiteProjectMobile.Services
{
  
    public class CartItemDataStore : ADataStore<CartItemForView>
    {
        public static Guid currentUserId;
        public CartItemDataStore()
            : base()
        {
            if (currentUserId == Guid.Empty)
            {
                currentUserId = Guid.NewGuid();
            }
            items = websideServiceReference.CartItemAllAsync().GetAwaiter().GetResult().ToList();

        }
        public async Task AddToCart(int id, double price)
        {
            string currentStringSessionId = currentUserId.ToString();
            string numericString = new string(currentStringSessionId.Where(char.IsDigit).ToArray());
            int currentIntSessionId = int.Parse(numericString.Substring(0, 5));

            CartItemForView pos = new CartItemForView
            {
                // CartId = currentIntSessionId,
                Discount = 0,
                TotalPrice = price,
                SessionId = currentUserId.ToString(),
                ProductId = id,
            };

            await websideServiceReference.CartItemPOSTAsync(pos);
        }
        public  async Task<List<ProductForView>> GetAllCurrentCartItems()
        {
            //get cart items
            IEnumerable<CartItemForView> cartItemForViews = await websideServiceReference.CartItemAllAsync();
            List<int> currentCartProductIds = cartItemForViews.Where(ci => ci.SessionId == currentUserId.ToString())
                .Select(ci => ci.ProductId).ToList();

            IEnumerable<ProductForView> productForViews = await websideServiceReference.ProductAllAsync();
            var currentCartProducts = productForViews.Where(p => currentCartProductIds.Contains(p.ProductId)).ToList();
            
            return currentCartProducts; 
        }

        public async Task MakeOrderFromCart()
        {
            string currentStringSessionId = currentUserId.ToString();
            string numericString = new string(currentStringSessionId.Where(char.IsDigit).ToArray());
            int currentIntSessionId = int.Parse(numericString.Substring(0, 5));

            List<ProductForView> currentCartProducts = await GetAllCurrentCartItems();
            OrderForView orderForView = new OrderForView
            {
                OrderId = currentIntSessionId,
                OrderDate = DateTime.Now,
                OrderStatusId = 1,
                UserId = 0,//change to autoincrement
                TotalCost = currentCartProducts.Sum(p => p.Cost),
                DeliveryDate = DateTime.Now.AddDays(7),      
            };
            await websideServiceReference.OrdersPOSTAsync(orderForView);
            int orderProductsCounter = currentIntSessionId; 

            foreach (var product in currentCartProducts)
            {
                OrderProductForView orderProductForView = new OrderProductForView
                {
                    OrderId = currentIntSessionId,
                    ProductId = product.ProductId,
                    Quantity = 1,
                    Price = product.Cost ?? 0,
                    OrderProductsId = orderProductsCounter//if you dont have autoincrement in db you can use this
                };
                orderProductsCounter++;
                await websideServiceReference.OrderProductsPOSTAsync(orderProductForView);
            }
           // await websideServiceReference.CartItemDELETEAsync(currentIntSessionId);
        }

        public override async Task<CartItemForView> AddItemToService(CartItemForView item)
        {
            try
            {
                await Application.Current.MainPage.DisplayAlert("Saved!", "Saved!", "OK");
                return await websideServiceReference.CartItemPOSTAsync(item);

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error, no saving!", ex.Message, "OK");
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override async Task<bool> DeleteItemFromService(CartItemForView item)
        {
            return await websideServiceReference.CartItemDELETEAsync(item.CartId).HandleRequest();
        }

        public override CartItemForView Find(CartItemForView item)
        {
            return items.Where((CartItemForView arg) => arg.CartId == item.CartId).FirstOrDefault();
        }

        public override CartItemForView Find(int id)
        {
            return items.Where((CartItemForView arg) => arg.CartId == id).FirstOrDefault();
        }

        public override async Task Refresh()
        {
            items = (await websideServiceReference.CartItemAllAsync()).ToList();
        }

        public override async Task<bool> UpdateItemInService(CartItemForView item)
        {
            return await websideServiceReference.CartItemPUTAsync(item.CartId, item).HandleRequest();
        }
    }

}
