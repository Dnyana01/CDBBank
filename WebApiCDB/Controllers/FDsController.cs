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
    public class FDsController : ControllerBase
    {
        private readonly WebApiCDBContext _context;

        public FDsController(WebApiCDBContext context)
        {
            _context = context;
        }

        // GET: api/FDs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FD>>> GetFD()
        {
            return await _context.FD.ToListAsync();
        }

        // GET: api/FDs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FD>> GetFD(int id)
        {
            var fD = await _context.FD.FindAsync(id);

            if (fD == null)
            {
                return NotFound();
            }

            return fD;
        }

        // PUT: api/FDs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFD(int id, FD fD)
        {
            if (id != fD.FdId)
            {
                return BadRequest();
            }

            _context.Entry(fD).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FDExists(id))
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

        // POST: api/FDs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FD>> PostFD(FD fD)
        {
            _context.FD.Add(fD);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFD", new { id = fD.FdId }, fD);
        }

        // DELETE: api/FDs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFD(int id)
        {
            var fD = await _context.FD.FindAsync(id);
            if (fD == null)
            {
                return NotFound();
            }

            _context.FD.Remove(fD);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FDExists(int id)
        {
            return _context.FD.Any(e => e.FdId == id);
        }
    }
}
