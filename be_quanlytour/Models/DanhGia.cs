using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class DanhGia
{
    public int MaDanhGia { get; set; }

    public DateTime ThoiDiem { get; set; }

    public string NoiDung { get; set; }

    public string MaTour { get; set; } = null!;

    public string MaKh { get; set; } = null!;

    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
