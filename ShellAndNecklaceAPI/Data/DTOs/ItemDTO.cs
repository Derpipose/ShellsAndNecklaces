using System.Drawing;

namespace ShellAndNecklaceAPI.Data.DTOs
{
    public class ItemDTO
    {
        public String Name {  get; set; }
        public String Description { get; set; }
        public int StatusId { get; set; }
        public decimal PriceBase { get; set; }
        public int PicId { get; set; }
    }
}
