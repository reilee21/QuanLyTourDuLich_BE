using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class TaiKhoanNv
{
    public string IdTaiKhoan { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
