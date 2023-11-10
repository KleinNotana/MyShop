using System;
using System.Collections.Generic;

namespace Entity;

public partial class Order1
{
    public int Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
