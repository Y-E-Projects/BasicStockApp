using EL.Concrete.Base;
using Microsoft.EntityFrameworkCore;

namespace EL.Concrete
{
    [Index(nameof(Barcode), IsUnique = true, Name = "IX_Product_Barcode")]
    [Index(nameof(Name), Name = "IX_Product_Name")]
    [Index(nameof(CategoryKey), Name = "IX_Product_CategoryKey")]
    [Index(nameof(SupplierKey), Name = "IX_Product_SupplierKey")]
    public class Product : BaseEntity
    {
        public required string Name { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public required string Barcode { get; set; }

        public int Quantity { get; set; }
        public int MinimumQuantity { get; set; }

        public Guid CategoryKey { get; set; }
        public Category Category { get; set; } = null!;

        public Guid? SupplierKey { get; set; }
        public Supplier? Supplier { get; set; }

        public bool IsVisible { get; set; }

        public List<PriceHistory> PriceHistories { get; set; } = new();
        public List<SellItem> SellItems { get; set; } = new();
        public List<StockHistory> StockHistories { get; set; } = new();
        public List<ReturnHistory> ReturnHistories { get; set; } = new();
    }
}
