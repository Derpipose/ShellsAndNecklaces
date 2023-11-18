using System;
using System.Collections.Generic;

namespace ShellAndNecklaceAPI.Data;

public partial class Account
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool? Verified { get; set; }
    public bool? Closed { get; set; }
    public virtual ICollection<Purchaseorder> Purchaseorders { get; set; } = new List<Purchaseorder>();
}
