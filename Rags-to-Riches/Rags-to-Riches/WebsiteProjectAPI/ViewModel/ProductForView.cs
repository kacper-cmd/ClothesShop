using Data;

namespace WebsiteProjectAPI.ViewModel

{
    public class ProductForView
    {
        public int ProductId { get; set; }

        public string? Description { get; set; }

        public decimal? Cost { get; set; }

        public bool IsActive { get; set; }

        public int? SizesId { get; set; }

        public int? CategoriesId { get; set; }

        public string? SizeName { get; set; }

        public string? CategoryName { get; set; }

        //public virtual ICollection<CartItemForView> CartItems { get; set; } = new List<CartItemForView>();

        //public virtual CategoryForView? Categories { get; set; }

        public ICollection<ImageForView>? Images { get; set; }

        public virtual ICollection<OrderProductForView> OrderProducts { get; set; } = new List<OrderProductForView>();

        //public virtual SizeForView Sizes { get; set; } = null!;

    }

}