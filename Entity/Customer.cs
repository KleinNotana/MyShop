using System;
using System.Collections.Generic;

namespace Entity;

public partial class Customer
{
    public int Id { get; set; }

    public string? CustomerName { get; set; }

    public int? Age { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Order1> Order1s { get; set; } = new List<Order1>();
}
