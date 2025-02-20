using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPoker_API.Models;

public partial class RemainingCard
{
    public int Id { get; set; }

    public int? GameId { get; set; }

    [Column(TypeName = "nvarchar(50)")] 
    public string Card { get; set; } = null!;

    public virtual Game? Game { get; set; }
}
