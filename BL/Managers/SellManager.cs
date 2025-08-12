using BL.Abstract;
using DAL.Abstract;
using DTO.Models;
using EL.Concrete;

namespace BL.Managers
{
    public class SellManager : ISellService
    {
        private readonly ISellDal _sellDal;

        public SellManager(ISellDal sellDal)
        {
            _sellDal = sellDal;
        }

        public void Add(Sell entity) => _sellDal.Create(entity);

        public void Delete(Sell entity) => _sellDal.Remove(entity);

        public List<Sell> GetAll() => _sellDal.GetAll();

        public Sell GetByKey(Guid key) => _sellDal.GetByKey(key);

        public void Update(Sell entity) => _sellDal.Update(entity);

        public DetailModel.Sell? GetByCode(string code)
        {
            var value = _sellDal.FullAttached().Where(x => x.SellCode == code).First();

            if (value == null)
                return null;

            return new DetailModel.Sell
            {
                Key = value.Key,
                SellCode = value.SellCode,
                SellDate = value.CreatedAt,
                TotalAmount = value.TotalAmount,
                NetAmount = value.NetAmount,
                Items = value.Items.Select(item => new DetailModel.SellItemDetail
                {
                    Key = item.Key,
                    ProductKey = item.ProductKey,
                    ProductName = item.Product.Name,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    LineTotal = item.LineTotal
                }).ToList(),
            };
        }

        public int DashboardCount(DateTime? start = null, DateTime? end = null)
        {
            return _sellDal.GetAll(x =>
                (start == null || x.CreatedAt >= start) &&
                (end == null || x.CreatedAt <= end)).Count;
        }

        public decimal DashboardAmount(DateTime? start = null, DateTime? end = null)
        {
            return _sellDal.GetAll(x =>
                (start == null || x.CreatedAt >= start) &&
                (end == null || x.CreatedAt <= end)).Sum(x => x.TotalAmount);
        }
    }
}
