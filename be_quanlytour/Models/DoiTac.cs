using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class DoiTac
{
    public string IdDoiTac { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string SoDienThoaiDt { get; set; } = null!;

    public virtual ICollection<KhachSan> KhachSans { get; set; } = new List<KhachSan>();

    public virtual ICollection<PhuongTien> PhuongTiens { get; set; } = new List<PhuongTien>();

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
