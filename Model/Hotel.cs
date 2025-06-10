using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class Hotel
{
    public int HotelId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public int? TeamId { get; set; }

    public virtual ICollection<HotelRoom> HotelRooms { get; set; } = new List<HotelRoom>();

    public virtual Team? Team { get; set; }

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
