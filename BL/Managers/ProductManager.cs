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

        public DetailModel.Product? GetWithBarcode(string barcode)
        {
            var value = _productDal.CategoryAndPriceAttached()
                .Where(p => p.Barcode == barcode)
                .FirstOrDefault();

            if (value == null) return null;

            return new DetailModel.Product
            {
                Key = value.Key,
                Name = value.Name,
                Price = value.Price,
                Barcode = value.Barcode,
                Category = value.Category.Name,
                Quantity = value.Quantity,
                PriceHistories = value.PriceHistories.OrderBy(x => x.CreatedAt).Select(ph => new DetailModel.PriceHistoryDetail
                {
                    BackPrice = ph.BackPrice,
                    NewPrice = ph.NewPrice,
                    Date = ph.CreatedAt
                }).ToList()
            };
        }

        public List<ListModel.Product> GetListWithCategory(Guid categoryKey)
        {
            var products = _productDal.CategoryAttached()
                .Where(p => p.CategoryKey == categoryKey)
                .ToList();

            if (products == null || !products.Any())
                return new List<ListModel.Product>();

            return products.Select(p => new ListModel.Product
            {
                Key = p.Key,
                Name = p.Name,
                Price = p.Price,
                Barcode = p.Barcode,
                Category = p.Category.Name,
                Quantity = p.Quantity,
            }).ToList();
        }

        public DetailModel.Product? GetDetailWithKey(Guid key)
        {
            var value = _productDal.CategoryAndPriceAttached()
                .Where(p => p.Key == key)
                .FirstOrDefault();

            if (value == null) return null;

            return new DetailModel.Product
            {
                Key = value.Key,
                Name = value.Name,
                Price = value.Price,
                Barcode = value.Barcode,
                Category = value.Category.Name,
                Quantity = value.Quantity,
                PriceHistories = value.PriceHistories.OrderBy(x => x.CreatedAt).Select(ph => new DetailModel.PriceHistoryDetail
                {
                    BackPrice = ph.BackPrice,
                    NewPrice = ph.NewPrice,
                    Date = ph.CreatedAt
                }).ToList()
            };
        }

        public List<ListModel.Product> GetList()
        {
            var products = _productDal.CategoryAttached().ToList();

            if (products == null || !products.Any())
                return new List<ListModel.Product>();

            return products.Select(p => new ListModel.Product
            {
                Key = p.Key,
                Name = p.Name,
                Price = p.Price,
                Barcode = p.Barcode,
                Category = p.Category.Name,
                Quantity = p.Quantity,
            }).ToList();
        }

        public void DecreaseQuantity(Guid productKey, int quantity)
        {
            _productDal.DecreaseQuantity(productKey, quantity);
        }

        public void IncreaseQuantity(Guid productKey, int quantity)
        {
            _productDal.IncreaseQuantity(productKey, quantity);
        }

        public List<Product> GetByKeys(List<Guid> productKeys)
        {
            return _productDal.GetByKeys(productKeys);
        }

        public void UpdateQuantities(List<AddModel.Stock> models)
        {
            _productDal.UpdateQuantities(models);
        }
    }
}
