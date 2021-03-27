using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels.Partials
{
    public class RemoveMilsGroupModel
    {
        public int GroupIdToRemove { get; set; }

        [Display(Name = "Pen")]
        public int PenIdToMoveGroup { get; set; }
        public SelectList Pens { get; set; }
    }
}
