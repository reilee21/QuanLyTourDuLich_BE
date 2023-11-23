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
    public class LichTrinhsController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public LichTrinhsController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/LichTrinhs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichTrinh>>> GetLichTrinhs()
        {
          if (_context.LichTrinhs == null)
          {
              return NotFound();
          }
            return await _context.LichTrinhs.ToListAsync();
        }

        // GET: api/LichTrinhs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LichTrinh>> GetLichTrinh(string id)
        {
          if (_context.LichTrinhs == null)
          {
              return NotFound();
          }
            var lichTrinh = await _context.LichTrinhs.FindAsync(id);

            if (lichTrinh == null)
            {
                return NotFound();
            }

            return lichTrinh;
        }

        // PUT: api/LichTrinhs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichTrinh(string id, LichTrinh lichTrinh)
        {
            if (id != lichTrinh.IdDiemDen)
            {
                return BadRequest();
            }

            _context.Entry(lichTrinh).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichTrinhExists(id))
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

        // POST: api/LichTrinhs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LichTrinh>> PostLichTrinh(LichTrinh lichTrinh)
        {
          if (_context.LichTrinhs == null)
          {
              return Problem("Entity set 'QltourDuLichContext.LichTrinhs'  is null.");
          }
            _context.LichTrinhs.Add(lichTrinh);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LichTrinhExists(lichTrinh.IdDiemDen))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLichTrinh", new { id = lichTrinh.IdDiemDen }, lichTrinh);
        }

        // DELETE: api/LichTrinhs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichTrinh(string id)
        {
            if (_context.LichTrinhs == null)
            {
                return NotFound();
            }
            var lichTrinh = await _context.LichTrinhs.FindAsync(id);
            if (lichTrinh == null)
            {
                return NotFound();
            }

            _context.LichTrinhs.Remove(lichTrinh);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LichTrinhExists(string id)
        {
            return (_context.LichTrinhs?.Any(e => e.IdDiemDen == id)).GetValueOrDefault();
        }

    


    }
}
