using System;
using System.Collections.Generic;

namespace Entity;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int? Amount { get; set; }

    public double? Price { get; set; }

    public int? TotalPrice { get; set; }

    public virtual Order1 Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
