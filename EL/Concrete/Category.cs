using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    [Index(nameof(Name), Name = "IX_Category_Name")]
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public bool IsVisible { get; set; }

        public List<Product> Products { get; set; } = null!;
    }
}
