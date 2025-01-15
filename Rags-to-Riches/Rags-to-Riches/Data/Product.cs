using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data;

public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    public string? Description { get; set; }

    public decimal? Cost { get; set; }

    public bool IsActive { get; set; }

    public int SizesId { get; set; }

    public int? CategoriesId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("CategoriesId")]
    public virtual Category? Categories { get; set; } //Navigation Property

    public virtual ICollection<Image> Images { get; set; } = new List<Image>(); //Collection of related entities

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    [ForeignKey("SizesId")]
    public virtual Size Sizes { get; set; } = null!;
}


//public  class ProductViewModelCreateUpdate
//{


//    public string? Description { get; set; }

//    public decimal? Cost { get; set; }

//    public int SizesId { get; set; }

//    public int? CategoriesId { get; set; }

//    public virtual List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();


//}

//public class PruductViewModelCLient
//{
//    public string? Description { get; set; }

//    public decimal? Cost { get; set; }
//}

//public class CartItemViewModel
//{
//    public int? Discount { get; set; }
//}

//public class X
//{
//    public void Post(ProductViewModelCreateUpdate vm)
//    {
//        //Save
//        Product p = new Product();  
//        p.Description = vm.Description;
//        p.Cost = vm.Cost;
//        p.SizesId = vm.SizesId;
//        p.CategoriesId = vm.CategoriesId;
//        p.CartItems = vm.CartItems.Select(ci => new CartItem
//        {
//            Discount = ci.Discount
//        }).ToList();
//        db.Product.Add(p);
//        db.SaveChanges();

//        //GetById
//        db.Product.where(=>x.id== id).select(x=> new ProductViewmodwelClient {
//         Description = x.Description,
//           Cost = x.Cost
//        }).FirstOrDefault();

//    }
//}