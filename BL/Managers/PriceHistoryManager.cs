using BL.Abstract;
using DAL.Abstract;
using DTO.Models;
using EL.Concrete;

namespace BL.Managers
{
    public class PriceHistoryManager : IPriceHistoryService
    {
        private readonly IPriceHistoryDal _priceHistoryDal;

        public PriceHistoryManager(IPriceHistoryDal priceHistoryDal)
        {
            _priceHistoryDal = priceHistoryDal;
        }

        public void Add(PriceHistory entity) => _priceHistoryDal.Create(entity);

        public void Delete(PriceHistory entity) => _priceHistoryDal.Remove(entity);

        public List<PriceHistory> GetAll() => _priceHistoryDal.GetAll();

        public PriceHistory GetByKey(Guid key) => _priceHistoryDal.GetByKey(key);

        public void Update(PriceHistory entity) => _priceHistoryDal.Update(entity);

        public DetailModel.PriceHistoryWithProduct? GetWithProduct(Guid productKey)
        {
            var values = _priceHistoryDal.FullAttached()
                .Where(x => x.ProductKey == productKey)
                .OrderBy(x => x.CreatedAt)
                .ToList();

            if (!values.Any())
                return null;

            return new DetailModel.PriceHistoryWithProduct
            {
                Product = values.First().Product.Name,
                PriceHistories = values.Select(x => new DetailModel.PriceHistoryDetail
                {
                    BackPrice = x.BackPrice,
                    NewPrice = x.NewPrice,
                    Date = x.CreatedAt
                }).ToList()
            };
        }

    }
}
