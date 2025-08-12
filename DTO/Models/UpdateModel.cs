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
            public decimal NewPrice { get; set; }
            public Guid ProductKey { get; set; }
        }
    }
}
