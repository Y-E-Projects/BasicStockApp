using BL.Abstract;
using DAL.Abstract;
using DTO.Models;
using EL.Concrete;

namespace BL.Managers
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Add(Category entity) => _categoryDal.Create(entity);

        public void Delete(Category entity) => _categoryDal.Remove(entity);

        public List<Category> GetAll() => _categoryDal.GetAll();

        public Category GetByKey(Guid key) => _categoryDal.GetByKey(key);

        public void Update(Category entity) => _categoryDal.Update(entity);

        public List<ListModel.Category> GetList() => _categoryDal.GetList();

        public async Task CreateRangeAsync(List<Category> entities) => await _categoryDal.CreateRangeAsync(entities);

        public async Task<Guid> GetFirstKey() => await _categoryDal.GetFirstKey();

        public async Task<List<Category>> GetByKeys(IEnumerable<Guid> keys) => await _categoryDal.GetByKeys(keys);
    }
}