using System;
using System.Collections.Generic;

namespace be_quanlytour.Models;

public partial class BookingTour
{
    public int IdBookingTour { get; set; }

    public string MaTour { get; set; } = null!;

    public string IdBooking { get; set; } = null!;

    public virtual ICollection<HanhKhach> HanhKhaches { get; set; } = new List<HanhKhach>();

    public virtual Booking IdBookingNavigation { get; set; } = null!;

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
