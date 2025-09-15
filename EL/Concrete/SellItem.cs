using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    [Index(nameof(SellKey), Name = "IX_SellItem_SellKey")]
    [Index(nameof(ProductKey), Name = "IX_SellItem_ProductKey")]
    public class SellItem : BaseEntity
    {
        public Guid SellKey { get; set; }
        public Sell Sell { get; set; } = null!;

        public Guid ProductKey { get; set; }
        public Product Product { get; set; } = null!;

        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        [Precision(18, 2)]
        public decimal LineTotal { get; set; }

        public List<ReturnHistory> ReturnHistories { get; set; } = new();
    }
}
