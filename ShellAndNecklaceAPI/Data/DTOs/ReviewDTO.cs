using System.Drawing;

namespace ShellAndNecklaceAPI.Data.DTOs
{
	public class ReviewDTO
	{
		public String Username { get; set; }
		public int RatingScore { get; set; }
		public String ReviewText { get; set; }
		public DateTime ReviewDate {  get; set; }
	}
}
