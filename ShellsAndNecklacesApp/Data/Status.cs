using System;
using System.Collections.Generic;

namespace ShellAndNecklaceAPI.Data;

public partial class Status
{
    public int Id { get; set; }

    public string? Status1 { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
