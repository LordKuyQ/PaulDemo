using System;
using System.Collections.Generic;

namespace Demo_2310.Models;

public partial class Adress
{
    public int Id { get; set; }

    public string Adress1 { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
