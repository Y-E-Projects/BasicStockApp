using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Generics
{
    public class GenericRep<T> : IGenericDal<T> where T : class
    {
        protected readonly MainDbContext _context;

        public GenericRep(MainDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Where(filter).AsNoTracking().ToList();
        }

        public T GetByKey(Guid key)
        {
            return _context.Set<T>().Find(key);
        }

        public void Remove(T t)
        {
            _context.Remove(t);
            _context.SaveChanges();
        }

        public void Update(T t)
        {
            _context.Update(t);
            _context.SaveChanges();
        }
    }
}
