using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class ChildDetailsViewModel
    {
        public Child Child { get; set; }
        public HerdMember Mother { get; set; }
        public HerdMember Father { get; set; }
    }
}
