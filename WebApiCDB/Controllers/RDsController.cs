using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCDB.Data;
using WebApiCDB.Models;

namespace WebApiCDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RDsController : ControllerBase
    {
        private readonly WebApiCDBContext _context;

        public RDsController(WebApiCDBContext context)
        {
            _context = context;
        }

        // GET: api/RDs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RD>>> GetRD()
        {
            return await _context.RD.ToListAsync();
        }

        // GET: api/RDs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RD>> GetRD(int id)
        {
            var rD = await _context.RD.FindAsync(id);

            if (rD == null)
            {
                return NotFound();
            }

            return rD;
        }

        // PUT: api/RDs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRD(int id, RD rD)
        {
            if (id != rD.RdId)
            {
                return BadRequest();
            }

            _context.Entry(rD).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RDExists(id))
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

        // POST: api/RDs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RD>> PostRD(RD rD)
        {
            _context.RD.Add(rD);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRD", new { id = rD.RdId }, rD);
        }

        // DELETE: api/RDs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRD(int id)
        {
            var rD = await _context.RD.FindAsync(id);
            if (rD == null)
            {
                return NotFound();
            }

            _context.RD.Remove(rD);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RDExists(int id)
        {
            return _context.RD.Any(e => e.RdId == id);
        }
    }
}
