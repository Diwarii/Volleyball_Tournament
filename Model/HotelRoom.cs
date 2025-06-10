using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class HotelRoom
{
    public int RoomId { get; set; }

    public int? HotelId { get; set; }

    public string? RoomNumber { get; set; }

    public int? Capacity { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<RoomAssignment> RoomAssignments { get; set; } = new List<RoomAssignment>();
}
