using Data;
using WebsiteProjectAPI.Models.BussinessLogic;
using WebsiteProjectAPI.Util;

namespace WebsiteProjectAPI.Models
{
    public class CartBAPI : CartB
    {
        public CartBAPI(RagsToRichesDbContext context) : base(context)
        {
        }

        public virtual async Task AddToCart(CartItem cartPosition)
        {
            var cartP = new CartItem().CopyProperties(cartPosition);

            await base.AddToCart(cartP);
        }
    }
}
