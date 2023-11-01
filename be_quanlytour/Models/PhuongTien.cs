using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class PhuongTien
{
    public int IdPhuongTien { get; set; }

    public string TenPhuongTien { get; set; } = null!;

    public string MoTa { get; set; } = null!;

    public string? IdDoiTac { get; set; }

    public virtual DoiTac? IdDoiTacNavigation { get; set; }

    public virtual ICollection<Tour> MaTours { get; set; } = new List<Tour>();
}
