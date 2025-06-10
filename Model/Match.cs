using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class Match
{
    public int MatchId { get; set; }

    public int? TournamentId { get; set; }

    public int? Team1Id { get; set; }

    public int? Team2Id { get; set; }

    public int? HallId { get; set; }

    public DateTime? MatchDate { get; set; }

    public int? Team1Score { get; set; }

    public int? Team2Score { get; set; }

    public virtual Hall? Hall { get; set; }

    public virtual Team? Team1 { get; set; }

    public virtual Team? Team2 { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual Tournament? Tournament { get; set; }

    public virtual ICollection<Referee> Referees { get; set; } = new List<Referee>();
}
