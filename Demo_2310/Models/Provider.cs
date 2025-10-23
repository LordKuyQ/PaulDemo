using System;
using System.Collections.Generic;

namespace Demo_2310.Models;

public partial class Provider
{
    public int Id { get; set; }

    public string Provider1 { get; set; } = null!;

    public virtual ICollection<Equioment> Equioments { get; set; } = new List<Equioment>();
}
