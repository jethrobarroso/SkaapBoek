using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkaapBoek.Core;
using SkaapBoek.DAL;
using SkaapBoek.DAL.Services;
using SkaapBoek.Web.Utilities;
using SkaapBoek.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FeedController : AppController
    {
        private readonly ILogger<FeedController> _logger;
        private readonly IFeedService _service;

        public FeedController(ILogger<FeedController> logger, IFeedService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllNoTrack();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeedCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newFeed = new Feed
                {
                    Name = model.Name,
                    CostPrice = model.CostPrice,
                    ProductCode = model.ProductCode,
                    Supplier = model.Supplier,
                    Description = model.Description
                };
                await _service.Add(newFeed);
                return RedirectToAction("Details", new { Id = newFeed.Id });
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var feed = await _service.GetById(id);
            if(feed == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Feed with ID = {id} could not be found";
                return View("NotFound");
            }

            return View(feed);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var feed = await _service.GetById(id);

            if (feed == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Feed with ID = {id} could not be found";
                return View("NotFound");
            }

            await _service.Delete(id);
            TempData["Success"] = $"Successfully deleted {feed.Name}";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var feed = await _service.GetById(id);

            if (FeedNullCheckWith404(feed, id))
                return View("NotFound");

            var model = new FeedEditViewModel
            {
                Id = feed.Id,
                Name = feed.Name,
                ProductCode = feed.ProductCode,
                Supplier = feed.Supplier,
                Description = feed.Description,
                CostPrice = feed.CostPrice
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FeedEditViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var feed = await _service.GetById(id);
                feed.Name = model.Name;
                feed.ProductCode = model.ProductCode;
                feed.CostPrice = model.CostPrice;
                feed.Description = model.Description;
                feed.Supplier = model.Supplier;

                await _service.Update(feed);
                TempData["Success"] = $"Successfully updated {model.Name}";
                return RedirectToAction("List");
            }

            return View(model);
        }
    }
}
