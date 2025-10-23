using System;
using System.Collections.Generic;

namespace Demo_2310.Models;

public partial class FkOrderEquip
{
    public int Id { get; set; }

    public int IdEquip { get; set; }

    public int IdOrder { get; set; }

    public virtual Equioment IdEquipNavigation { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;
}
