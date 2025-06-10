using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class Catering
{
    public int CateringId { get; set; }

    public int? TeamId { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public string? MenuType { get; set; }

    public string? Menu { get; set; }

    public string? Requirements { get; set; }

    public virtual Team? Team { get; set; }
}
