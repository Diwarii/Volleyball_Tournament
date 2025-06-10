using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class TeamStaff
{
    public int StaffId { get; set; }

    public int? TeamId { get; set; }

    public string? FullName { get; set; }

    public string? Role { get; set; }

    public DateOnly? BirthDate { get; set; }

    public virtual Team? Team { get; set; }
}
