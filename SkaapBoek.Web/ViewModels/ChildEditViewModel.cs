using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class ChildEditViewModel : BaseSheepViewModel
    {
        [Display(Name ="Mother")]
        public int? MotherId { get; set; }

        [Display(Name = "Father")]
        public int? FatherId { get; set; }
        public SelectList Males { get; set; }
        public SelectList Females { get; set; }
    }
}
