using System.Drawing;

namespace ShellAndNecklaceAPI.Data.DTOs
{
    public class ItemDTO
    {
        public String Name {  get; set; }
        public String Description { get; set; }
        public String Status { get; set; }
        public decimal PriceBase { get; set; }
        public String Picture { get; set; }
    }
}
