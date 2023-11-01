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
    public class TourKhuyenMaisController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public TourKhuyenMaisController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/TourKhuyenMais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourKhuyenMai>>> GetTourKhuyenMais()
        {
          if (_context.TourKhuyenMais == null)
          {
              return NotFound();
          }
            return await _context.TourKhuyenMais.ToListAsync();
        }

        // GET: api/TourKhuyenMais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TourKhuyenMai>> GetTourKhuyenMai(int id)
        {
          if (_context.TourKhuyenMais == null)
          {
              return NotFound();
          }
            var tourKhuyenMai = await _context.TourKhuyenMais.FindAsync(id);

            if (tourKhuyenMai == null)
            {
                return NotFound();
            }

            return tourKhuyenMai;
        }

        // PUT: api/TourKhuyenMais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTourKhuyenMai(int id, TourKhuyenMai tourKhuyenMai)
        {
            if (id != tourKhuyenMai.MaKm)
            {
                return BadRequest();
            }

            _context.Entry(tourKhuyenMai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourKhuyenMaiExists(id))
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

        // POST: api/TourKhuyenMais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TourKhuyenMai>> PostTourKhuyenMai(TourKhuyenMai tourKhuyenMai)
        {
          if (_context.TourKhuyenMais == null)
          {
              return Problem("Entity set 'QltourDuLichContext.TourKhuyenMais'  is null.");
          }
            _context.TourKhuyenMais.Add(tourKhuyenMai);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TourKhuyenMaiExists(tourKhuyenMai.MaKm))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTourKhuyenMai", new { id = tourKhuyenMai.MaKm }, tourKhuyenMai);
        }

        // DELETE: api/TourKhuyenMais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourKhuyenMai(int id)
        {
            if (_context.TourKhuyenMais == null)
            {
                return NotFound();
            }
            var tourKhuyenMai = await _context.TourKhuyenMais.FindAsync(id);
            if (tourKhuyenMai == null)
            {
                return NotFound();
            }

            _context.TourKhuyenMais.Remove(tourKhuyenMai);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TourKhuyenMaiExists(int id)
        {
            return (_context.TourKhuyenMais?.Any(e => e.MaKm == id)).GetValueOrDefault();
        }
    }
}
