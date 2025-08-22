using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    public class Sell : BaseEntity
    {
        public required string SellCode { get; set; } // Örn. S20250809-0001

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }

        [Precision(18, 2)]
        public decimal TotalDiscount { get; set; }

        [Precision(18, 2)]
        public decimal NetAmount { get; set; } 

        public List<SellItem> Items { get; set; } = new();
    }
}
