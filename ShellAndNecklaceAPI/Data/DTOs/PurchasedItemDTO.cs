﻿namespace ShellAndNecklaceAPI.Data.DTOs
{
    public class PurchasedItemDTO
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
        public string PictureString { get; set; }
    }
}
