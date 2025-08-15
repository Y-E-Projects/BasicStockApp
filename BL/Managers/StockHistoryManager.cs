using BL.Abstract;
using DAL.Abstract;
using DTO.Models;
using EL.Concrete;

namespace BL.Managers
{
    public class StockHistoryManager : IStockHistoryService
    {
        private readonly IStockHistoryDal _stockHistoryDal;

        public StockHistoryManager(IStockHistoryDal stockHistoryDal)
        {
            _stockHistoryDal = stockHistoryDal;
        }

        public void Add(StockHistory entity) => _stockHistoryDal.Create(entity);

        public void Delete(StockHistory entity) => _stockHistoryDal.Remove(entity);

        public List<StockHistory> GetAll() => _stockHistoryDal.GetAll();

        public StockHistory GetByKey(Guid key) => _stockHistoryDal.GetByKey(key);

        public void Update(StockHistory entity) => _stockHistoryDal.Update(entity);

        public void AddRange(List<StockHistory> stockHistories) => _stockHistoryDal.AddRange(stockHistories);

        public List<ListModel.StockHistory> GetList() => _stockHistoryDal.GetList();
    }
}