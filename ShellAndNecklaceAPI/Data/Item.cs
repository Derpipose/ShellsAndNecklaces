﻿using System;
using System.Collections.Generic;

namespace ShellAndNecklaceAPI.Data;

public partial class Item
{
    public int Id { get; set; }

    public string Itemname { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? Pictureid { get; set; }

    public decimal Pricebase { get; set; }

    public int? Statusid { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<ItemReview> Itemreviews { get; set; } = new List<ItemReview>();

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Picture? Picture { get; set; }

    public virtual Status? Status { get; set; }
}
