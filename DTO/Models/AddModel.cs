using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace DTO.Models
{
    public class AddModel
    {
        public class Category
        {
            public required string Name { get; set; }
        }

        public class Product
        {
            public required string Name { get; set; }
            public decimal Price { get; set; }
            public required string Barcode { get; set; }
            public Guid CategoryKey { get; set; }
        }

        public class PriceHistory
        {
            public decimal NewPrice { get; set; }
            public Guid ProductKey { get; set; }
        }
    }
}
