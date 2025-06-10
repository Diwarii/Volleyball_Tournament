using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class RoomAssignment
{
    public int AssignmentId { get; set; }

    public int? RoomId { get; set; }

    public string? PersonType { get; set; }

    public int? PersonId { get; set; }

    public DateOnly? CheckIn { get; set; }

    public DateOnly? CheckOut { get; set; }

    public string? SpecialRequests { get; set; }

    public virtual HotelRoom? Room { get; set; }
}
