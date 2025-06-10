using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class Seat
{
    public int SeatId { get; set; }

    public int? RowId { get; set; }

    public int? SeatNumber { get; set; }

    public virtual HallRow? Row { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
