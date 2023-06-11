using System;
using System.Collections.Generic;

namespace TP_LC4.Models;

public partial class Actor
{
    public int ActorId { get; set; }

    public string? ActorName { get; set; }

    public DateTime? ActorBirthdate { get; set; }

    public string? ActorPicture { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
