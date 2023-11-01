using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class KhachHang
{
    public string HoTen { get; set; } = null!;

    public string SoDienThoaiKh { get; set; } = null!;

    public DateTime NgaySinh { get; set; }

    public string Email { get; set; } = null!;

    public string? SoCccd { get; set; }

    public string? MaPassport { get; set; }

    public string DiaChi { get; set; } = null!;

    public string MaKh { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<DanhGia> DanhGia { get; set; } = new List<DanhGia>();

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
