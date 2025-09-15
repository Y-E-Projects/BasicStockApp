using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    [Index(nameof(Name), Name = "IX_Supplier_Name")]
    [Index(nameof(Email), IsUnique = true, Name = "IX_Supplier_Email")]
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
