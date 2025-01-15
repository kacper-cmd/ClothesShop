using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.Data.Data.Shop
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter a username")]
        [StringLength(50, ErrorMessage = "The username must be between {2} and {1} characters long", MinimumLength = 3)]
        public string? Username { get; set; }

       
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [Column(TypeName = "varchar(MAX)")]
        [MaxLength]
        [DataType(DataType.Password)]

        public string? Password { get; set; }



        public ICollection<Order>? Orders { get; set; }
    }

}
