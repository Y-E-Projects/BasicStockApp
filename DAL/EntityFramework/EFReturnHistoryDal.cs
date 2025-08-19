using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using EL.Concrete;

namespace DAL.EntityFramework
{
    public class EFReturnHistoryDal : GenericRep<ReturnHistory>, IReturnHistoryDal
    {
        public EFReturnHistoryDal(MainDbContext context) : base(context)
        {
        }
    }
}