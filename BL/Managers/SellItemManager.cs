using BL.Abstract;
using DAL.Abstract;
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
    }
}