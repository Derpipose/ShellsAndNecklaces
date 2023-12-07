using System;
using System.Collections.Generic;

namespace ShellAndNecklaceAPI.Data;

public partial class Cart
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public decimal? Actualprice { get; set; }

    public int? Itemid { get; set; }

    public int? Accountid { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Item? Item { get; set; }
}
