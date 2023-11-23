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

        [HttpPost]
        public async Task<ActionResult<IBookingK>> PostBookingK([FromForm]IBookingK bookingK)
        {
          if (_context.BookingKs == null)
          {
              return Problem("Entity set 'QltourDuLichContext.BookingKs'  is null.");
          }
            IBooking bk = new IBooking();
            bk = JsonConvert.DeserializeObject<IBooking>(bookingK.Booking);

            Booking newbk = new Booking();
            string t = "BKS-" + Utils.TaoMaTuDong(6);
            while (BookingExists(t))
            {
                t = "BKS-" + Utils.TaoMaTuDong(6);
            }
            newbk.IdBooking = t; newbk.ThoiDiemBook = bk.ThoiDiemBook; newbk.LoaiBooking = bk.LoaiBooking; newbk.MaKh = bk.MaKh; newbk.GiaTri = bk.GiaTri;
            if (bk.MaVoucher != null && bk.MaVoucher.Length > 5)
                newbk.MaVoucher = bk.MaVoucher;
            if (bk.MaNv != null && bk.MaNv.Length > 5)
                newbk.MaNv = bk.MaNv;
            
            _context.Bookings.Add(newbk);
            int[] phongsArray = JsonConvert.DeserializeObject<int[]>(bookingK.Phongs);
            foreach (int i in phongsArray)
            {
                _context.BookingKs.Add(new BookingK() { NgayNhan = bookingK.NgayNhan, NgayTra = bookingK.NgayTra, IdLoaiPhong = i, IdBooking = newbk.IdBooking });
                var loaiPhong = await _context.LoaiPhongs.FindAsync(i);
                if (loaiPhong != null)
                {
                    if (loaiPhong.SoPhong > 0)
                    {
                        loaiPhong.SoPhong--;
                    }
                    else
                    {
                        return Conflict();
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtAction("GetBookingK", new { id = newbk.IdBooking }, bookingK);
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
        private bool BookingExists(string id)
        {
            return (_context.Bookings?.Any(e => e.IdBooking == id)).GetValueOrDefault();
        }
    }
}
