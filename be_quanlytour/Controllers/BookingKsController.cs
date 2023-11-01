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
    public class BookingKsController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public BookingKsController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/BookingKs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingK>>> GetBookingKs()
        {
          if (_context.BookingKs == null)
          {
              return NotFound();
          }
            return await _context.BookingKs.ToListAsync();
        }

        // GET: api/BookingKs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingK>> GetBookingK(int id)
        {
          if (_context.BookingKs == null)
          {
              return NotFound();
          }
            var bookingK = await _context.BookingKs.FindAsync(id);

            if (bookingK == null)
            {
                return NotFound();
            }

            return bookingK;
        }

        // PUT: api/BookingKs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingK(int id, BookingK bookingK)
        {
            if (id != bookingK.IdBookingKs)
            {
                return BadRequest();
            }

            _context.Entry(bookingK).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingKExists(id))
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

        // POST: api/BookingKs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookingK>> PostBookingK(BookingK bookingK)
        {
          if (_context.BookingKs == null)
          {
              return Problem("Entity set 'QltourDuLichContext.BookingKs'  is null.");
          }
            _context.BookingKs.Add(bookingK);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingKExists(bookingK.IdBookingKs))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookingK", new { id = bookingK.IdBookingKs }, bookingK);
        }

        // DELETE: api/BookingKs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingK(int id)
        {
            if (_context.BookingKs == null)
            {
                return NotFound();
            }
            var bookingK = await _context.BookingKs.FindAsync(id);
            if (bookingK == null)
            {
                return NotFound();
            }

            _context.BookingKs.Remove(bookingK);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingKExists(int id)
        {
            return (_context.BookingKs?.Any(e => e.IdBookingKs == id)).GetValueOrDefault();
        }
    }
}
