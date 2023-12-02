namespace ShellAndNecklaceAPI.Data.DTOs
{
    public class ItemReviewDTO
    {
        public String Username {  get; set; }
        public String ItemReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime? ReviewDate { get; set; }
        public String ItemName { get; set; }
        public String ItemPicture { get; set; }
    }
}
