using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using be_quanlytour.Models;

namespace be_quanlytour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuyenBaysController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public ChuyenBaysController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/ChuyenBays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChuyenBay>>> GetChuyenBays()
        {
          if (_context.ChuyenBays == null)
          {
              return NotFound();
          }
            return await _context.ChuyenBays.ToListAsync();
        }

        // GET: api/ChuyenBays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChuyenBay>> GetChuyenBay(string id)
        {
          if (_context.ChuyenBays == null)
          {
              return NotFound();
          }
            var chuyenBay = await _context.ChuyenBays.FindAsync(id);

            if (chuyenBay == null)
            {
                return NotFound();
            }

            return chuyenBay;
        }

        // PUT: api/ChuyenBays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChuyenBay(string id, ChuyenBay chuyenBay)
        {
            if (id != chuyenBay.MaChuyenBay)
            {
                return BadRequest();
            }

            _context.Entry(chuyenBay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChuyenBayExists(id))
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

        // POST: api/ChuyenBays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChuyenBay>> PostChuyenBay(ChuyenBay chuyenBay)
        {
          if (_context.ChuyenBays == null)
          {
              return Problem("Entity set 'QltourDuLichContext.ChuyenBays'  is null.");
          }
            _context.ChuyenBays.Add(chuyenBay);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChuyenBayExists(chuyenBay.MaChuyenBay))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChuyenBay", new { id = chuyenBay.MaChuyenBay }, chuyenBay);
        }

        // DELETE: api/ChuyenBays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChuyenBay(string id)
        {
            if (_context.ChuyenBays == null)
            {
                return NotFound();
            }
            var chuyenBay = await _context.ChuyenBays.FindAsync(id);
            if (chuyenBay == null)
            {
                return NotFound();
            }

            _context.ChuyenBays.Remove(chuyenBay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChuyenBayExists(string id)
        {
            return (_context.ChuyenBays?.Any(e => e.MaChuyenBay == id)).GetValueOrDefault();
        }
    }
}
