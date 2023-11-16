namespace ShellAndNecklaceAPI.Data.DTOs
{
    public class ItemPurchasedDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
        public string PictureName { get; set; }
        public string PictureFileType { get; set; }
    }
}
