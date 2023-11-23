using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entity;

public partial class Product:INotifyPropertyChanged
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public int? Price { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public string? ImgPath { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetailProducts { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderDetail> OrderDetailPrs { get; set; } = new List<OrderDetail>();

    public event PropertyChangedEventHandler? PropertyChanged;
}
