using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class Booking
{
    public string IdBooking { get; set; } = null!;

    public DateTime ThoiDiemBook { get; set; }

    public double GiaTri { get; set; }

    public bool ThanhToan { get; set; }

    public bool LoaiBooking { get; set; }

    public string MaNv { get; set; } = null!;

    public string MaKh { get; set; } = null!;

    public string? MaVoucher { get; set; }

    public virtual ICollection<BookingK> BookingKs { get; set; } = new List<BookingK>();

    public virtual ICollection<BookingTour> BookingTours { get; set; } = new List<BookingTour>();

    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;

    public virtual Voucher? MaVoucherNavigation { get; set; }
}
