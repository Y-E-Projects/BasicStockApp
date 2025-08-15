using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        public required string Barcode { get; set; }
        public int Quantity { get; set; }

        public Guid CategoryKey { get; set; }
        public Category Category { get; set; } = null!;

        public List<PriceHistory> PriceHistories { get; set; } = null!;
        public List<SellItem> SellItems { get; set; } = null!;
        public List<StockHistory> StockHistories { get; set; } = null!;
    }
}
