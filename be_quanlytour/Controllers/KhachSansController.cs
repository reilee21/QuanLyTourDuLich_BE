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
    public class KhachSansController : ControllerBase
    {
        private readonly QltourDuLichContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public KhachSansController(QltourDuLichContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachSan>>> GetKhachSans()
        {
          if (_context.KhachSans == null)
          {
              return NotFound();
          }
          var ksl = await _context.KhachSans.Include(x => x.LoaiPhongs).ToListAsync();
            foreach (var ks in ksl)
            {
                if (ks.Anh != null)
                {
                    ks.Anh = Utils.GetIMGAsBase64(_hostingEnvironment, ks.Anh);
                }
            }
            return ksl;
        }
        [HttpGet("PhongKhachSan/{idPhong}")]
        public async Task<ActionResult<KhachSan>> GetPhongKhachSan(int idPhong)
        {
            // Find the room by its ID
            var phong = await _context.LoaiPhongs.FindAsync(idPhong);

            if (phong == null)
            {
                return NotFound(); 
            }

            var khachSans = await _context.KhachSans
                .Include(x => x.LoaiPhongs)
                .Where(x => x.IdKhachSan == phong.IdKhachSan).FirstAsync();

            if (khachSans == null) { 
                return NotFound(); 
            }

            return khachSans;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<KhachSan>> GetKhachSan(string id)
        {
          if (_context.KhachSans == null)
          {
              return NotFound();
          }
            var khachSan = await _context.KhachSans
     .Include(x => x.LoaiPhongs)
     .FirstOrDefaultAsync(x => x.IdKhachSan == id);
            if (khachSan.Anh != null)
            {
                khachSan.Anh = Utils.GetIMGAsBase64(_hostingEnvironment, khachSan.Anh);
            }
            if (khachSan == null)
            {
                return NotFound();
            }

            return khachSan;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachSan(string id, [FromForm] IKhachSan khachSan)
        {
            var existingks = await _context.KhachSans.FindAsync(id);

            if (existingks == null)
            {
                return NotFound();
            }
            existingks.Ten = khachSan.Ten;
            existingks.MoTa = khachSan.MoTa;
            existingks.SoSao = khachSan.SoSao;
            existingks.DiaChi = khachSan.DiaChi;
            existingks.IdDoiTac = khachSan.IdDoiTac;
            if(khachSan.Image != null)
            {
                string v = await Utils.SaveIMG(_hostingEnvironment, existingks.IdKhachSan, khachSan.Image);
                existingks.Anh = v;
            }
            List<ILoaiPhong> plist = new List<ILoaiPhong>();
            plist = JsonConvert.DeserializeObject<List<ILoaiPhong>>(khachSan.Phongs);

            var existingLoaiPhong = _context.LoaiPhongs.Where(i => i.IdKhachSan == id);
            _context.LoaiPhongs.RemoveRange(existingLoaiPhong);

            foreach (var item in plist)
            {
                _context.LoaiPhongs.Add(new LoaiPhong() { TenLoai = item.TenLoai, Gia = item.Gia, Mota = item.Mota, SoPhong = item.SoPhong, IdKhachSan = id });
            }
            try
            {
                await _context.SaveChangesAsync();
                return Ok("Update tour thanh cong");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        public async Task<ActionResult<IKhachSan>> PostKhachSan([FromForm]IKhachSan khachSan)
        {
            if (_context.KhachSans == null)
            {
                return Problem("Entity set 'QltourDuLichContext.KhachSans'  is null.");
            }

            List<ILoaiPhong> plist = new List<ILoaiPhong>();
            plist = JsonConvert.DeserializeObject<List<ILoaiPhong>>(khachSan.Phongs);
            KhachSan newks = new KhachSan() {Ten = khachSan.Ten, DiaChi =khachSan.DiaChi, MoTa= khachSan.MoTa, SoSao=khachSan.SoSao,IdDoiTac = khachSan.IdDoiTac };
            newks.IdKhachSan = "KS"+Utils.TaoMaTuDong(8);
           
            while (KhachSanExists(newks.IdKhachSan))
            {
                newks.IdKhachSan = Utils.TaoMaTuDong(8);
            }
            string v = await Utils.SaveIMG(_hostingEnvironment, newks.IdKhachSan, khachSan.Image);
            newks.Anh = v;



            _context.KhachSans.Add(newks);
            foreach (var item in plist)
            {
                _context.LoaiPhongs.Add(new LoaiPhong() { TenLoai = item.TenLoai, Gia = item.Gia, Mota = item.Mota, SoPhong = item.SoPhong,IdKhachSan=newks.IdKhachSan});
            }
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                if (KhachSanExists(newks.IdKhachSan))
                {
                    return Conflict();
                }
            }

            return CreatedAtAction("GetKhachSan", new { id = newks.IdKhachSan }, khachSan);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachSan(string id)
        {
            if (_context.KhachSans == null)
            {
                return NotFound();
            }
            var khachSan = await _context.KhachSans.FindAsync(id);
            if (khachSan == null)
            {
                return NotFound();
            }

            _context.KhachSans.Remove(khachSan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachSanExists(string id)
        {
            return (_context.KhachSans?.Any(e => e.IdKhachSan == id)).GetValueOrDefault();
        }
       
    }
}
