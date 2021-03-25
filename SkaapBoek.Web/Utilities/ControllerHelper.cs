using Microsoft.AspNetCore.Mvc;
using SkaapBoek.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Utilities
{
    public class AppController : Controller
    {
        /// <summary>
        /// Prepares NotFound page for feed if the entity does not exist.
        /// </summary>
        /// <param name="model">Model fetched from the datastore</param>
        /// <param name="id">Id for entity lookup</param>
        /// <returns>True if entity was not found</returns>
        protected bool FeedNullCheckWith404(object model, int id)
        {
            if (model is null)
            {
                ViewBag.ErrorMessage = $"Feed with ID = {id} could not be found";
                return true;
            }

            return false;
        }

        /// <summary>
        /// Prepares NotFound page for sheep if the entity does not exist.
        /// </summary>
        /// <param name="model">Model fetched from the datastore</param>
        /// <param name="id">Id for entity lookup</param>
        /// <returns>True if entity was not found</returns>
        protected bool SheepNullCheckWith404(object model, int id)
        {
            if (model is null)
            {
                ViewBag.ErrorMessage = $"Sheep with ID = {id} could not be found";
                return true;
            }

            return false;
        }
    }
}
