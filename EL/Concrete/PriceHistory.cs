using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    public class PriceHistory : BaseEntity
    {
        [Precision(18, 2)]
        public decimal BackPrice { get; set; }

        [Precision(18, 2)]
        public decimal NewPrice { get; set; }

        public Guid ProductKey { get; set; }
        public Product Product { get; set; } = null!;
    }
}
