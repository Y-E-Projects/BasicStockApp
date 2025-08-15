using EL.Concrete;

namespace DTO.Models
{
    public class ListModel
    {
        public class Product
        {
            public required Guid Key { get; set; }
            public required string Name { get; set; }
            public required decimal Price { get; set; }
            public required string Barcode { get; set; }
            public required string Category { get; set; }
            public int Quantity { get; set; }
        }

        public class Category
        {
            public required Guid Key { get; set; }
            public required string Name { get; set; }
            public int Count { get; set; }
            public bool IsVisible { get; set; }
        }

        public class Sell
        {
            public required string SellCode { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal TotalDiscount { get; set; }
            public decimal NetAmount { get; set; }
            public int ProductCount { get; set; }
            public required DateTime Date { get; set; }
        }

        public class StockHistory
        {
            public Guid Key { get; set; }
            public StockHistoryType Type { get; set; }
            public required string Product { get; set; }
            public int Quantity { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
