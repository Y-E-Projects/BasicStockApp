using BL.Abstract;
using DAL.Abstract;
using DTO.Models;
using EL.Concrete;

namespace BL.Managers
{
    public class SellItemManager : ISellItemService
    {
        private readonly ISellItemDal _sellItemDal;

        public SellItemManager(ISellItemDal sellItemDal)
        {
            _sellItemDal = sellItemDal;
        }

        public void Add(SellItem entity) => _sellItemDal.Create(entity);

        public void Delete(SellItem entity) => _sellItemDal.Remove(entity);

        public List<SellItem> GetAll() => _sellItemDal.GetAll();

        public SellItem GetByKey(Guid key) => _sellItemDal.GetByKey(key);

        public void Update(SellItem entity) => _sellItemDal.Update(entity);

        public DetailModel.BestSellingProduct? GetBestSellingProductKey(DateTime? start = null, DateTime? end = null)
        {
            var query = _sellItemDal.GetAll();

            if (start.HasValue)
                query = query.Where(x => x.CreatedAt.Date >= start.Value).ToList();

            if (end.HasValue)
                query = query.Where(x => x.CreatedAt.Date <= end.Value).ToList();
            var result = query
                .GroupBy(x => x.ProductKey)
                .Select(g => new DetailModel.BestSellingProduct
                {
                    ProductKey = g.Key,
                    TotalQuantity = g.Sum(x => x.Quantity)
                }).OrderByDescending(g => g.TotalQuantity).FirstOrDefault();

            if (result == null)
                return null;

            return result;
        }
    }
}