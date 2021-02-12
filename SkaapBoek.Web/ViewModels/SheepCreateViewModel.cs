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
    public class SheepCreateViewModel : BaseSheepViewModel
    {
        [Display(Name = "Date acquired"), DataType(DataType.Date)]
        public DateTime AcquireDate { get; set; }

        [Display(Name = "Cost price")]
        public decimal CostPrice { get; set; }
    }
}
