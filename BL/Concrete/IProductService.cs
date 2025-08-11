using BL.Generics;
using DTO.Models;
using EL.Concrete;

namespace BL.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        List<ListModel.Product> GetList();
        bool CheckBarcodeExists(string barcode);
        DetailModel.Product? GetWithBarcode(string barcode);
        List<ListModel.Product> GetListWithCategory(Guid categoryKey);
        DetailModel.Product? GetDetailWithKey(Guid key);
    }
}