using MessagePack;

namespace WebsiteProjectAPI.ViewModel
{
    public class CartItemForView
    {

        public int CartId { get; set; }

        public string SessionId { get; set; }//Guid string

        public int ProductId { get; set; }

        public int? Discount { get; set; }

        public decimal TotalPrice { get; set; }

        //public virtual ProductForView? Product { get; set; } = null!;

    }
}
