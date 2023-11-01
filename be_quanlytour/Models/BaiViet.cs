using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class BaiViet
{
    public int IdBaiViet { get; set; }

    public DateTime NgayDang { get; set; }

    public string NoiDung { get; set; } = null!;

    public string AnhBia { get; set; } = null!;

    public string TieuDe { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
