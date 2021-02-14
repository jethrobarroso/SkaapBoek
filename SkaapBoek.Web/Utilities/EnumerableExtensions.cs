using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Utilities
{
    public static class EnumerableExtensions
    {
        public static List<SelectListItem> ToListOfSelectListItem(this IEnumerable<SheepStatus> original)
        {
            var list = new List<SelectListItem>();

            foreach (var state in original)
            {
                list.Add(new SelectListItem
                {
                    Text = state.Name,
                    Value = state.Id.ToString()
                });
            }

            return list;
        }

        public static List<SelectListItem> ToListOfSelectListItem(this IEnumerable<Gender> original)
        {
            var list = new List<SelectListItem>();

            foreach (var gender in original)
            {
                list.Add(new SelectListItem
                {
                    Text = gender.Type,
                    Value = gender.Id.ToString()
                });
            }

            return list;
        }

        public static List<SelectListItem> ToListOfSelectListItem(this IEnumerable<HerdMember> original)
        {
            var list = new List<SelectListItem>();

            foreach (var sheep in original)
            {
                list.Add(new SelectListItem
                {
                    Text = sheep.TagNumber,
                    Value = sheep.Id.ToString()
                });
            }

            return list;
        }

        public static List<SelectListItem> ToListOfSelectListItem(this IEnumerable<Priority> original)
        {
            var list = new List<SelectListItem>();

            foreach (var priority in original)
            {
                list.Add(new SelectListItem
                {
                    Text = priority.Name,
                    Value = priority.Id.ToString()
                });
            }

            return list;
        }

        public static List<SelectListItem> ToListOfSelectListItem(this IEnumerable<Status> original)
        {
            var list = new List<SelectListItem>();

            foreach (var status in original)
            {
                list.Add(new SelectListItem
                {
                    Text = status.Name,
                    Value = status.Id.ToString()
                });
            }

            return list;
        }
    }
}
