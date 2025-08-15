using DAL.Generics;
using DTO.Models;
using EL.Concrete;

namespace DAL.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        List<Product> CategoryAttached();
        List<Product> CategoryAndPriceAttached();
        List<Product> FullAttached();
        void DecreaseQuantity(Guid productKey, int quantity);
        void IncreaseQuantity(Guid productKey, int quantity);
        List<Product> GetByKeys(List<Guid> productKeys);
        void UpdateQuantities(List<AddModel.Stock> models);
    }
}