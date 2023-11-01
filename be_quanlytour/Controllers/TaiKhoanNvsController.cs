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
    public class TaiKhoanNvsController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public TaiKhoanNvsController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/TaiKhoanNvs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiKhoanNv>>> GetTaiKhoanNvs()
        {
          if (_context.TaiKhoanNvs == null)
          {
              return NotFound();
          }
            return await _context.TaiKhoanNvs.ToListAsync();
        }

        // GET: api/TaiKhoanNvs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaiKhoanNv>> GetTaiKhoanNv(string id)
        {
          if (_context.TaiKhoanNvs == null)
          {
              return NotFound();
          }
            var taiKhoanNv = await _context.TaiKhoanNvs.FindAsync(id);

            if (taiKhoanNv == null)
            {
                return NotFound();
            }

            return taiKhoanNv;
        }

        // PUT: api/TaiKhoanNvs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaiKhoanNv(string id, TaiKhoanNv taiKhoanNv)
        {
            if (id != taiKhoanNv.IdTaiKhoan)
            {
                return BadRequest();
            }

            _context.Entry(taiKhoanNv).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaiKhoanNvExists(id))
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

        // POST: api/TaiKhoanNvs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaiKhoanNv>> PostTaiKhoanNv(TaiKhoanNv taiKhoanNv)
        {
          if (_context.TaiKhoanNvs == null)
          {
              return Problem("Entity set 'QltourDuLichContext.TaiKhoanNvs'  is null.");
          }
            _context.TaiKhoanNvs.Add(taiKhoanNv);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaiKhoanNvExists(taiKhoanNv.IdTaiKhoan))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTaiKhoanNv", new { id = taiKhoanNv.IdTaiKhoan }, taiKhoanNv);
        }

        // DELETE: api/TaiKhoanNvs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiKhoanNv(string id)
        {
            if (_context.TaiKhoanNvs == null)
            {
                return NotFound();
            }
            var taiKhoanNv = await _context.TaiKhoanNvs.FindAsync(id);
            if (taiKhoanNv == null)
            {
                return NotFound();
            }

            _context.TaiKhoanNvs.Remove(taiKhoanNv);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaiKhoanNvExists(string id)
        {
            return (_context.TaiKhoanNvs?.Any(e => e.IdTaiKhoan == id)).GetValueOrDefault();
        }
    }
}
