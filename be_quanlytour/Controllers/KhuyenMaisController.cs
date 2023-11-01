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
    public class KhuyenMaisController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public KhuyenMaisController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/KhuyenMais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhuyenMai>>> GetKhuyenMais()
        {
          if (_context.KhuyenMais == null)
          {
              return NotFound();
          }
            return await _context.KhuyenMais.ToListAsync();
        }

        // GET: api/KhuyenMais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhuyenMai>> GetKhuyenMai(int id)
        {
          if (_context.KhuyenMais == null)
          {
              return NotFound();
          }
            var khuyenMai = await _context.KhuyenMais.FindAsync(id);

            if (khuyenMai == null)
            {
                return NotFound();
            }

            return khuyenMai;
        }

        // PUT: api/KhuyenMais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhuyenMai(int id, KhuyenMai khuyenMai)
        {
            if (id != khuyenMai.MaKm)
            {
                return BadRequest();
            }

            _context.Entry(khuyenMai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhuyenMaiExists(id))
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

        // POST: api/KhuyenMais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhuyenMai>> PostKhuyenMai(KhuyenMai khuyenMai)
        {
          if (_context.KhuyenMais == null)
          {
              return Problem("Entity set 'QltourDuLichContext.KhuyenMais'  is null.");
          }
            _context.KhuyenMais.Add(khuyenMai);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KhuyenMaiExists(khuyenMai.MaKm))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKhuyenMai", new { id = khuyenMai.MaKm }, khuyenMai);
        }

        // DELETE: api/KhuyenMais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhuyenMai(int id)
        {
            if (_context.KhuyenMais == null)
            {
                return NotFound();
            }
            var khuyenMai = await _context.KhuyenMais.FindAsync(id);
            if (khuyenMai == null)
            {
                return NotFound();
            }

            _context.KhuyenMais.Remove(khuyenMai);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhuyenMaiExists(int id)
        {
            return (_context.KhuyenMais?.Any(e => e.MaKm == id)).GetValueOrDefault();
        }
    }
}
