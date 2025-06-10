using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class Referee
{
    public int RefereeId { get; set; }

    public string? FullName { get; set; }

    public string? CertificationLevel { get; set; }

    public int? ExperienceYears { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? PassportSeries { get; set; }

    public string? PassportNumber { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
