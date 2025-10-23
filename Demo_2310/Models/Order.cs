using System;
using System.Collections.Generic;

namespace Demo_2310.Models;

public partial class Order
{
    public int Id { get; set; }

    public double NumberOrder { get; set; }

    public double CountRent { get; set; }

    public DateTime DateStart { get; set; }

    public int IdAdress { get; set; }

    public int IdUser { get; set; }

    public double Code { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<FkOrderEquip> FkOrderEquips { get; set; } = new List<FkOrderEquip>();

    public virtual Adress IdAdressNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
