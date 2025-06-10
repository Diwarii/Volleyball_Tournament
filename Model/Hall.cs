using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class Hall
{
    public int HallId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<HallRow> HallRows { get; set; } = new List<HallRow>();

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
