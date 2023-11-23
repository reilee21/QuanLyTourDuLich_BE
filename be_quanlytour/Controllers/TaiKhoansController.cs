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
    public class TaiKhoansController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public TaiKhoansController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/TaiKhoans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiKhoan>>> GetTaiKhoans()
        {
          if (_context.TaiKhoans == null)
          {
              return NotFound();
          }
            return await _context.TaiKhoans.ToListAsync();
        }

        // GET: api/TaiKhoans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaiKhoan>> GetTaiKhoan(string id)
        {
          if (_context.TaiKhoans == null)
          {
              return NotFound();
          }
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            return taiKhoan;
        }

        // PUT: api/TaiKhoans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaiKhoan(string id, TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.IdTaiKhoan)
            {
                return BadRequest();
            }

            _context.Entry(taiKhoan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaiKhoanExists(id))
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

        // POST: api/TaiKhoans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaiKhoan>> PostTaiKhoan(TaiKhoan taiKhoan)
        {
            if (_context.TaiKhoans == null)
            {
                return Problem("Entity set 'QltourDuLichContext.TaiKhoans'  is null.");
            }
            try
            {
                taiKhoan.IdTaiKhoan = GenerateAccID();
                KhachHang khachHang = _context.KhachHangs.FirstOrDefault(x => x.MaKh == taiKhoan.MaKh);
                if (khachHang == null)
                {
                    // Handle the case where MaKh doesn't exist in KhachHangs
                    return NotFound($"KhachHang with MaKh '{taiKhoan.MaKh}' not found.");
                }

                taiKhoan.MaKhNavigation = khachHang;

                _context.TaiKhoans.Add(taiKhoan);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTaiKhoans", new { id = taiKhoan.IdTaiKhoan }, taiKhoan);
            }
            catch (Exception ex)
            {
                return Problem($"Error creating TaiKhoan: {ex.Message}");
            }
        }

        // DELETE: api/TaiKhoans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiKhoan(string id)
        {
            if (_context.TaiKhoans == null)
            {
                return NotFound();
            }
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            _context.TaiKhoans.Remove(taiKhoan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaiKhoanExists(string id)
        {
            return (_context.TaiKhoans?.Any(e => e.IdTaiKhoan == id)).GetValueOrDefault();
        }
        private string GenerateAccID()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string id;
            do
            {
                id = new string(Enumerable.Repeat(chars, 10 - 2)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            } while (TaiKhoanExists(id));

            return id;
        }
        [HttpGet("CheckUsername/{username}")]
        public async Task<ActionResult<bool>> CheckUsername(string username)
        {
            try
            {
                var existingUser = await _context.TaiKhoans.AnyAsync(x => x.Username == username);

                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string email, string newpass)
        {           
            try
            {
                var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(kh => kh.Email == email);

                if (khachHang == null)
                {
                    return NotFound("Customer not found");
                }
                
                var taiKhoan = await _context.TaiKhoans.FirstOrDefaultAsync(tk => tk.MaKh == khachHang.MaKh);

                if (taiKhoan == null)
                {
                    return NotFound("Account not found");
                }

                taiKhoan.Password = newpass;

                await _context.SaveChangesAsync();

                return Ok("Password updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
