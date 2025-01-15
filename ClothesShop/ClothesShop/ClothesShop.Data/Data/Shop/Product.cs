using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClothesShop.Data.Data.Shop
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 100 characters")]
        public string Name { get; set; }
        [Range(0.01, 1000000, ErrorMessage = "Price must be between 0.01 and 1000000")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Display(Name = "Image Name Url")]
        [Column(TypeName = "varchar(MAX)")]
        public string? ImageNameUrl { get; set; }
        //[NotMapped]
        //public byte[]? ImageArray { get; set; }
        [AllowHtml]
        public string? Description { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public string? Color { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; }

        //public int? IdSize { get; set; }
        //[ForeignKey("IdSize")]
        //public Size? Size { get; set; }



    }
}
