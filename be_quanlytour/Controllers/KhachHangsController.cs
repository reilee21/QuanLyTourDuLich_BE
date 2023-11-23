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
    public class KhachHangsController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public KhachHangsController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/KhachHangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetKhachHangs()
        {
            if (_context.KhachHangs == null)
            {
                return NotFound();
            }
            return await _context.KhachHangs.ToListAsync();
        }

        // GET: api/KhachHangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHang>> GetKhachHang(string id)
        {
            if (_context.KhachHangs == null)
            {
                return NotFound();
            }
            var khachHang = await _context.KhachHangs.FindAsync(id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return khachHang;
        }

        // PUT: api/KhachHangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(string id, KhachHang khachHang)
        {
            if (id != khachHang.MaKh)
            {
                return BadRequest();
            }

            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
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

        // POST: api/KhachHangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhachHang>> PostKhachHang(KhachHang khachHang)
        {
            if (_context.KhachHangs == null)
            {
                return Problem("Entity set 'QltourDuLichContext.KhachHangs'  is null.");
            }
            try
            {
                khachHang.MaKh = GenerateCustomerID();
                _context.KhachHangs.Add(khachHang);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetKhachHang", new { id = khachHang.MaKh }, khachHang);
            }
            catch (Exception ex)
            {
                return Problem($"Error creating KhachHang: {ex.Message}");
            }
        }

        // DELETE: api/KhachHangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang(string id)
        {
            if (_context.KhachHangs == null)
            {
                return NotFound();
            }
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachHangExists(string id)
        {
            return (_context.KhachHangs?.Any(e => e.MaKh == id)).GetValueOrDefault();
        }
        private string GenerateCustomerID()
        {
            const string chars = "0123456789";
            Random random = new Random();
            string id;
            do
            {
                id = new string(Enumerable.Repeat(chars, 10)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            } while (KhachHangExists(id));

            return id;
        }
        [HttpGet("checkEmail")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            var existingKhachHang = await _context.KhachHangs.FirstOrDefaultAsync(kh => kh.Email == email);
            return existingKhachHang != null;
        }
        [HttpGet("checkPhoneNumber")]
        public async Task<ActionResult<bool>> CheckPhoneNumberExists(string phoneNumber)
        {
            var existingKhachHang = await _context.KhachHangs.FirstOrDefaultAsync(kh => kh.SoDienThoaiKh == phoneNumber);
            return existingKhachHang != null;
        }

        [HttpGet("GetKhachHangByEmail")]
        public async Task<ActionResult<bool>> GetKhachHangByEmail(string email)
        {
            var infokh = await _context.KhachHangs.FirstOrDefaultAsync(kh => kh.Email == email);
            return Ok(infokh) ;
        }
    }
}