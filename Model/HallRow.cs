using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class HallRow
{
    public int RowId { get; set; }

    public int? HallId { get; set; }

    public int? RowNumber { get; set; }

    public virtual Hall? Hall { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
