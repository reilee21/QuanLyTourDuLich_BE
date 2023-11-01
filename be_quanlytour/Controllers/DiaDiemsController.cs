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
    public class DiaDiemsController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public DiaDiemsController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/DiaDiems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaDiem>>> GetDiaDiems()
        {
          if (_context.DiaDiems == null)
          {
              return NotFound();
          }
            return await _context.DiaDiems.ToListAsync();
        }

        // GET: api/DiaDiems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiaDiem>> GetDiaDiem(int id)
        {
          if (_context.DiaDiems == null)
          {
              return NotFound();
          }
            var diaDiem = await _context.DiaDiems.FindAsync(id);

            if (diaDiem == null)
            {
                return NotFound();
            }

            return diaDiem;
        }

        // PUT: api/DiaDiems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiaDiem(int id, DiaDiem diaDiem)
        {
            if (id != diaDiem.IdDiaDiem)
            {
                return BadRequest();
            }

            _context.Entry(diaDiem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiaDiemExists(id))
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

        // POST: api/DiaDiems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiaDiem>> PostDiaDiem(DiaDiem diaDiem)
        {
          if (_context.DiaDiems == null)
          {
              return Problem("Entity set 'QltourDuLichContext.DiaDiems'  is null.");
          }
            _context.DiaDiems.Add(diaDiem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiaDiemExists(diaDiem.IdDiaDiem))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDiaDiem", new { id = diaDiem.IdDiaDiem }, diaDiem);
        }

        // DELETE: api/DiaDiems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiaDiem(int id)
        {
            if (_context.DiaDiems == null)
            {
                return NotFound();
            }
            var diaDiem = await _context.DiaDiems.FindAsync(id);
            if (diaDiem == null)
            {
                return NotFound();
            }

            _context.DiaDiems.Remove(diaDiem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiaDiemExists(int id)
        {
            return (_context.DiaDiems?.Any(e => e.IdDiaDiem == id)).GetValueOrDefault();
        }
    }
}
