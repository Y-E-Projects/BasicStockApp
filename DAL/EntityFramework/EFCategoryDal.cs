using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using EL.Concrete;

namespace DAL.EntityFramework
{
    public class EFCategoryDal : GenericRep<Category>, ICategoryDal
    {
        public EFCategoryDal(MainDbContext context) : base(context)
        {
        }
    }
}