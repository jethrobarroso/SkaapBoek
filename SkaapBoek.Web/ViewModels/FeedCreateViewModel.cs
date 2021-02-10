using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class FeedCreateViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [MaxLength(50)]
        public string Supplier { get; set; }
        
        [Display(Name = "Cost Price")]
        public decimal? CostPrice { get; set; }
    }
}
