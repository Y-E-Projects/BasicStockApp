using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using EL.Concrete;

namespace DAL.EntityFramework
{
    public class EFProductDal : GenericRep<Product>, IProductDal
    {
        public EFProductDal(MainDbContext context) : base(context)
        {
        }
    }
}