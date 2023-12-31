﻿using System;
using System.Collections.Generic;

namespace ShellAndNecklaceAPI.Data;

public partial class Purchaseorder
{
    public int Id { get; set; }

    public int? Accountid { get; set; }

    public string? Notes { get; set; }

    public DateTime Orderdate { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
