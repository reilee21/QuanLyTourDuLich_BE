using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string SoDienThoaiNv { get; set; } = null!;

    public string? SoCccd { get; set; }

    public string Email { get; set; } = null!;

    public string ChucVu { get; set; } = null!;

    public virtual ICollection<BaiViet> BaiViets { get; set; } = new List<BaiViet>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<MucHoiDap> MucHoiDaps { get; set; } = new List<MucHoiDap>();

    public virtual ICollection<TaiKhoanNv> TaiKhoanNvs { get; set; } = new List<TaiKhoanNv>();
}
