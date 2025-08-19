using EL.Concrete;

namespace DTO.Models
{
    public class AddModel
    {
        public class Category
        {
            public required string Name { get; set; }
            public bool IsVisible { get; set; }
        }

        public class Product
        {
            public required string Name { get; set; }
            public decimal Price { get; set; }
            public required string Barcode { get; set; }
            public int Quantity { get; set; }
            public int MinimumQuantity { get; set; }
            public Guid CategoryKey { get; set; }
            public Guid? SupplierKey { get; set; }
        }

        public class Stock
        {
            public StockHistoryType Type { get; set; }
            public Guid ProductKey { get; set; }
            public int Quantity { get; set; }
            public string? Note { get; set; }
        }

        public class CreateSellRequest
        {
            public List<CreateSellItemRequest> Items { get; set; } = new();
        }

        public class CreateSellItemRequest
        {
            public Guid ProductKey { get; set; }
            public int Quantity { get; set; }
        }

        public class ReturnSellItemRequest
        {
            public required string SellCode { get; set; }
            public Guid ProductKey { get; set; }
            public int Quantity { get; set; }
            public string? ReturnNote { get; set; }
        }

        public class Supplier
        {
            public required string Name { get; set; }
            public required string ContactName { get; set; }
            public required string Phone { get; set; }
            public required string Email { get; set; }
            public required string Address { get; set; }
        }
    }
}
