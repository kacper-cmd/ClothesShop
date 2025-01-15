using ClothesShop.Data.Data.Shop;

namespace ClothesShop.PortalWWW.Models.Shop
{
    public class DataForCart
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }

    }
}