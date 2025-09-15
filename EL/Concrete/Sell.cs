using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    [Index(nameof(SellCode), IsUnique = true, Name = "IX_Sell_SellCode")]
    public class Sell : BaseEntity
    {
        public required string SellCode { get; set; }

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }

        [Precision(18, 2)]
        public decimal TotalDiscount { get; set; }

        [Precision(18, 2)]
        public decimal NetAmount { get; set; }

        public List<SellItem> Items { get; set; } = new();
    }
}
