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
    public class KhachSansController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public KhachSansController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/KhachSans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachSan>>> GetKhachSans()
        {
          if (_context.KhachSans == null)
          {
              return NotFound();
          }
            return await _context.KhachSans.ToListAsync();
        }

        // GET: api/KhachSans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachSan>> GetKhachSan(string id)
        {
          if (_context.KhachSans == null)
          {
              return NotFound();
          }
            var khachSan = await _context.KhachSans.FindAsync(id);

            if (khachSan == null)
            {
                return NotFound();
            }

            return khachSan;
        }

        // PUT: api/KhachSans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachSan(string id, KhachSan khachSan)
        {
            if (id != khachSan.IdKhachSan)
            {
                return BadRequest();
            }

            _context.Entry(khachSan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachSanExists(id))
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

        // POST: api/KhachSans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhachSan>> PostKhachSan(KhachSan khachSan)
        {
          if (_context.KhachSans == null)
          {
              return Problem("Entity set 'QltourDuLichContext.KhachSans'  is null.");
          }
            _context.KhachSans.Add(khachSan);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KhachSanExists(khachSan.IdKhachSan))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKhachSan", new { id = khachSan.IdKhachSan }, khachSan);
        }

        // DELETE: api/KhachSans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachSan(string id)
        {
            if (_context.KhachSans == null)
            {
                return NotFound();
            }
            var khachSan = await _context.KhachSans.FindAsync(id);
            if (khachSan == null)
            {
                return NotFound();
            }

            _context.KhachSans.Remove(khachSan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachSanExists(string id)
        {
            return (_context.KhachSans?.Any(e => e.IdKhachSan == id)).GetValueOrDefault();
        }
    }
}
