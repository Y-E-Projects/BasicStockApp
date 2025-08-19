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

        public DetailModel.Sell? GetDetailWithCode(string code) => _sellDal.GetDetailWithCode(code);

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

        public List<ListModel.Sell> GetList()
        {
            return _sellDal.FullAttached().Select(x => new ListModel.Sell
            {
                SellCode = x.SellCode,
                TotalAmount = x.TotalAmount,
                NetAmount = x.NetAmount,
                ProductCount = x.Items.Count,
                Date = x.CreatedAt,
            }).ToList();
        }
    }
}
