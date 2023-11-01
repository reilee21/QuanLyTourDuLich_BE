using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class TaiKhoan
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string IdTaiKhoan { get; set; } = null!;

    public string MaKh { get; set; } = null!;

    public virtual KhachHang MaKhNavigation { get; set; } = null!;
}
