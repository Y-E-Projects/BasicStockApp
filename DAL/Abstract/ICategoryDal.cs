using DAL.Generics;
using EL.Concrete;

namespace DAL.Abstract
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        List<Category> FullAttached();
    }
}