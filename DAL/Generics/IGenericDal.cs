using System.Linq.Expressions;

namespace DAL.Generics
{
    public interface IGenericDal<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Remove(T entity);
        List<T> GetAll();
        T GetByKey(Guid key);
        List<T> GetAll(Expression<Func<T, bool>> filter);
    }
}
