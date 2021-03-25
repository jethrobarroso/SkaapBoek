using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class SheepCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Tag cannot exceed 100 characters")]
        [Display(Name = "Tag number")]
        public string TagNumber { get; set; }

        [Display(Name = "Tag color")]
        public int ColorId { get; set; }
        public SelectList Colors { get; set; }

        [Display(Name = "Color change count")]
        public int ColorChangedCount { get; set; }

        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        public List<Gender> Genders { get; set; }

        [Display(Name = "Birth date"), DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Today;

        [Display(Name = "Selling price")]
        public decimal? SalePrice { get; set; }

        [Display(Name = "Date acquired"), DataType(DataType.Date)]
        public DateTime AcquireDate { get; set; } = DateTime.Today;

        [Display(Name = "Cost price")]
        public decimal? CostPrice { get; set; }

        [Display(Name = "State")]
        public int SheepStatusId { get; set; }
        public SelectList StatusList { get; set; }

        [Required]
        [Display(Name = "Weight")]
        [Range(0, 1000)]
        public float Weight { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int SheepCategoryId { get; set; }
        public SelectList Categories { get; set; }

        [Display(Name = "Pen number")]
        public int? PenId { get; set; }
        public SelectList Pens { get; set; }

        [Display(Name ="Feed")]
        public int? FeedId { get; set; }
        public SelectList FeedList { get; set; }

        [Display(Name = "Mother")]
        public int? MotherId { get; set; }
        public SelectList Ewes { get; set; }

        [Display(Name = "Father")]
        public int? FatherId { get; set; }
        public SelectList Rams { get; set; }
    }
}
