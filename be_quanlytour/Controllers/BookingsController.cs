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
    public class BookingsController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public BookingsController(QltourDuLichContext context)
        {
            _context = context;
        } 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
          if (_context.Bookings == null)
          {
              return NotFound();
          }
            return await _context.Bookings.ToListAsync();
        }

     
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(string id)
        {
            if (_context.Bookings == null)
            {
                return NotFound();
            }

            var bk = await _context.Bookings.Include(x => x.MaKhNavigation).Include(t => t.BookingTours).Include(t => t.BookingKs).FirstOrDefaultAsync(t => t.IdBooking ==id);

            return bk;
        }
        [HttpGet("HanhKhach")]
        public async Task<ActionResult<IEnumerable<HanhKhach>>> GetHanhKhach(int idBookingTour)
        {
            if (_context.HanhKhaches == null)
            {
                return NotFound();
            }
            var hks = await _context.HanhKhaches.Where(x => x.IdBookingTour == idBookingTour).ToListAsync();


            return hks;

        }

            [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(string id, [FromForm] bool isPaid)
        {
            var bk = await _context.Bookings.FirstOrDefaultAsync(t => t.IdBooking ==id);
            if(bk == null)
            {
                return NotFound();
            }

            bk.ThanhToan = isPaid;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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


        private bool BookingExists(string id)
        {
            return (_context.Bookings?.Any(e => e.IdBooking == id)).GetValueOrDefault();
        }
    }
}
