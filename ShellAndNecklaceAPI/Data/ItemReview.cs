using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShellAndNecklaceAPI.Data;

public partial class ItemReview
{
    [Key]
    public int Id { get; set; }

    public int? Itemid { get; set; }

    public int? Accountid { get; set; }
    [Required]
    public int Rating { get; set; }

    public DateTime Reviewdate { get; set; }

    public string Reviewtext { get; set; } = null!;

    public virtual Account? Account { get; set; }

    public virtual Item? Item { get; set; }
}
