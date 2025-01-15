using ClothesShop.Data.Data.Shop;

namespace ClothesShop.PortalWWW.Models
{
    public class LoginResult
    {
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public User? User { get; set; }
    }

}
