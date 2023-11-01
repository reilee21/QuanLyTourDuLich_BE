using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class Voucher
{
    public string MaVoucher { get; set; } = null!;

    public string TenVoucher { get; set; } = null!;

    public DateTime ThoiGianBatDau { get; set; }

    public DateTime ThoiGianKetThuc { get; set; }

    public byte SoLuong { get; set; }

    public double PhanTramGiam { get; set; }

    public int DiemDoiThuong { get; set; }

    public string? IdDoiTac { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual DoiTac? IdDoiTacNavigation { get; set; }
}
