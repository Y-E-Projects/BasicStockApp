using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    public enum StockHistoryType
    {
        Adding = 1,
        Decrease = -1,
    }

    [Index(nameof(ProductKey), Name = "IX_StockHistory_ProductKey")]
    public class StockHistory : BaseEntity
    {
        public StockHistoryType Type { get; set; }

        public Guid ProductKey { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        public string? Note { get; set; }
    }
}
