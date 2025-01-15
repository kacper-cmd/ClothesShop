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
    public class Footer
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        [AllowHtml]
        [Column(TypeName = "varchar(MAX)")]
        public string? Icon { get; set; }
        public string? Logo { get; set; }
        public string? Description { get; set; }
        [Range(1, 3, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int ColumnNumber { get; set; }
    }
}
