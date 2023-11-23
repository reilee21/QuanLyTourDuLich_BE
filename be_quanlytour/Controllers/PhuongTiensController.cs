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
    public class PhuongTiensController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public PhuongTiensController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/PhuongTiens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhuongTien>>> GetPhuongTiens()
        {
          if (_context.PhuongTiens == null)
          {
              return NotFound();
          }
            return await _context.PhuongTiens.ToListAsync();
        }

        // GET: api/PhuongTiens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhuongTien>> GetPhuongTien(int id)
        {
          if (_context.PhuongTiens == null)
          {
              return NotFound();
          }
            var phuongTien = await _context.PhuongTiens.FindAsync(id);

            if (phuongTien == null)
            {
                return NotFound();
            }

            return phuongTien;
        }

        // PUT: api/PhuongTiens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhuongTien(int id, PhuongTien phuongTien)
        {
            if (id != phuongTien.IdPhuongTien)
            {
                return BadRequest();
            }

            _context.Entry(phuongTien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhuongTienExists(id))
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

        // POST: api/PhuongTiens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhuongTien>> PostPhuongTien(PhuongTien phuongTien)
        {
          if (_context.PhuongTiens == null)
          {
              return Problem("Entity set 'QltourDuLichContext.PhuongTiens'  is null.");
          }
           
            _context.PhuongTiens.Add(phuongTien);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhuongTienExists(phuongTien.IdPhuongTien))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhuongTien", new { id = phuongTien.IdPhuongTien }, phuongTien);
        }

        // DELETE: api/PhuongTiens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhuongTien(int id)
        {
            if (_context.PhuongTiens == null)
            {
                return NotFound();
            }
            var phuongTien = await _context.PhuongTiens.FindAsync(id);
            if (phuongTien == null)
            {
                return NotFound();
            }

            _context.PhuongTiens.Remove(phuongTien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhuongTienExists(int id)
        {
            return (_context.PhuongTiens?.Any(e => e.IdPhuongTien == id)).GetValueOrDefault();
        }
    }
}
