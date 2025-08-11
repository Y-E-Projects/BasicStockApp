using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFramework
{
    public class EFSellDal : GenericRep<Sell>, ISellDal
    {
        public EFSellDal(MainDbContext context) : base(context)
        {
        }

        public List<Sell> FullAttached()
        {
            var values = _context.Sells
                .Include(s => s.Items)
                    .ThenInclude(si => si.Product)
                .ToList();

            return values;
        }
    }
}