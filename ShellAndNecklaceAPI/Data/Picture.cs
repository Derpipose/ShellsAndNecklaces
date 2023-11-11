using System;
using System.Collections.Generic;

namespace ShellAndNecklaceAPI.Data;

public partial class Picture
{
    public int Id { get; set; }

    public int? Filetypeid { get; set; }

    public string? Imagename { get; set; }

    public virtual Filetype? Filetype { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
