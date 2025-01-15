using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClothesShop.PortalWWW.ViewModel
{
    public class FilterViewModel
    {

        public int? CategoryId { get; set; }
        public int? CategoryName { get; set; }
        public string Color { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public SelectList Categories { get; set; }
        
    }
}
