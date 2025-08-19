using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using DTO.Models;
using EL.Concrete;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.EntityFramework
{
    public class EFProductDal : GenericRep<Product>, IProductDal
    {
        public EFProductDal(MainDbContext context) : base(context)
        {
        }

        public List<Product> CategoryAttached()
        {
            var values = _context.Products
                .Include(p => p.Category)
                .ToList();

            return values;
        }

        public List<Product> CategoryAndPriceAttached()
        {
            var values = _context.Products
                .Include(p => p.Category)
                .Include(p => p.PriceHistories)
                .ToList();

            return values;
        }

        public List<Product> FullAttached()
        {
            var values = _context.Products
                .Include(p => p.Category)
                .Include(p => p.PriceHistories)
                .Include(p => p.SellItems)
                    .ThenInclude(si => si.Sell)
                .ToList();

            return values;
        }

        public void DecreaseQuantity(Guid productKey, int quantity)
        {
            var value = _context.Products
                .FirstOrDefault(p => p.Key == productKey);

            if (value == null)
                return;

            value.Quantity -= quantity;
            _context.Products.Update(value);
            _context.SaveChanges();
        }

        public void IncreaseQuantity(Guid productKey, int quantity)
        {
            var value = _context.Products
                .FirstOrDefault(p => p.Key == productKey);

            if (value == null)
                return;

            value.Quantity += quantity;
            _context.Products.Update(value);
            _context.SaveChanges();
        }

        public List<Product> GetByKeys(List<Guid> productKeys)
        {
            return _context.Products
                .Where(p => productKeys.Contains(p.Key))
                .ToList();
        }

        public void UpdateQuantities(List<AddModel.Stock> models)
        {
            var productKeys = models.Select(m => m.ProductKey).ToList();
            var products = _context.Products
                .Where(p => productKeys.Contains(p.Key))
                .ToList();

            foreach (var model in models)
            {
                var product = products.FirstOrDefault(p => p.Key == model.ProductKey);
                if (product == null)
                    continue;

                if (model.Type == StockHistoryType.Adding)
                    product.Quantity += model.Quantity;
                else
                    product.Quantity -= model.Quantity;
            }

            _context.Products.UpdateRange(products);
            _context.SaveChanges();
        }

        public List<ListModel.Product> GetListWithCategory(Guid categoryKey)
        {
            return _context.Products.Where(p => p.CategoryKey == categoryKey).Select(p => new ListModel.Product
            {
                Key = p.Key,
                Name = p.Name,
                Price = p.Price,
                Barcode = p.Barcode,
                Category = p.Category.Name,
                Quantity = p.Quantity,
            }).ToList();
        }

        public List<ListModel.Product> GetListWithSupplier(Guid supplierKey)
        {
            return _context.Products.Where(p => p.SupplierKey == supplierKey).Select(p => new ListModel.Product
            {
                Key = p.Key,
                Name = p.Name,
                Price = p.Price,
                Barcode = p.Barcode,
                Category = p.Category.Name,
                Quantity = p.Quantity,
            }).ToList();
        }

        public DetailModel.Product? GetDetailWithKey(Guid key)
        {
            return _context.Products.Where(p => p.Key == key).Select(p => new DetailModel.Product
            {
                Key = p.Key,
                Name = p.Name,
                Price = p.Price,
                Barcode = p.Barcode,
                Category = p.Category.Name,
                Quantity = p.Quantity,
                PriceHistories = p.PriceHistories.OrderBy(x => x.CreatedAt).Select(ph => new DetailModel.PriceHistoryDetail
                {
                    BackPrice = ph.BackPrice,
                    NewPrice = ph.NewPrice,
                    Date = ph.CreatedAt
                }).ToList()
            }).FirstOrDefault();
        }

        public DetailModel.Product? GetDetailWithBarcode(string barcode)
        {
            return _context.Products.Where(p => p.Barcode == barcode).Select(p => new DetailModel.Product
            {
                Key = p.Key,
                Name = p.Name,
                Price = p.Price,
                Barcode = p.Barcode,
                Category = p.Category.Name,
                Quantity = p.Quantity,
                Supplier = p.Supplier != null ? p.Supplier.Name : null,
                SupplierKey = p.SupplierKey ?? null,
                PriceHistories = p.PriceHistories.OrderBy(x => x.CreatedAt).Select(ph => new DetailModel.PriceHistoryDetail
                {
                    BackPrice = ph.BackPrice,
                    NewPrice = ph.NewPrice,
                    Date = ph.CreatedAt
                }).ToList()
            }).FirstOrDefault();
        }

        public List<ListModel.Product> GetList()
        {
            return _context.Products.Select(p => new ListModel.Product
            {
                Key = p.Key,
                Name = p.Name,
                Price = p.Price,
                Barcode = p.Barcode,
                Category = p.Category.Name,
                Quantity = p.Quantity,
            }).ToList();
        }
    }
}
