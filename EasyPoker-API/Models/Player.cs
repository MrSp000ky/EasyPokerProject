using System;
using System.Collections.Generic;

namespace EasyPoker_API.Models;

public partial class Player
{
    public int Id { get; set; }

    public int? GameId { get; set; }

    public string PlayerName { get; set; } = null!;

    public virtual Game? Game { get; set; }

    public virtual ICollection<PlayerHand> PlayerHands { get; set; } = new List<PlayerHand>();
}
