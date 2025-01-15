using ClothesShop.Data.Data.Shop;
using System.ComponentModel.DataAnnotations;

namespace ClothesShop.PortalWWW.Models.Shop
{
    public class DataForCheckout
    {
        public List<CartItem>? CartItems { get; set; }
        public decimal? TotalPrice { get; set; }
        [Display(Name = "Customer Name")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "The Street field is required.")]
        public string? Street { get; set; }
        [Required(ErrorMessage = "The City field is required.")]
        public string? City { get; set; }
        [Required(ErrorMessage = "The State field is required.")]
        public string? State { get; set; }
        [Required(ErrorMessage = "The ZipCode field is required.")]
        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }
    }
}
