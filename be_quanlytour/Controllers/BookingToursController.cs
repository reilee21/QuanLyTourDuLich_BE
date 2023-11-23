using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using be_quanlytour.Models;
using Newtonsoft.Json;
using be_quanlytour.Helper;

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
            var bookingTour = await _context.BookingTours
          .Include(x => x.HanhKhaches)
          .Include(x => x.MaTourNavigation)
          .FirstOrDefaultAsync(x => x.IdBookingTour == id);

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

        [HttpPost]
        public async Task<ActionResult<BookingTour>> PostBookingTour([FromForm] IBookingTour bookingTour)
        {
            List<HanhKhach> hk = new List<HanhKhach>();
            IBooking bk = new IBooking();
            hk = JsonConvert.DeserializeObject<List<HanhKhach>>(bookingTour.HanhKhaches);
            bk = JsonConvert.DeserializeObject<IBooking>(bookingTour.Booking);

            Booking newbk = new Booking();

             string t = "BKT-" + Utils.TaoMaTuDong(6);
            while (BookingExists(t))
            {
                t = "BKT-" + Utils.TaoMaTuDong(6);
            }
            newbk.IdBooking = t;newbk.ThoiDiemBook = bk.ThoiDiemBook; newbk.LoaiBooking = bk.LoaiBooking; newbk.MaKh = bk.MaKh; newbk.GiaTri = bk.GiaTri;
            if (bk.MaVoucher!=null && bk.MaVoucher.Length>5)
                newbk.MaVoucher = bk.MaVoucher; 
            if (bk.MaNv!= null &&bk.MaNv.Length >5)
                newbk.MaNv = bk.MaNv;
            _context.Bookings.Add(newbk);
            BookingTour newbkt = new BookingTour() { MaTour = bookingTour.MaTour, IdBooking = newbk.IdBooking };
            _context.BookingTours.Add(newbkt);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingTourExists(newbkt.IdBookingTour))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            if (newbkt.IdBookingTour != null)
            {
                foreach (var item in hk)
                {

                    item.IdBookingTour = newbkt.IdBookingTour;
                    _context.HanhKhaches.Add(new HanhKhach() { IdBookingTour = item.IdBookingTour, TenHanhKhach = item.TenHanhKhach, PhongRieng = item.PhongRieng, LoaiHanhKhach = item.LoaiHanhKhach });
                }
                var tourToUpdate = await _context.Tours.FirstOrDefaultAsync(t => t.MaTour == newbkt.MaTour);
                if (tourToUpdate != null)
                {
                    tourToUpdate.SoLuongNguoiDaDat +=(byte) hk.Count;
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                return StatusCode(405);
            }
            await _context.SaveChangesAsync();




            return Ok(newbkt.IdBooking  );

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
        private bool BookingExists(string id)
        {
            return (_context.Bookings?.Any(e => e.IdBooking == id)).GetValueOrDefault();
        }
  
    }
}
