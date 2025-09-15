using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    [Index(nameof(SellItemKey), Name = "IX_ReturnHistory_SellItemKey")]
    [Index(nameof(ProductKey), Name = "IX_ReturnHistory_ProductKey")]
    public class ReturnHistory : BaseEntity
    {
        public Guid SellItemKey { get; set; }
        public SellItem SellItem { get; set; } = null!;

        public Guid ProductKey { get; set; }
        public Product Product { get; set; } = null!;

        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string? Reason { get; set; }
    }
}
