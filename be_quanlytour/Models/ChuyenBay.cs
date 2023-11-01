using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class ChuyenBay
{
    public string MaChuyenBay { get; set; } = null!;

    public DateTime NgayGioBay { get; set; }

    public string MaTour { get; set; } = null!;

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
