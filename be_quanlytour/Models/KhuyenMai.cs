using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class KhuyenMai
{
    public int MaKm { get; set; }

    public string TenKm { get; set; } = null!;

    public double PhanTramKm { get; set; }

    public virtual ICollection<TourKhuyenMai> TourKhuyenMais { get; set; } = new List<TourKhuyenMai>();
}
