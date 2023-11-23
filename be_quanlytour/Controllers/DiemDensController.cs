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
    public class DiemDensController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public DiemDensController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/DiemDens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiemDen>>> GetDiemDens()
        {
          if (_context.DiemDens == null)
          {
              return NotFound();
          }
            return await _context.DiemDens.ToListAsync();
        }

        // GET: api/DiemDens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiemDen>> GetDiemDen(string id)
        {
          if (_context.DiemDens == null)
          {
              return NotFound();
          }
            var diemDen = await _context.DiemDens.FindAsync(id);

            if (diemDen == null)
            {
                return NotFound();
            }

            return diemDen;
        }

        // PUT: api/DiemDens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiemDen(string id, DiemDen diemDen)
        {
            if (id != diemDen.IdDiemDen)
            {
                return BadRequest();
            }

            _context.Entry(diemDen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiemDenExists(id))
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

        // POST: api/DiemDens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiemDen>> PostDiemDen(DiemDen diemDen)
        {
          if (_context.DiemDens == null)
          {
              return Problem("Entity set 'QltourDuLichContext.DiemDens'  is null.");
          }
            diemDen.IdDiemDen = TaoMaDD();
            _context.DiemDens.Add(diemDen);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiemDenExists(diemDen.IdDiemDen))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDiemDen", new { id = diemDen.IdDiemDen }, diemDen);
        }

        // DELETE: api/DiemDens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiemDen(string id)
        {
            if (_context.DiemDens == null)
            {
                return NotFound();
            }
            var diemDen = await _context.DiemDens.FindAsync(id);
            if (diemDen == null)
            {
                return NotFound();
            }

            _context.DiemDens.Remove(diemDen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiemDenExists(string id)
        {
            return (_context.DiemDens?.Any(e => e.IdDiemDen == id)).GetValueOrDefault();
        }
        private string TaoMaDD()
        {

            Random random = new Random();
            const string chars = "0123456789";

            string randomString = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());

            while (DiemDenExists(randomString))
            {
                randomString = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
            }

            return randomString;
        }
    }
}
