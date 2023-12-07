using System;
using System.Collections.Generic;

namespace ShellAndNecklaceAPI.Data;

public partial class Review
{
    public int Id { get; set; }

    public int Rating { get; set; }

    public int? Accountid { get; set; }

    public string Reviewbody { get; set; } = null!;

    public DateTime Reviewdate { get; set; }

    public virtual Account? Account { get; set; }
}
