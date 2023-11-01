using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class KhachSan
{
    public string IdKhachSan { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string MoTa { get; set; } = null!;

    public string IdDoiTac { get; set; } = null!;

    public virtual DoiTac IdDoiTacNavigation { get; set; } = null!;
}
