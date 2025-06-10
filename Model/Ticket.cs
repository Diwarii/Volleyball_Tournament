using System;
using System.Collections.Generic;

namespace Work.Model;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int? MatchId { get; set; }

    public int? SeatId { get; set; }

    public decimal? Price { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public int? BuyerId { get; set; }

    public virtual TicketBuyer? Buyer { get; set; }

    public virtual Match? Match { get; set; }

    public virtual Seat? Seat { get; set; }
}
