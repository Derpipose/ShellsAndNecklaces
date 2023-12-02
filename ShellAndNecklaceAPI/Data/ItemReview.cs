namespace ShellAndNecklaceAPI.Data
{
    public class ItemReview
    {
        public int Id { get; set; }

        public int Rating { get; set; }
        public int? Itemid { get; set; }
        public int? Accountid { get; set; }

        public DateTime? Reviewdate { get; set; }
        public string Reviewtext { get; set; } = null!;
        public virtual Item? Item { get; set; }
        public virtual Account? Account { get; set; }
    }
}
