using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFramework
{
    public class EFCategoryDal : GenericRep<Category>, ICategoryDal
    {
        public EFCategoryDal(MainDbContext context) : base(context)
        {
        }

        public List<Category> FullAttached()
        {
            var values = _context.Categories
                .Include(c => c.Products)
                .ToList();

            return values;
        }
    }
}