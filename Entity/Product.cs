﻿using System;
using System.Collections.Generic;

namespace Entity;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public double? Price { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public string? ImgPath { get; set; }

    public int? Amount { get; set; }

    public int? Discount { get; set; }

    public DateTime? DiscountDate { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
