using EL.Concrete;
using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

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
            public Guid CategoryKey { get; set; }
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
    }
}
