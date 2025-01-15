using ClothesShop.Data.Data.Shop;

namespace ClothesShop.PortalWWW.ViewModel
{
    public class ProductsViewModel
    {
        public List<Product> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalProducts { get; set; } // Add this property to hold the total number of products

    }

}
