using DAL.Generics;
using DTO.Models;
using EL.Concrete;

namespace DAL.Abstract
{
    public interface IStockHistoryDal : IGenericDal<StockHistory>
    {
        void AddRange(List<StockHistory> stockHistories);
        List<ListModel.StockHistory> GetList();
    }
}