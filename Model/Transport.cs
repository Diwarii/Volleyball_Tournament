using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class Transport
{
    public int TransportId { get; set; }

    public int? TeamId { get; set; }

    public string? DriverName { get; set; }

    public string? DriverPhone { get; set; }

    public string? VehicleNumber { get; set; }

    public virtual Team? Team { get; set; }
}
