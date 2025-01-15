using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClothesShop.Data.Data.CMS
{
    public class Banner
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Display(Name = "Sub Title")]
        public string? SubTitle { get; set; }
        [AllowHtml]
        [Column(TypeName = "varchar(MAX)")]
        public string? Description { get; set; }
        [Display(Name = "Image")]
        public string? ImageNameUrl { get; set; }
        public string ButtonText { get; set; }
        [Display(Name = "Button Url")]
        public string? ButtonUrl { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Social Links 1")]
        public string? SocialLinks1 { get; set; }
        [Display(Name = "Social Icon 1")]
        public string? SocialIcon1 { get; set; }
        [Display(Name = "Social Links 2")]
        public string? SocialLinks2 { get; set; }
        [Display(Name = "Social Icon 2")]
        public string? SocialIcon2 { get; set; }
        [Display(Name = "Social Links 3")]
        public string? SocialLinks3 { get; set; }
        [Display(Name = "Social Icon 3")]
        public string? SocialIcon3 { get; set; }
        [Display(Name = "Social Links 4")]
        public string? SocialLinks4 { get; set; }
        [Display(Name = "Social Icon 4")]
        public string? SocialIcon4 { get; set; }


    }
}
