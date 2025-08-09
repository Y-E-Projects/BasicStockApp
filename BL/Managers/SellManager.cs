using BL.Abstract;
using DAL.Abstract;
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
    }
}