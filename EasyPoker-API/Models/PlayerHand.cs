using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPoker_API.Models;

public partial class PlayerHand
{
    public int Id { get; set; }

    public int? GameId { get; set; }

    public int? PlayerId { get; set; }

    [Column(TypeName = "nvarchar(50)")] 
    public string Card1 { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string Card2 { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string Card3 { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string Card4 { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string Card5 { get; set; } = null!;

    public virtual Game? Game { get; set; }

    public virtual Player? Player { get; set; }
}
