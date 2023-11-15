using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace ShellAndNecklaceAPI.Data.DTOs
{
    public class PurchaseLineDTO
    {
        public DateTime OrderDate { get; set; }
        public List<ItemPurchasedDTO> Items { get; set; }
        public string Notes { get; set; }
        public decimal PricePaid { get; set; }
        public string Email { get; set; }

    }
}
