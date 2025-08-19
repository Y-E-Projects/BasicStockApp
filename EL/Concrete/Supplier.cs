using EL.Concrete.Base;

namespace EL.Concrete
{
    public class Supplier : BaseEntity
    {
        public required string Name { get; set; }
        public required string ContactName { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}
