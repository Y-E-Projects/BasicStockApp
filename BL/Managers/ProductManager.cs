using BL.Abstract;
using DAL.Abstract;
using DTO.Models;
using EL.Concrete;

namespace BL.Managers
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product entity) => _productDal.Create(entity);

        public void Delete(Product entity) => _productDal.Remove(entity);

        public List<Product> GetAll() => _productDal.GetAll();

        public Product GetByKey(Guid key) => _productDal.GetByKey(key);

        public void Update(Product entity) => _productDal.Update(entity);

        public bool CheckBarcodeExists(string barcode) => _productDal.GetAll().Any(p => p.Barcode == barcode);

        public DetailModel.Product? GetDetailWithKey(Guid key) => _productDal.GetDetailWithKey(key);

        public DetailModel.Product? GetWithBarcode(string barcode) => _productDal.GetDetailWithBarcode(barcode);

        public List<ListModel.Product> GetListWithCategory(Guid categoryKey) => _productDal.GetListWithCategory(categoryKey);

        public List<ListModel.Product> GetListWithSupplier(Guid supplierKey) => _productDal.GetListWithSupplier(supplierKey);

        public List<ListModel.Product> GetList() => _productDal.GetList();

        public void DecreaseQuantity(Guid productKey, int quantity) => _productDal.DecreaseQuantity(productKey, quantity);

        public void IncreaseQuantity(Guid productKey, int quantity) => _productDal.IncreaseQuantity(productKey, quantity);

        public List<Product> GetByKeys(List<Guid> productKeys) => _productDal.GetByKeys(productKeys);

        public void UpdateQuantities(List<AddModel.Stock> models) => _productDal.UpdateQuantities(models);

        public List<ListModel.Product> GetListWithLowStock() => _productDal.GetListWithLowStock();

        public List<ListModel.TopSellProduct> GetTopProducts(int count) => _productDal.GetTopProducts(count);
    }
}
