namespace DTO.Models
{
    public class UpdateModel
    {
        public class CategoryName
        {
            public required Guid Key { get; set; }
            public required string Name { get; set; }
        }

        public class ProductPrice
        {
            public Guid ProductKey { get; set; }
            public decimal NewPrice { get; set; }
        }

        public class ProductName
        {
            public Guid ProductKey { get; set; }
            public required string Name { get; set; }
        }

        public class Product
        {
            public Guid Key { get; set; }
            public Guid? SupplierKey { get; set; }
            public Guid CategoryKey { get; set; }
            public int MinimumQuantity { get; set; }
            public required string Barcode { get; set; }
        }
    }
}
