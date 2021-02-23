using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeReserved = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    _logger.LogWarning($"404 Error - Path: {statusCodeReserved.OriginalPath} and QueryString = {statusCodeReserved.OriginalQueryString}");
                    return View("NotFound");
                case 400:
                    ViewBag.ErrorMessage = "Bad request. No ID specified for the resource.";
                    return View("BadRequest");
                default:
                    ViewBag.ErrorMessage = "There was an issue processing your request. Please try again. If the issue persists, please contact us at support@crybit.co.za and detail out the actions that your performed";
                    break;
            }
            return View("Error");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            
            _logger.LogError($"The path {exceptionDetails.Path} " +
                $"threw an exception {exceptionDetails.Error}");

            return View("Error");
        }
    }
}
