using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using EL.Concrete;

namespace DAL.EntityFramework
{
    public class EFSellDal : GenericRep<Sell>, ISellDal
    {
        public EFSellDal(MainDbContext context) : base(context)
        {
        }
    }
}