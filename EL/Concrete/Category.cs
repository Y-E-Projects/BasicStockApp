using EL.Concrete.Base;

namespace EL.Concrete
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public bool IsVisible { get; set; }

        public List<Product> Products { get; set; } = null!;
    }
}
