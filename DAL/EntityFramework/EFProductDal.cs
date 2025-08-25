using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;

        public EFProductDal(
            MainDbContext context, 
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
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
            return _context.Products.Where(p => p.CategoryKey == categoryKey)
                .ProjectTo<ListModel.Product>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public List<ListModel.Product> GetListWithSupplier(Guid supplierKey)
        {
            return _context.Products.Where(p => p.SupplierKey == supplierKey)
                .ProjectTo<ListModel.Product>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public DetailModel.Product? GetDetailWithKey(Guid key)
        {
            return _context.Products.Where(p => p.Key == key)
                .ProjectTo<DetailModel.Product?>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public DetailModel.Product? GetDetailWithBarcode(string barcode)
        {
            return _context.Products.Where(p => p.Barcode == barcode)
                .ProjectTo<DetailModel.Product?>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public List<ListModel.Product> GetList() => _context.Products.ProjectTo<ListModel.Product>(_mapper.ConfigurationProvider).ToList();

        public List<ListModel.Product> GetListWithLowStock() => _context.Products
            .Where(x => x.Quantity <= x.MinimumQuantity)
            .ProjectTo<ListModel.Product>(_mapper.ConfigurationProvider).ToList();

        public List<ListModel.TopSellProduct> GetTopProducts(int count, DateTime start, DateTime end) => _context.Products
            .Where(p => p.SellItems.Any(si => si.Sell.CreatedAt >= start && si.Sell.CreatedAt <= end))
            .OrderByDescending(p =>
                p.SellItems.Sum(si => si.Quantity) - p.SellItems.SelectMany(si => si.ReturnHistories).Sum(rh => (int?)rh.Quantity) ?? 0
            )
            .Take(count)
            .ProjectTo<ListModel.TopSellProduct>(_mapper.ConfigurationProvider)
            .ToList();

    }
}
