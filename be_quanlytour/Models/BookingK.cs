using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class BookingK
{
    public int IdBookingKs { get; set; }

    public DateTime NgayNhan { get; set; }

    public DateTime NgayTra { get; set; }

    public int IdLoaiPhong { get; set; }

    public string IdBooking { get; set; } = null!;

    public virtual Booking IdBookingNavigation { get; set; } = null!;

    public virtual LoaiPhong IdLoaiPhongNavigation { get; set; } = null!;
}
