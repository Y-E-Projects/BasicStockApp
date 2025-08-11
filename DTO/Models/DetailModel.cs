namespace DTO.Models
{
    public class DetailModel
    {
        public class Product
        {
            public required Guid Key { get; set; }
            public required string Name { get; set; }
            public required decimal Price { get; set; }
            public required string Barcode { get; set; }
            public required string Category { get; set; }
            public List<PriceHistoryDetail> PriceHistories { get; set; } = null!;
        }

        public class PriceHistoryDetail
        {
            public required decimal BackPrice { get; set; }
            public required decimal NewPrice { get; set; }
            public required DateTime Date { get; set; }
        }

        public class PriceHistoryWithProduct
        {
            public required string Product { get; set; }
            public List<PriceHistoryDetail> PriceHistories { get; set; } = null!;
        }

        public class Sell
        {
            public required Guid Key { get; set; }
            public required string SellCode { get; set; }
            public required DateTime SellDate { get; set; }
            public required decimal TotalAmount { get; set; }
            public required decimal NetAmount { get; set; }
            public List<SellItemDetail> Items { get; set; } = null!;
        }

        public class SellItemDetail
        {
            public required Guid Key { get; set; }
            public required Guid ProductKey { get; set; }
            public required string ProductName { get; set; }
            public required decimal UnitPrice { get; set; }
            public required int Quantity { get; set; }
            public required decimal LineTotal { get; set; }
        }
    }
}
