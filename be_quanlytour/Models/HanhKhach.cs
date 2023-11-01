using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class HanhKhach
{
    public int IdHanhKhach { get; set; }

    public string TenHanhKhach { get; set; } = null!;

    public string LoaiHanhKhach { get; set; } = null!;

    public bool PhongRieng { get; set; }

    public int IdBookingTour { get; set; }

    public virtual BookingTour IdBookingTourNavigation { get; set; } = null!;
}
