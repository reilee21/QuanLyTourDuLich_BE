using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class DiaDiem
{
    public int IdDiaDiem { get; set; }

    public string TenDiaDiem { get; set; } = null!;

    public bool Loai { get; set; }

    public virtual ICollection<DiemDen> DiemDens { get; set; } = new List<DiemDen>();
}
