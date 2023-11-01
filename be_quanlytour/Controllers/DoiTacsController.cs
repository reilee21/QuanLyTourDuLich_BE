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
    public class DoiTacsController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public DoiTacsController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/DoiTacs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoiTac>>> GetDoiTacs()
        {
          if (_context.DoiTacs == null)
          {
              return NotFound();
          }
            return await _context.DoiTacs.ToListAsync();
        }

        // GET: api/DoiTacs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoiTac>> GetDoiTac(string id)
        {
          if (_context.DoiTacs == null)
          {
              return NotFound();
          }
            var doiTac = await _context.DoiTacs.FindAsync(id);

            if (doiTac == null)
            {
                return NotFound();
            }

            return doiTac;
        }

        // PUT: api/DoiTacs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoiTac(string id, DoiTac doiTac)
        {
            if (id != doiTac.IdDoiTac)
            {
                return BadRequest();
            }

            _context.Entry(doiTac).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoiTacExists(id))
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

        // POST: api/DoiTacs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DoiTac>> PostDoiTac(DoiTac doiTac)
        {
          if (_context.DoiTacs == null)
          {
              return Problem("Entity set 'QltourDuLichContext.DoiTacs'  is null.");
          }
            _context.DoiTacs.Add(doiTac);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoiTacExists(doiTac.IdDoiTac))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDoiTac", new { id = doiTac.IdDoiTac }, doiTac);
        }

        // DELETE: api/DoiTacs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoiTac(string id)
        {
            if (_context.DoiTacs == null)
            {
                return NotFound();
            }
            var doiTac = await _context.DoiTacs.FindAsync(id);
            if (doiTac == null)
            {
                return NotFound();
            }

            _context.DoiTacs.Remove(doiTac);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoiTacExists(string id)
        {
            return (_context.DoiTacs?.Any(e => e.IdDoiTac == id)).GetValueOrDefault();
        }
    }
}
