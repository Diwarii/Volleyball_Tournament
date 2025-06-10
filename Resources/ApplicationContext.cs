using Microsoft.EntityFrameworkCore;
using Work.Model;

namespace Work.Resources;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext() { }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public virtual DbSet<Catering> Caterings { get; set; }

    public virtual DbSet<Hall> Halls { get; set; }

    public virtual DbSet<HallRow> HallRows { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<HotelRoom> HotelRooms { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Referee> Referees { get; set; }

    public virtual DbSet<RoomAssignment> RoomAssignments { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamStaff> TeamStaffs { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketBuyer> TicketBuyers { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=DBSRV\\vip2024; Database=Маслов2307св1_Воллейбол; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catering>(entity =>
        {
            entity.HasKey(e => e.CateringId).HasName("PK__Catering__25F9658218548D68");

            entity.ToTable("Catering");

            entity.Property(e => e.CateringId).HasColumnName("catering_id");
            entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
            entity.Property(e => e.Menu)
                .HasColumnType("text")
                .HasColumnName("menu");
            entity.Property(e => e.MenuType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("menu_type");
            entity.Property(e => e.Requirements)
                .HasColumnType("text")
                .HasColumnName("requirements");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Team).WithMany(p => p.Caterings)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Catering__team_i__5006DFF2");
        });

        modelBuilder.Entity<Hall>(entity =>
        {
            entity.HasKey(e => e.HallId).HasName("PK__Halls__A63DE8CF2E331ABF");

            entity.Property(e => e.HallId).HasColumnName("hall_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<HallRow>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK__HallRows__6965AB57893285E4");

            entity.Property(e => e.RowId).HasColumnName("row_id");
            entity.Property(e => e.HallId).HasColumnName("hall_id");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");

            entity.HasOne(d => d.Hall).WithMany(p => p.HallRows)
                .HasForeignKey(d => d.HallId)
                .HasConstraintName("FK__HallRows__hall_i__3BFFE745");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PK__Hotels__45FE7E26BB07323F");

            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Team).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Hotels__team_id__54CB950F");
        });

        modelBuilder.Entity<HotelRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__HotelRoo__19675A8A4E8B6391");

            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.RoomNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("room_number");

            entity.HasOne(d => d.Hotel).WithMany(p => p.HotelRooms)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__HotelRoom__hotel__4A4E069C");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("PK__Matches__9D7FCBA3CD86904B");

            entity.Property(e => e.MatchId).HasColumnName("match_id");
            entity.Property(e => e.HallId).HasColumnName("hall_id");
            entity.Property(e => e.MatchDate)
                .HasColumnType("datetime")
                .HasColumnName("match_date");
            entity.Property(e => e.Team1Id).HasColumnName("team1_id");
            entity.Property(e => e.Team1Score).HasColumnName("team1_score");
            entity.Property(e => e.Team2Id).HasColumnName("team2_id");
            entity.Property(e => e.Team2Score).HasColumnName("team2_score");
            entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

            entity.HasOne(d => d.Hall).WithMany(p => p.Matches)
                .HasForeignKey(d => d.HallId)
                .HasConstraintName("FK__Matches__hall_id__3552E9B6");

            entity.HasOne(d => d.Team1).WithMany(p => p.MatchTeam1s)
                .HasForeignKey(d => d.Team1Id)
                .HasConstraintName("FK__Matches__team1_i__336AA144");

            entity.HasOne(d => d.Team2).WithMany(p => p.MatchTeam2s)
                .HasForeignKey(d => d.Team2Id)
                .HasConstraintName("FK__Matches__team2_i__345EC57D");

            entity.HasOne(d => d.Tournament).WithMany(p => p.Matches)
                .HasForeignKey(d => d.TournamentId)
                .HasConstraintName("FK__Matches__tournam__32767D0B");

            entity.HasMany(d => d.Referees).WithMany(p => p.Matches)
                .UsingEntity<Dictionary<string, object>>(
                    "MatchReferee",
                    r => r.HasOne<Referee>().WithMany()
                        .HasForeignKey("RefereeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MatchRefe__refer__39237A9A"),
                    l => l.HasOne<Match>().WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MatchRefe__match__382F5661"),
                    j =>
                    {
                        j.HasKey("MatchId", "RefereeId").HasName("PK__MatchRef__2FFEC954F601794F");
                        j.ToTable("MatchReferees");
                        j.IndexerProperty<int>("MatchId").HasColumnName("match_id");
                        j.IndexerProperty<int>("RefereeId").HasColumnName("referee_id");
                    });
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Players__44DA120C579D3F83");

            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.JerseyNumber).HasColumnName("jersey_number");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Players__team_id__2AD55B43");
        });

        modelBuilder.Entity<Referee>(entity =>
        {
            entity.HasKey(e => e.RefereeId).HasName("PK__Referees__28102F7A1763F66E");

            entity.Property(e => e.RefereeId).HasColumnName("referee_id");
            entity.Property(e => e.CertificationLevel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("certification_level");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.ExperienceYears).HasColumnName("experience_years");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("passport_number");
            entity.Property(e => e.PassportSeries)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("passport_series");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<RoomAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__RoomAssi__DA89181497F56D02");

            entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
            entity.Property(e => e.CheckIn).HasColumnName("check_in");
            entity.Property(e => e.CheckOut).HasColumnName("check_out");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.PersonType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("person_type");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.SpecialRequests)
                .HasColumnType("text")
                .HasColumnName("special_requests");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomAssignments)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__RoomAssig__room___4D2A7347");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PK__Seats__906DED9CBD0406E1");

            entity.Property(e => e.SeatId).HasColumnName("seat_id");
            entity.Property(e => e.RowId).HasColumnName("row_id");
            entity.Property(e => e.SeatNumber).HasColumnName("seat_number");

            entity.HasOne(d => d.Row).WithMany(p => p.Seats)
                .HasForeignKey(d => d.RowId)
                .HasConstraintName("FK__Seats__row_id__3EDC53F0");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__F82DEDBCD791A040");

            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CoachContact)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("coach_contact");
            entity.Property(e => e.CoachName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("coach_name");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Teams)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__Teams__hotel_id__53D770D6");
        });

        modelBuilder.Entity<TeamStaff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__TeamStaf__1963DD9C0DE8021A");

            entity.ToTable("TeamStaff");

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamStaffs)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__TeamStaff__team___2DB1C7EE");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__D596F96BD72ABF4D");

            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.BuyerId).HasColumnName("buyer_id");
            entity.Property(e => e.MatchId).HasColumnName("match_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("datetime")
                .HasColumnName("purchase_date");
            entity.Property(e => e.SeatId).HasColumnName("seat_id");

            entity.HasOne(d => d.Buyer).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("FK__Tickets__buyer_i__4589517F");

            entity.HasOne(d => d.Match).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MatchId)
                .HasConstraintName("FK__Tickets__match_i__43A1090D");

            entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeatId)
                .HasConstraintName("FK__Tickets__seat_id__44952D46");
        });

        modelBuilder.Entity<TicketBuyer>(entity =>
        {
            entity.HasKey(e => e.BuyerId).HasName("PK__TicketBu__BAD17152B9F9A101");

            entity.Property(e => e.BuyerId).HasColumnName("buyer_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("passport_number");
            entity.Property(e => e.PassportSeries)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("passport_series");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.TournamentId).HasName("PK__Tourname__B93AA09DF673DC57");

            entity.Property(e => e.TournamentId).HasColumnName("tournament_id");
            entity.Property(e => e.Budget)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("budget");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Format)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("format");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.TransportId).HasName("PK__Transpor__A17E3277EA656CCC");

            entity.ToTable("Transport");

            entity.Property(e => e.TransportId).HasColumnName("transport_id");
            entity.Property(e => e.DriverName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("driver_name");
            entity.Property(e => e.DriverPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("driver_phone");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("vehicle_number");

            entity.HasOne(d => d.Team).WithMany(p => p.Transports)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Transport__team___52E34C9D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
