using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using EL.Concrete;

namespace DAL.EntityFramework
{
    public class EFSellItemDal : GenericRep<SellItem>, ISellItemDal
    {
        public EFSellItemDal(MainDbContext context) : base(context)
        {
        }
    }
}