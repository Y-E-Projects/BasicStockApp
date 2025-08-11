using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using EL.Concrete;
using Microsoft.EntityFrameworkCore;

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
    }
}