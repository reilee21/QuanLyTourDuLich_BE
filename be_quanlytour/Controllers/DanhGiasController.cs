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
    public class DanhGiasController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public DanhGiasController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/DanhGias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DanhGia>>> GetDanhGia()
        {
          if (_context.DanhGia == null)
          {
              return NotFound();
          }
            return await _context.DanhGia.ToListAsync();
        }

        // GET: api/DanhGias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhGia>> GetDanhGia(int id)
        {
          if (_context.DanhGia == null)
          {
              return NotFound();
          }
            var danhGia = await _context.DanhGia.FindAsync(id);

            if (danhGia == null)
            {
                return NotFound();
            }

            return danhGia;
        }

        // PUT: api/DanhGias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDanhGia(int id, DanhGia danhGia)
        {
            if (id != danhGia.MaDanhGia)
            {
                return BadRequest();
            }

            _context.Entry(danhGia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhGiaExists(id))
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

        // POST: api/DanhGias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DanhGia>> PostDanhGia(DanhGia danhGia)
        {
          if (_context.DanhGia == null)
          {
              return Problem("Entity set 'QltourDuLichContext.DanhGia'  is null.");
          }
            _context.DanhGia.Add(danhGia);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DanhGiaExists(danhGia.MaDanhGia))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDanhGia", new { id = danhGia.MaDanhGia }, danhGia);
        }

        // DELETE: api/DanhGias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanhGia(int id)
        {
            if (_context.DanhGia == null)
            {
                return NotFound();
            }
            var danhGia = await _context.DanhGia.FindAsync(id);
            if (danhGia == null)
            {
                return NotFound();
            }

            _context.DanhGia.Remove(danhGia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DanhGiaExists(int id)
        {
            return (_context.DanhGia?.Any(e => e.MaDanhGia == id)).GetValueOrDefault();
        }
    }
}
