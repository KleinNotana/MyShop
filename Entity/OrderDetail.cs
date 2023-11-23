using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entity;

public partial class OrderDetail:INotifyPropertyChanged
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int? Amount { get; set; }

    public int? Price { get; set; }

    public int? TotalPrice { get; set; }

    public virtual Order1 Order { get; set; } = null!;

    public virtual Product? Pr { get; set; }

    public virtual Product Product { get; set; } = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
}
