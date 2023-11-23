using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class DiemDen
{
    public string IdDiemDen { get; set; } = null!;

    public string TenDiemDen { get; set; } = null!;

    public int IdDiaDiem { get; set; }

    public virtual DiaDiem? IdDiaDiemNavigation { get; set; } = null!;

    public virtual ICollection<LichTrinh> LichTrinhs { get; set; } = new List<LichTrinh>();
}
