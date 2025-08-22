using BL.Generics;
using DTO.Models;
using EL.Concrete;

namespace BL.Abstract
{
    public interface IStockHistoryService : IGenericService<StockHistory>
    {
        void AddRange(List<StockHistory> stockHistories);
        List<ListModel.StockHistory> GetList();
        List<DetailModel.StockHistory> GetWithProduct(Guid productKey);
    }
}