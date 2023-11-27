using System.Drawing;

namespace ShellAndNecklaceAPI.Data.DTOs
{
	public class ReviewDTO
	{
		public String Userame { get; set; }
		public int RatingScore { get; set; }
		public String ReviewText { get; set; }
		public decimal PriceBase { get; set; }
		public String PicString { get; set; }
		public DateTime ReviewDate {  get; set; }
	}
}
