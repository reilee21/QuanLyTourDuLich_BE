using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class LichTrinh
{
    public DateTime Ngay { get; set; }

    public string MoTa { get; set; } = null!;

    public string IdDiemDen { get; set; } = null!;

    public string MaTour { get; set; } = null!;

    public virtual DiemDen IdDiemDenNavigation { get; set; } = null!;

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
