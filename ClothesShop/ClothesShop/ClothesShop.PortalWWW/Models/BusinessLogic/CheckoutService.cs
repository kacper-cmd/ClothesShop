using ClothesShop.Data.Data.Shop;
using ClothesShop.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.PortalWWW.Models.BusinessLogic
{
    public class CheckoutService
    {
        private readonly ClothesShopContext _context;
        private string IdSession { get; set; }

        public CheckoutService(ClothesShopContext context, HttpContext httpContext)
        {
            _context = context;
            IdSession = GetIdSession(httpContext);
        }

        private string GetIdSession(HttpContext httpContext)
        {
            if (httpContext.Session.GetString("IdSession") == null)
            {

                if (!string.IsNullOrWhiteSpace(httpContext.User.Identity.Name))
                {
                    httpContext.Session.SetString("IdSession", httpContext.User.Identity.Name);
                }
                else
                {
                    Guid tempIdSesjiKoszyka = Guid.NewGuid();
                    httpContext.Session.SetString("IdSession", tempIdSesjiKoszyka.ToString());
                }
            }
            return httpContext.Session.GetString("IdSession").ToString();
        }
        public void AddToCart(Product product)
        {
            //najpierw sprawdzamy czy dany towar nie istnieje juz w koszyku danego klienta
            //var elementKoszyka = _context.ElementKoszyka.FirstOrDefault(
            //                   element => element.IdSesjiKoszyka == this.IdSesjiKoszyka && element.TowarId == towar.IdTowaru);
            var cartItem =
                (
                 from ci in _context.CartItems
                 where ci.SessionId == this.IdSession && ci.ProductId == product.ProductId
                 select ci
                 ).FirstOrDefault();
            //jezeli towaru brak w koszyku 
            if (cartItem == null)
            {
                //tworzymy towar koszyka
                //to dodajemy nowy element koszyka
                cartItem = new CartItem()
                {
                    SessionId = this.IdSession,
                    ProductId = product.ProductId,
                    Product = _context.Product.Find(product.ProductId),
                    //Size = _context.Size.Find(size.IdSize),
                    //    SizeId = product.SizeId,
                    Quantity = 1,
                    CreationDate = DateTime.Now,
                    Discount = 0
                };
                //dodajemy element koszyka do bazy danych
                _context.CartItems.Add(cartItem);
            }
            else
            {
                //jezeli towar juz istnieje w koszyku to zwiekszamy jego ilosc o 1
                cartItem.Quantity++;
            }
            //zapisujemy zmiany w bazie danych
            _context.SaveChanges();

        }
        //funckja pobiera wszystkie elementy koszyka danej przegladarki
        public async Task<List<CartItem>> GetCartItems()
        {
            return await _context.CartItems.Where(e => e.SessionId == this.IdSession).Include(e => e.Product).ToListAsync();
        }
        //funckja oblicza wartosc koszyka za ile pieniedzy kupilismy towary
        public async Task<decimal> GetTotal()
        {
            var items =
            (
                from element in _context.CartItems
                where element.SessionId == this.IdSession
                select (decimal?)element.Quantity * element.Product.Price
            );
            return await items.SumAsync() ?? 0;
        }
    }
}