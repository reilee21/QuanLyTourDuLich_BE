using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class TourKhuyenMai
{
    public int Id { get; set; }

    public int MaKm { get; set; }

    public string MaTour { get; set; } = null!;

    public virtual KhuyenMai MaKmNavigation { get; set; } = null!;

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
