using System;
using System.Collections.Generic;

namespace TP_LC4.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string? MovieName { get; set; }

    public string? MovieGenre { get; set; }

    public int? MovieDuration { get; set; }

    public decimal? MovieBudget { get; set; }

    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();
}
