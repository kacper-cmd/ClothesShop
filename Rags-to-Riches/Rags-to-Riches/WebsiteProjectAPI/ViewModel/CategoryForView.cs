using Data;

namespace WebsiteProjectAPI.ViewModel
{
    public class CategoryForView
    {
        public int CategoriesId { get; set; }

        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public virtual ICollection<ProductForView>? Products { get; set; }         //public virtual ICollection<ProductForView> Products { get; set; } = new List<ProductForView>();
    }
}




//public int CategoriesId { get; set; }

//public string Name { get; set; }

//public bool IsActive { get; set; }

//public int ProductCount { get; set; }