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
    public class LoaiPhongsController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public LoaiPhongsController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/LoaiPhongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiPhong>>> GetLoaiPhongs()
        {
          if (_context.LoaiPhongs == null)
          {
              return NotFound();
          }
            return await _context.LoaiPhongs.ToListAsync();
        }

        // GET: api/LoaiPhongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoaiPhong>> GetLoaiPhong(int id)
        {
          if (_context.LoaiPhongs == null)
          {
              return NotFound();
          }
            var loaiPhong = await _context.LoaiPhongs.FindAsync(id);

            if (loaiPhong == null)
            {
                return NotFound();
            }

            return loaiPhong;
        }

        // PUT: api/LoaiPhongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoaiPhong(int id, LoaiPhong loaiPhong)
        {
            if (id != loaiPhong.IdLoaiPhong)
            {
                return BadRequest();
            }

            _context.Entry(loaiPhong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiPhongExists(id))
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

        // POST: api/LoaiPhongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoaiPhong>> PostLoaiPhong(LoaiPhong loaiPhong)
        {
          if (_context.LoaiPhongs == null)
          {
              return Problem("Entity set 'QltourDuLichContext.LoaiPhongs'  is null.");
          }
            _context.LoaiPhongs.Add(loaiPhong);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoaiPhongExists(loaiPhong.IdLoaiPhong))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLoaiPhong", new { id = loaiPhong.IdLoaiPhong }, loaiPhong);
        }

        // DELETE: api/LoaiPhongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoaiPhong(int id)
        {
            if (_context.LoaiPhongs == null)
            {
                return NotFound();
            }
            var loaiPhong = await _context.LoaiPhongs.FindAsync(id);
            if (loaiPhong == null)
            {
                return NotFound();
            }

            _context.LoaiPhongs.Remove(loaiPhong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoaiPhongExists(int id)
        {
            return (_context.LoaiPhongs?.Any(e => e.IdLoaiPhong == id)).GetValueOrDefault();
        }
    }
}
