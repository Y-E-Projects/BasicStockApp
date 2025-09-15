using DAL.Generics;
using DTO.Models;
using EL.Concrete;

namespace DAL.Abstract
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        List<Category> FullAttached();
        List<ListModel.Category> GetList();
        Task CreateRangeAsync(List<Category> entities);
        Task<Guid> GetFirstKey();
        Task<List<Category>> GetByKeys(IEnumerable<Guid> keys);
    }
}