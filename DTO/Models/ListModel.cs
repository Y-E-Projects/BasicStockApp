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
        }

        public class Category
        {
            public required Guid Key { get; set; }
            public required string Name { get; set; }
            public int Count { get; set; }
            public bool IsVisible { get; set; }
        }
    }
}
