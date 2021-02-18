using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public abstract class BaseSheepViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Tag cannot exceed 100 characters")]
        [Display(Name = "Tag number")]
        public string TagNumber { get; set; }

        [Display(Name = "Tag color")]
        public int ColorId { get; set; }
        public SelectList Colors { get; set; }

        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        public List<Gender> Genders { get; set; }

        [Display(Name = "Birth date"), DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Selling price")]
        public decimal? SalePrice { get; set; }

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
        public int? EnclosureId { get; set; }
        public SelectList Enclosures { get; set; }

        [Display(Name ="Feed")]
        public int? FeedId { get; set; }
        public SelectList FeedList { get; set; }
    }
}
