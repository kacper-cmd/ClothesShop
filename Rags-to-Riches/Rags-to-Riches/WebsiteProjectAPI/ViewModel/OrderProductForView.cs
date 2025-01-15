using Data;

namespace WebsiteProjectAPI.ViewModel
{
    public class OrderProductForView
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int OrderProductsId { get; set; }

       // public virtual OrderForView Order { get; set; } = null!;

       // public virtual ProductForView Product { get; set; } = null!;
    }
}
