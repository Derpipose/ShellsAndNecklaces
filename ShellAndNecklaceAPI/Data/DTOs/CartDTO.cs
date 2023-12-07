namespace ShellAndNecklaceAPI.Data.DTOs
{
    public class CartDTO
    {
        public int id { get; set; }
        public string itemname { get; set; }
        public string email { get; set; }
        public int quantity { get; set; }
        public decimal actualprice { get; set; }

    }
}
