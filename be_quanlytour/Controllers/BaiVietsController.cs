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
    public class BaiVietsController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public BaiVietsController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/BaiViets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaiViet>>> GetBaiViets()
        {
          if (_context.BaiViets == null)
          {
              return NotFound();
          }
            return await _context.BaiViets.ToListAsync();
        }

        // GET: api/BaiViets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaiViet>> GetBaiViet(int id)
        {
          if (_context.BaiViets == null)
          {
              return NotFound();
          }
            var baiViet = await _context.BaiViets.FindAsync(id);

            if (baiViet == null)
            {
                return NotFound();
            }

            return baiViet;
        }

        // PUT: api/BaiViets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBaiViet(int id, BaiViet baiViet)
        {
            if (id != baiViet.IdBaiViet)
            {
                return BadRequest();
            }

            _context.Entry(baiViet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BaiVietExists(id))
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

        // POST: api/BaiViets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BaiViet>> PostBaiViet(BaiViet baiViet)
        {
          if (_context.BaiViets == null)
          {
              return Problem("Entity set 'QltourDuLichContext.BaiViets'  is null.");
          }
            _context.BaiViets.Add(baiViet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BaiVietExists(baiViet.IdBaiViet))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBaiViet", new { id = baiViet.IdBaiViet }, baiViet);
        }

        // DELETE: api/BaiViets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBaiViet(int id)
        {
            if (_context.BaiViets == null)
            {
                return NotFound();
            }
            var baiViet = await _context.BaiViets.FindAsync(id);
            if (baiViet == null)
            {
                return NotFound();
            }

            _context.BaiViets.Remove(baiViet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BaiVietExists(int id)
        {
            return (_context.BaiViets?.Any(e => e.IdBaiViet == id)).GetValueOrDefault();
        }
    }
}
