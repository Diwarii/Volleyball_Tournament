using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class Tournament
{
    public int TournamentId { get; set; }

    public string? Name { get; set; }

    public string? Format { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? Budget { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
