namespace ShellAndNecklaceAPI.Data.DTOs
{
    public class OrderDTO
    {
        public List<PurchasedItemDTO> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string Notes { get; set; }
    }
}
