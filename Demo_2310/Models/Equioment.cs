using System;
using System.Collections.Generic;

namespace Demo_2310.Models;

public partial class Equioment
{
    public int Id { get; set; }

    public string Articul { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string UnitRent { get; set; } = null!;

    public double CostRent { get; set; }

    public int IdProvider { get; set; }

    public int IdProducer { get; set; }

    public int IdTypeEquipment { get; set; }

    public double Discount { get; set; }

    public double CountFree { get; set; }

    public string Description { get; set; } = null!;

    public string? Photo { get; set; }

    public virtual ICollection<FkOrderEquip> FkOrderEquips { get; set; } = new List<FkOrderEquip>();

    public virtual Producer IdProducerNavigation { get; set; } = null!;

    public virtual Provider IdProviderNavigation { get; set; } = null!;

    public virtual TypeEquipment IdTypeEquipmentNavigation { get; set; } = null!;
}
