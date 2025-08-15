using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using DTO.Models;
using EL.Concrete;
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
    }
}