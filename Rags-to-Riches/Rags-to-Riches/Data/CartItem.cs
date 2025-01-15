using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data;
public partial class CartItem
{
    [Key]
    public int CartId { get; set; }

    public string SessionId { get; set; }//string

    public int ProductId { get; set; }

    public int? Discount { get; set; }

    public decimal TotalPrice { get; set; }
    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; } = null!;
}


//public class CartItemViewModel
//{
//    public int IdCart { get; set; }
//    public string ProductName { get; set; }
//}