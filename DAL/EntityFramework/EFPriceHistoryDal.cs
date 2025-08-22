using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFramework
{
    public class EFPriceHistoryDal : GenericRep<PriceHistory>, IPriceHistoryDal
    {
        public EFPriceHistoryDal(MainDbContext context) : base(context)
        {
        }

        public List<PriceHistory> FullAttached()
        {
            var values = _context.PriceHistories
                .Include(x => x.Product)
                .ToList();

            return values;
        }
    }
}