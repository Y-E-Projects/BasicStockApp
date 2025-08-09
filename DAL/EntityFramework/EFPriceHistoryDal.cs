using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using EL.Concrete;

namespace DAL.EntityFramework
{
    public class EFPriceHistoryDal : GenericRep<PriceHistory>, IPriceHistoryDal
    {
        public EFPriceHistoryDal(MainDbContext context) : base(context)
        {
        }
    }
}