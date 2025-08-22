using BL.Abstract;
using DAL.Abstract;
using EL.Concrete;

namespace BL.Managers
{
    public class ReturnHistoryManager : IReturnHistoryService
    {
        private readonly IReturnHistoryDal _returnHistoryDal;

        public ReturnHistoryManager(IReturnHistoryDal returnHistoryDal)
        {
            _returnHistoryDal = returnHistoryDal;
        }

        public void Add(ReturnHistory entity) => _returnHistoryDal.Create(entity);

        public void Delete(ReturnHistory entity) => _returnHistoryDal.Remove(entity);

        public List<ReturnHistory> GetAll() => _returnHistoryDal.GetAll();

        public ReturnHistory GetByKey(Guid key) => _returnHistoryDal.GetByKey(key);

        public void Update(ReturnHistory entity) => _returnHistoryDal.Update(entity);

        public List<ReturnHistory> GetBySellItemKey(Guid sellItemKey) => _returnHistoryDal.GetAll(x => x.SellItemKey == sellItemKey).ToList();
    }
}