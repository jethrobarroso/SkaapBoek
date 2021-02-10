using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using SkaapBoek.DAL;

namespace SkaapBoek.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSheepApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestSheepApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TestSheepApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sheep>>> GetSheepSet()
        {
            return await _context.SheepSet.ToListAsync();
        }

        // GET: api/TestSheepApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sheep>> GetSheep(int id)
        {
            var sheep = await _context.SheepSet.FindAsync(id);

            if (sheep == null)
            {
                return NotFound();
            }

            return sheep;
        }

        // PUT: api/TestSheepApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSheep(int id, Sheep sheep)
        {
            if (id != sheep.Id)
            {
                return BadRequest();
            }

            _context.Entry(sheep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SheepExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TestSheepApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sheep>> PostSheep(Sheep sheep)
        {
            _context.SheepSet.Add(sheep);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSheep", new { id = sheep.Id }, sheep);
        }

        // DELETE: api/TestSheepApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sheep>> DeleteSheep(int id)
        {
            var sheep = await _context.SheepSet.FindAsync(id);
            if (sheep == null)
            {
                return NotFound();
            }

            _context.SheepSet.Remove(sheep);
            await _context.SaveChangesAsync();

            return sheep;
        }

        private bool SheepExists(int id)
        {
            return _context.SheepSet.Any(e => e.Id == id);
        }
    }
}
