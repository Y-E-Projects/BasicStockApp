using BL.Abstract;
using DAL.Abstract;
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

        public Product? GetWithBarcode(string barcode) => _productDal.GetAll().FirstOrDefault(p => p.Barcode == barcode);
    }
}
