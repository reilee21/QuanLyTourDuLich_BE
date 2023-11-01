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
    public class MucHoiDapsController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public MucHoiDapsController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/MucHoiDaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MucHoiDap>>> GetMucHoiDaps()
        {
          if (_context.MucHoiDaps == null)
          {
              return NotFound();
          }
            return await _context.MucHoiDaps.ToListAsync();
        }

        // GET: api/MucHoiDaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MucHoiDap>> GetMucHoiDap(int id)
        {
          if (_context.MucHoiDaps == null)
          {
              return NotFound();
          }
            var mucHoiDap = await _context.MucHoiDaps.FindAsync(id);

            if (mucHoiDap == null)
            {
                return NotFound();
            }

            return mucHoiDap;
        }

        // PUT: api/MucHoiDaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMucHoiDap(int id, MucHoiDap mucHoiDap)
        {
            if (id != mucHoiDap.IdHoiDap)
            {
                return BadRequest();
            }

            _context.Entry(mucHoiDap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MucHoiDapExists(id))
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

        // POST: api/MucHoiDaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MucHoiDap>> PostMucHoiDap(MucHoiDap mucHoiDap)
        {
          if (_context.MucHoiDaps == null)
          {
              return Problem("Entity set 'QltourDuLichContext.MucHoiDaps'  is null.");
          }
            _context.MucHoiDaps.Add(mucHoiDap);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MucHoiDapExists(mucHoiDap.IdHoiDap))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMucHoiDap", new { id = mucHoiDap.IdHoiDap }, mucHoiDap);
        }

        // DELETE: api/MucHoiDaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMucHoiDap(int id)
        {
            if (_context.MucHoiDaps == null)
            {
                return NotFound();
            }
            var mucHoiDap = await _context.MucHoiDaps.FindAsync(id);
            if (mucHoiDap == null)
            {
                return NotFound();
            }

            _context.MucHoiDaps.Remove(mucHoiDap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MucHoiDapExists(int id)
        {
            return (_context.MucHoiDaps?.Any(e => e.IdHoiDap == id)).GetValueOrDefault();
        }
    }
}
