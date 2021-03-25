using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkaapBoek.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace SkaapBoek.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheepController : ControllerBase
    {
        private readonly ISheepService _sheepService;
        private readonly ILogger<SheepController> _logger;

        public SheepController(ISheepService sheepService,
            ILogger<SheepController> logger)
        {
            _sheepService = sheepService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GetSheep()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var sheepData = _sheepService.GetAll()
                    .Include(s => s.Gender)
                    .Include(s => s.SheepStatus)
                    .Include(s => s.Color)
                    .Include(s => s.Category)
                    .Include(s => s.Mother)
                    .Include(s => s.Father)
                    .AsNoTracking();
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    sheepData = sheepData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    sheepData = sheepData.Where(m => m.TagNumber.Contains(searchValue)
                                        || m.Color.Name.Contains(searchValue)
                                        || m.Gender.Type.Contains(searchValue)
                                        || m.Mother.TagNumber.Contains(searchValue)
                                        || m.Father.TagNumber.Contains(searchValue)
                                        || m.SheepStatus.Name.Contains(searchValue));
                }
                recordsTotal = sheepData.Count();
                var data = await sheepData.Skip(skip).Take(pageSize).ToListAsync();
                var jsonData = new 
                { 
                    draw = draw, 
                    recordsFiltered = recordsTotal, 
                    recordsTotal = recordsTotal, 
                    data = data 
                };
                return Ok(jsonData);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
