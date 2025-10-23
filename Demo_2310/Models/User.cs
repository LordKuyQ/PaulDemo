using System;
using System.Collections.Generic;

namespace Demo_2310.Models;

public partial class User
{
    public int Id { get; set; }

    public int IdRole { get; set; }

    public string Fio { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
