using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class Player
{
    public int PlayerId { get; set; }

    public int? TeamId { get; set; }

    public string? FullName { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Position { get; set; }

    public int? JerseyNumber { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public virtual Team? Team { get; set; }
}
