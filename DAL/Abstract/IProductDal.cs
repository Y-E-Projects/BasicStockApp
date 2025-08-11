using DAL.Generics;
using EL.Concrete;

namespace DAL.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        List<Product> CategoryAttached();
        List<Product> CategoryAndPriceAttached();
        List<Product> FullAttached();
    }
}