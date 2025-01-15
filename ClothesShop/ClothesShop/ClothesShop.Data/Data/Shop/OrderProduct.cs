using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.Data.Data.Shop
{
    public class OrderProduct
    {
        [Key]
        public int OrderDetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
        [Required]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter a value lower than or equal to {1}")]
        public int? Quantity { get; set; }

        [Range(0, 100, ErrorMessage = "Please enter a value between {1} and {2}")]
        public int? Discount { get; set; }
        //public string DiscountCode { get; set; }

        
        
        public int? IdSize { get; set; }
        [ForeignKey("IdSize")]
        public Size? Size { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalPrice { get; set; }
    }
}
