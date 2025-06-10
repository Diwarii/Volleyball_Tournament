using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class TicketBuyer
{
    public int BuyerId { get; set; }

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? PassportSeries { get; set; }

    public string? PassportNumber { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
