using System;
using System.Collections.Generic;

namespace ShellAndNecklaceAPI.Data;

public partial class Filetype
{
    public int Id { get; set; }

    public string Fileextension { get; set; } = null!;

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();
}
