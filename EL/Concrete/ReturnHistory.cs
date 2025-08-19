using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    public class ReturnHistory : BaseEntity
    {
        public Guid SellItemKey { get; set; }
        public SellItem SellItem { get; set; } = null!;

        public Guid ProductKey { get; set; }
        public Product Product { get; set; } = null!;

        [Precision(18, 2)]
        public decimal UnitPrice { get; set; } // Satış anındaki fiyat (SellItem.UnitPrice'dan alınacak)
        public int Quantity { get; set; }
        public string? Reason { get; set; }
    }
}
