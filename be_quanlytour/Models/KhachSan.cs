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

    public string? Anh { get; set; }

    public byte SoSao { get; set; }

    public virtual DoiTac IdDoiTacNavigation { get; set; } = null!;

    public virtual ICollection<LoaiPhong> LoaiPhongs { get; set; } = new List<LoaiPhong>();

}

public partial class IKhachSan
{
    public string Ten { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string MoTa { get; set; } = null!;

    public string IdDoiTac { get; set; } = null!;

    public string Phongs { get; set; } = null!;
    public byte SoSao { get; set; }


    public IFormFile? Image { get; set; }

}