using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entity;

public partial class Category:INotifyPropertyChanged
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public event PropertyChangedEventHandler? PropertyChanged;
}
