namespace Work.Model;

public partial class Team
{
    public int TeamId { get; set; }

    public string? Name { get; set; }

    public string? City { get; set; }

    public string? CoachName { get; set; }

    public string? CoachContact { get; set; }

    public int? HotelId { get; set; }

    public virtual ICollection<Catering> Caterings { get; set; } = new List<Catering>();

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

    public virtual ICollection<Match> MatchTeam1s { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchTeam2s { get; set; } = new List<Match>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<TeamStaff> TeamStaffs { get; set; } = new List<TeamStaff>();

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}
