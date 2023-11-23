using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class TourPhuongTien
{
    public int Id { get; set; }

    public string MaTour { get; set; } = null!;

    public int IdPhuongTien { get; set; }

    public virtual PhuongTien IdPhuongTienNavigation { get; set; } = null!;

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
