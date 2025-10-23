using System;
using System.Collections.Generic;

namespace Demo_2310.Models;

public partial class TypeEquipment
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Equioment> Equioments { get; set; } = new List<Equioment>();
}
