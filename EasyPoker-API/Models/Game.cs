using System;
using System.Collections.Generic;

namespace EasyPoker_API.Models;

public partial class Game
{
    public int Id { get; set; }

    public DateTime? Timestamp { get; set; }

    public string Winner { get; set; } = null!;

    public virtual ICollection<PlayerHand> PlayerHands { get; set; } = new List<PlayerHand>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<RemainingCard> RemainingCards { get; set; } = new List<RemainingCard>();
}
