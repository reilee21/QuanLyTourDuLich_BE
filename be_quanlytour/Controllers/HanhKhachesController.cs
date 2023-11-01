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
    public class HanhKhachesController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public HanhKhachesController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/HanhKhaches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HanhKhach>>> GetHanhKhaches()
        {
          if (_context.HanhKhaches == null)
          {
              return NotFound();
          }
            return await _context.HanhKhaches.ToListAsync();
        }

        // GET: api/HanhKhaches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HanhKhach>> GetHanhKhach(int id)
        {
          if (_context.HanhKhaches == null)
          {
              return NotFound();
          }
            var hanhKhach = await _context.HanhKhaches.FindAsync(id);

            if (hanhKhach == null)
            {
                return NotFound();
            }

            return hanhKhach;
        }

        // PUT: api/HanhKhaches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHanhKhach(int id, HanhKhach hanhKhach)
        {
            if (id != hanhKhach.IdHanhKhach)
            {
                return BadRequest();
            }

            _context.Entry(hanhKhach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HanhKhachExists(id))
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

        // POST: api/HanhKhaches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HanhKhach>> PostHanhKhach(HanhKhach hanhKhach)
        {
          if (_context.HanhKhaches == null)
          {
              return Problem("Entity set 'QltourDuLichContext.HanhKhaches'  is null.");
          }
            _context.HanhKhaches.Add(hanhKhach);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HanhKhachExists(hanhKhach.IdHanhKhach))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHanhKhach", new { id = hanhKhach.IdHanhKhach }, hanhKhach);
        }

        // DELETE: api/HanhKhaches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHanhKhach(int id)
        {
            if (_context.HanhKhaches == null)
            {
                return NotFound();
            }
            var hanhKhach = await _context.HanhKhaches.FindAsync(id);
            if (hanhKhach == null)
            {
                return NotFound();
            }

            _context.HanhKhaches.Remove(hanhKhach);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HanhKhachExists(int id)
        {
            return (_context.HanhKhaches?.Any(e => e.IdHanhKhach == id)).GetValueOrDefault();
        }
    }
}
