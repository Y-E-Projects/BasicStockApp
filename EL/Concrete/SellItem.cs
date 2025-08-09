using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    public class SellItem : BaseEntity
    {
        public Guid SellKey { get; set; }
        public Sell Sell { get; set; } = null!;

        public Guid ProductKey { get; set; }
        public Product Product { get; set; } = null!;

        [Precision(18, 2)]
        public decimal UnitPrice { get; set; } // Satış anındaki fiyat
        public int Quantity { get; set; }

        [Precision(18, 2)]
        public decimal LineTotal { get; set; } // UnitPrice * Quantity
    }
}
