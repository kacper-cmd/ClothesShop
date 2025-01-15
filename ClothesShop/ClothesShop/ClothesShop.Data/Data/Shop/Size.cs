using ClothesShop.Data.Data.Shop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClothesShop.Data.Data.Shop
{
  
    public class Size
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }//Small, Medium, Large, XLarge, XXLarge, XXXLarge
        public bool IsActive { get; set; }
    }
}



