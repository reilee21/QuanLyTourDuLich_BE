using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class MucHoiDap
{
    public int IdHoiDap { get; set; }

    public string CauHoi { get; set; } = null!;

    public string CauTraLoi { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
