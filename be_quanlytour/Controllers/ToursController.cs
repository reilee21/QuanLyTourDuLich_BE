using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using be_quanlytour.Models;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using be_quanlytour.Helper;

namespace be_quanlytour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly QltourDuLichContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment; 

        public ToursController(QltourDuLichContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet("SearchTour")]
        public async Task<ActionResult<IEnumerable<Tour>>> GetToursByLocation(string? noikhoihanh, int? iddiadiem, DateTime? ngaykhoihanh,string? tendiadiem)
        {

            if(tendiadiem !=null)
            {

                var query = (from tour in _context.Tours
                             join lichTrinh in _context.LichTrinhs on tour.MaTour equals lichTrinh.MaTour
                             join diemDen in _context.DiemDens on lichTrinh.IdDiemDen equals diemDen.IdDiemDen
                             join diaDiem in _context.DiaDiems on diemDen.IdDiaDiem equals diaDiem.IdDiaDiem
                             where diaDiem.TenDiaDiem.Contains(tendiadiem) || diemDen.TenDiemDen.Contains(tendiadiem)
                             select tour).Distinct();
                foreach (var item in query)
                {
                    if (item.AnhBia != null)
                    {
                        item.Image = Utils.GetIMGAsBase64(_hostingEnvironment, item.AnhBia);
                    }
                }
                return Ok(query);

            }

            if (noikhoihanh==null && iddiadiem ==null && ngaykhoihanh ==null)
                   return NotFound();



            if(iddiadiem==null && ngaykhoihanh ==null)
            {
                var query = _context.Tours.Include(t => t.LichTrinhs).Include(t => t.TourPhuongTiens).Where(x => x.NoiKhoiHanh.Contains(noikhoihanh.Trim())).ToList();
                foreach (var item in query)
                {
                    if (item.AnhBia != null)
                    {
                        item.Image = Utils.GetIMGAsBase64(_hostingEnvironment, item.AnhBia);
                    }
                }
                return Ok(query);
            }
            if (iddiadiem == null && noikhoihanh == null)
            {
                var query = _context.Tours.Include(t => t.LichTrinhs).Include(t => t.TourPhuongTiens).Where(x => x.NgayKhoiHanh == ngaykhoihanh).ToList();
                foreach (var item in query)
                {
                    if (item.AnhBia != null)
                    {
                        item.Image = Utils.GetIMGAsBase64(_hostingEnvironment, item.AnhBia);
                    }
                }
                return Ok(query);
            }

            if (noikhoihanh == null && ngaykhoihanh ==null)
            {
                var query = _context.Tours.Include(t => t.LichTrinhs).Include(t => t.TourPhuongTiens)
                                                            .Join(_context.LichTrinhs,
                                                                tour => tour.MaTour,lichTrinh => lichTrinh.MaTour,
                                                                (tour, lichTrinh) => new { tour, lichTrinh }
                                                            )
                                                            .Join(_context.DiemDens,
                                                                combined => combined.lichTrinh.IdDiemDen,diemDen => diemDen.IdDiemDen,
                                                                (combined, diemDen) => new { combined.tour, combined.lichTrinh, diemDen }
                                                            )
                                                            .Where(result => result.diemDen.IdDiaDiem == iddiadiem)
                                                            .Select(result => result.tour)
                                                            .Distinct()
                                                            .ToList();

                foreach (var item in query)
                {
                    if (item.AnhBia != null)
                    {
                        item.Image = Utils.GetIMGAsBase64(_hostingEnvironment, item.AnhBia);
                    }
                }
                return Ok(query);
            }           
     
            if (ngaykhoihanh == null)
            {
                var query = _context.Tours.Where(tour => tour.NoiKhoiHanh == noikhoihanh && tour.NgayKhoiHanh == ngaykhoihanh).ToList();
                foreach (var item in query)
                {
                    if (item.AnhBia != null)
                    {
                        item.Image = Utils.GetIMGAsBase64(_hostingEnvironment, item.AnhBia);
                    }
                }

                return Ok(query);
            }


            var query1 = _context.Tours.Include(t => t.LichTrinhs).Include(t => t.TourPhuongTiens)
                                                                .Join(
                                                                    _context.LichTrinhs,
                                                                    tour => tour.MaTour,
                                                                    lichTrinh => lichTrinh.MaTour,
                                                                    (tour, lichTrinh) => new { tour, lichTrinh }
                                                                )
                                                                .Join(
                                                                    _context.DiemDens,
                                                                    combined => combined.lichTrinh.IdDiemDen,
                                                                    diemDen => diemDen.IdDiemDen,
                                                                    (combined, diemDen) => new { combined.tour, combined.lichTrinh, diemDen }
                                                                )
                                                                .Where(result => result.diemDen.IdDiaDiem == iddiadiem && result.tour.NoiKhoiHanh == noikhoihanh && result.lichTrinh.Ngay == ngaykhoihanh)
                                                                .Select(result => result.tour)
                                                                .Distinct()
                                                                .ToList();

            foreach (var item in query1)
            {
                if (item.AnhBia != null)
                {
                    item.Image = Utils.GetIMGAsBase64(_hostingEnvironment, item.AnhBia);
                }
            }

            return Ok(query1);
        }
        // GET: api/Tours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tour>>> GetTours()
        {
          if (_context.Tours == null)
          {
              return NotFound();
          }
            return await _context.Tours.ToListAsync();
        }

        // GET: api/Tours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tour>> GetTour(string id)
        {
          if (_context.Tours == null)
          {
              return NotFound();
          }
            var tour = await _context.Tours
       .Include(t => t.LichTrinhs)
       .Include(t => t.TourPhuongTiens)
       .FirstOrDefaultAsync(t => t.MaTour == id);
            if (tour.AnhBia != null)
            {

                    tour.Image = Utils.GetIMGAsBase64(_hostingEnvironment, tour.AnhBia); 
            }
            if (tour == null)
            {
                return NotFound();
            }

            return tour;
        }

        // PUT: api/Tours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTour(string id, [FromForm] ITour tour)
        {
            var existingTour = await _context.Tours.FindAsync(id);

            if (existingTour == null)
            {
                return NotFound(); 
            }
            existingTour.TenTour = tour.TenTour;
            existingTour.SoLuongNguoi = tour.SoLuongNguoi;
            existingTour.SoLuongNguoiDaDat = tour.SoLuongNguoiDaDat;
            existingTour.NgayKhoiHanh = tour.NgayKhoiHanh;
            existingTour.NoiKhoiHanh = tour.NoiKhoiHanh;
            existingTour.SoNgay = tour.SoNgay;
            existingTour.SoDem = tour.SoDem;
            existingTour.Gia = tour.Gia;
            _context.Tours.Update(existingTour);
            List<ILichTrinh> lt = new List<ILichTrinh>();
            List<PhuongTien> pts = new List<PhuongTien>();

            lt = JsonConvert.DeserializeObject<List<ILichTrinh>>(tour.LichTrinhs);
            pts = JsonConvert.DeserializeObject<List<PhuongTien>>(tour.TourPhuongTiens);
            if(tour.Image != null)
            {
                string v = await Utils.SaveIMG(_hostingEnvironment, tour.MaTour, tour.Image);
                tour.AnhBia = v;
            }


            var existingLichTrinhs = _context.LichTrinhs.Where(lt => lt.MaTour == tour.MaTour);
            var existingTourPhuongTiens = _context.TourPhuongTiens.Where(pt => pt.MaTour == tour.MaTour);

            _context.LichTrinhs.RemoveRange(existingLichTrinhs);
            _context.TourPhuongTiens.RemoveRange(existingTourPhuongTiens);
            foreach (var lichTrinh in lt)
            {
                lichTrinh.MaTour = tour.MaTour;
                _context.LichTrinhs.Add(new LichTrinh() { IdDiemDen = lichTrinh.IdDiemDen, MaTour = lichTrinh.MaTour, MoTa = lichTrinh.MoTa, Ngay = lichTrinh.Ngay });
            }

            foreach (var pt in pts)
            {
                _context.TourPhuongTiens.Add(new TourPhuongTien() { IdPhuongTien = pt.IdPhuongTien, MaTour = tour.MaTour });

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

        // POST: api/Tours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ITour>> PostTour([FromForm]ITour tour)
        {
          if (_context.Tours == null)
          {
              return Problem("Entity set 'QltourDuLichContext.Tours'  is null.");
          }
            string mt = "T-" + tour.NgayKhoiHanh.ToString("yyyyMMdd") + Utils.TaoMaTuDong(4);
            while (TourExists(mt))
            {
                mt = "T-" + tour.NgayKhoiHanh.ToString("yyyyMMdd") + Utils.TaoMaTuDong(4);
            }



            var tourin4 = new Tour() { MaTour = mt, TenTour = tour.TenTour, SoLuongNguoi = tour.SoLuongNguoi, SoLuongNguoiDaDat = tour.SoLuongNguoiDaDat, NgayKhoiHanh = tour.NgayKhoiHanh, NoiKhoiHanh = tour.NoiKhoiHanh, SoNgay = tour.SoNgay, SoDem = tour.SoDem, GioTapTrung = tour.GioTapTrung, Gia = tour.Gia };
            List<ILichTrinh> lt = new List<ILichTrinh>();
            List<PhuongTien> pts = new List<PhuongTien>();

            lt = JsonConvert.DeserializeObject<List<ILichTrinh>>(tour.LichTrinhs);
            pts = JsonConvert.DeserializeObject<List<PhuongTien>>(tour.TourPhuongTiens);
            string v = await Utils.SaveIMG(_hostingEnvironment, tourin4.MaTour, tour.Image);
            tourin4.AnhBia = v;


            _context.Tours.Add(tourin4);
            foreach (var lichTrinh in lt)
            {
                lichTrinh.MaTour = tourin4.MaTour;
                _context.LichTrinhs.Add(new LichTrinh() { IdDiemDen = lichTrinh.IdDiemDen, MaTour = lichTrinh.MaTour, MoTa = lichTrinh.MoTa, Ngay = lichTrinh.Ngay });
            }

            foreach (var pt in pts)
            {
                _context.TourPhuongTiens.Add(new TourPhuongTien() { IdPhuongTien = pt.IdPhuongTien, MaTour = tourin4.MaTour });

            }

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                if (TourExists(tour.MaTour))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTour", new { id = tour.MaTour }, tour);
        }
     

        [HttpGet("getimg/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "application/octet-stream");
            }

            return NotFound();

        }
       

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(string id)
        {
            if (_context.Tours == null)
            {
                return NotFound();
            }
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            _context.Tours.Remove(tour);
            var imageName = tour.AnhBia;     
        

            try
            {                
               Utils.DeleteIMG(_hostingEnvironment, imageName);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete tour: {ex.Message}");
            }
        }





        private bool TourExists(string id)
        {
            return (_context.Tours?.Any(e => e.MaTour == id)).GetValueOrDefault();
        }
       
    }
}
