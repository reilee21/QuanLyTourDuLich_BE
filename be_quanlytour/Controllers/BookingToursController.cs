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
    public class BookingToursController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public BookingToursController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/BookingTours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingTour>>> GetBookingTours()
        {
          if (_context.BookingTours == null)
          {
              return NotFound();
          }
            return await _context.BookingTours.ToListAsync();
        }

        // GET: api/BookingTours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingTour>> GetBookingTour(int id)
        {
          if (_context.BookingTours == null)
          {
              return NotFound();
          }
            var bookingTour = await _context.BookingTours.FindAsync(id);

            if (bookingTour == null)
            {
                return NotFound();
            }

            return bookingTour;
        }

        // PUT: api/BookingTours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingTour(int id, BookingTour bookingTour)
        {
            if (id != bookingTour.IdBookingTour)
            {
                return BadRequest();
            }

            _context.Entry(bookingTour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingTourExists(id))
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

        // POST: api/BookingTours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookingTour>> PostBookingTour(BookingTour bookingTour)
        {
          if (_context.BookingTours == null)
          {
              return Problem("Entity set 'QltourDuLichContext.BookingTours'  is null.");
          }
            _context.BookingTours.Add(bookingTour);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingTourExists(bookingTour.IdBookingTour))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookingTour", new { id = bookingTour.IdBookingTour }, bookingTour);
        }

        // DELETE: api/BookingTours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingTour(int id)
        {
            if (_context.BookingTours == null)
            {
                return NotFound();
            }
            var bookingTour = await _context.BookingTours.FindAsync(id);
            if (bookingTour == null)
            {
                return NotFound();
            }

            _context.BookingTours.Remove(bookingTour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingTourExists(int id)
        {
            return (_context.BookingTours?.Any(e => e.IdBookingTour == id)).GetValueOrDefault();
        }
    }
}
