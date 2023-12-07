using System;
using System.Collections.Generic;

namespace ShellAndNecklaceAPI.Data;

public partial class Account
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public bool? Verified { get; set; }

    public bool? Closed { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<ItemReview> Itemreviews { get; set; } = new List<ItemReview>();

    public virtual ICollection<Purchaseorder> Purchaseorders { get; set; } = new List<Purchaseorder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
