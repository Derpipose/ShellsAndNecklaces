using System;
using System.Collections.Generic;

namespace ShellAndNecklaceAPI.Data;

public partial class Orderitem
{
    public int Id { get; set; }

    public int? Itemid { get; set; }

    public int? Orderid { get; set; }

    public int Quantity { get; set; }

    public decimal Pricepaid { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Purchaseorder? Order { get; set; }
}
