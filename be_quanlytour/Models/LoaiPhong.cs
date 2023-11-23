using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class LoaiPhong
{
    public int IdLoaiPhong { get; set; }

    public string TenLoai { get; set; } = null!;

    public string Mota { get; set; } = null!;

    public int Gia { get; set; }

    public string IdKhachSan { get; set; } = null!;

    public byte SoPhong { get; set; }

    public virtual ICollection<BookingK> BookingKs { get; set; } = new List<BookingK>();

    public virtual KhachSan IdKhachSanNavigation { get; set; } = null!;
}
public partial class ILoaiPhong
{

    public string TenLoai { get; set; } = null!;

    public string Mota { get; set; } = null!;

    public int Gia { get; set; }
    public byte SoPhong { get; set; }

}