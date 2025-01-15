using ClothesShop.Data.Data.Shop;
using ClothesShop.PortalWWW.Models;

namespace ClothesShop.PortalWWW.Services
{
    public interface IAccountService
    {
       // public User Login(string username, string password);
       public LoginResult Login(string username, string password);
        public LoginResult Register(string username, string password, string email);

    }
}
