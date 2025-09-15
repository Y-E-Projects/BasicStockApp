using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using DTO.Models;
using EL.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        public List<ListModel.Category> GetList()
        {
            var values = _context.Categories
                .Select(c => new ListModel.Category
                {
                    Key = c.Key,
                    Name = c.Name,
                    Count = c.Products.Count,
                    IsVisible = c.IsVisible
                }).ToList();

            return values;
        }

        public async Task CreateRangeAsync(List<Category> entities)
        {
            await _context.Categories.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> GetFirstKey()
        {
            var key = await _context.Categories
                .Select(c => c.Key)
                .FirstOrDefaultAsync();

            return key;
        }

        public async Task<List<Category>> GetByKeys(IEnumerable<Guid> keys)
        {
            return await _context.Categories
                .Where(c => keys.Contains(c.Key))
                .ToListAsync();
        }
    }
}