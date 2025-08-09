using BL.Abstract;
using DAL.Abstract;
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
    }
}