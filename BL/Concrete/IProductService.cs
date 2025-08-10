using BL.Generics;
using EL.Concrete;

namespace BL.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        bool CheckBarcodeExists(string barcode);
        Product? GetWithBarcode(string barcode);
    }
}