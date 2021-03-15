using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels.Partials
{
    public class EditPhaseGroupModel
    {
        public int OnEditedGroupId { get; set; }

        [Display(Name = "New Date")]
        public DateTime NewDate { get; set; } = DateTime.Today;
    }
}
