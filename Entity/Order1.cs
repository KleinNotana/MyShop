using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entity;

public partial class Order1:INotifyPropertyChanged
{
    public int Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public event PropertyChangedEventHandler? PropertyChanged;
}
