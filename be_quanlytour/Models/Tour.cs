using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace be_quanlytour.Models;

public partial class Tour
{
    public string MaTour { get; set; } = null!;

    public string TenTour { get; set; } = null!;

    public byte SoLuongNguoi { get; set; }

    public byte SoLuongNguoiDaDat { get; set; }

    public DateTime NgayKhoiHanh { get; set; }

    public byte SoNgay { get; set; }

    public byte SoDem { get; set; }

    public string NoiKhoiHanh { get; set; } = null!;

    public DateTime GioTapTrung { get; set; }

    public double Gia { get; set; }

    public string? AnhBia { get; set; }
    [NotMapped]
    public string? Image { get; set; }

    public virtual ICollection<BookingTour> BookingTours { get; set; } = new List<BookingTour>();

    public virtual ICollection<DanhGia> DanhGia { get; set; } = new List<DanhGia>();

    public virtual ICollection<LichTrinh> LichTrinhs { get; set; } = new List<LichTrinh>();

    public virtual ICollection<TourKhuyenMai> TourKhuyenMais { get; set; } = new List<TourKhuyenMai>();

    public virtual ICollection<TourPhuongTien> TourPhuongTiens { get; set; } = new List<TourPhuongTien>();
}

public partial class ITour
{
    public string MaTour { get; set; } = null!;

    public string TenTour { get; set; } = null!;

    public byte SoLuongNguoi { get; set; }

    public byte SoLuongNguoiDaDat { get; set; }

    public DateTime NgayKhoiHanh { get; set; }

    public byte SoNgay { get; set; }

    public byte SoDem { get; set; }

    public string NoiKhoiHanh { get; set; } = null!;

    public DateTime GioTapTrung { get; set; }

    public double Gia { get; set; }

    public string? AnhBia { get; set; }
    public IFormFile? Image { get; set; }

    public string LichTrinhs { get; set; }
    public string TourPhuongTiens { get; set; }


}
