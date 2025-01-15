using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClothesShop.Data.Data.CMS
{
    public class MenuItem
    {

        [Key]
        public int IdWebsiteLinks { get; set; }
        [Required(ErrorMessage = "Link is required")]
        [MaxLength(20, ErrorMessage = "The link can contain a max of 20 characters")]
        [Display(Name = "Link to page")]
        public string Link { get; set; }//link /Test/Index
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(30, ErrorMessage = "The title can contain a max of 30 characters")]
        [Display(Name = "Link name")]
        public string? LinkName { get; set; }//title home, login

        [Display(Name = "Controller name")]
        public string? Controller { get; set; }
        [Display(Name = "Additional info")]
        public string? AdditionalInfo { get; set; }//price 0 /

        [Display(Name = "Action name")]
        public string? Action { get; set; }
        [Display(Name = "Icons of link")]
        public string? Icon { get; set; }


        [Required(ErrorMessage = "Order is required")]
        [Display(Name = "Display Position")]
        public int? Order { get; set; }
        [Required(ErrorMessage = "Active is required")]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public bool RightMenu { get; set; }
    }
}
