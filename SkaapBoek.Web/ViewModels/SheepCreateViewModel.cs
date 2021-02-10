using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class SheepCreateViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage ="Tag cannot exceed 100 characters")]
        [Display(Name ="Tag number")]
        public string TagNumber { get; set; }

        [Display(Name = "Tag color")]
        public int ColorId { get; set; }
        public SelectList Colors { get; set; }

        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        public List<Gender> Genders { get; set; }

        [Display(Name ="Birth date"), DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Date acquired"), DataType(DataType.Date)]
        public DateTime AcquireDate { get; set; }

        [Display(Name = "Cost price")]
        public decimal CostPrice { get; set; }

        [Display(Name = "Selling price")]
        public decimal SalePrice { get; set; }

        [Display(Name = "State")]
        public int SheepStatusId { get; set; }
        public SelectList StatusList { get; set; }

        [Required]
        [Display(Name = "Weight")]
        public float Weight { get; set; }
    }
}
