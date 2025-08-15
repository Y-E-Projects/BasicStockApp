using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using DTO.Models;
using EL.Concrete;

namespace DAL.EntityFramework
{
    public class EFStockHistoryDal : GenericRep<StockHistory>, IStockHistoryDal
    {
        public EFStockHistoryDal(MainDbContext context) : base(context)
        {
        }

        public void AddRange(List<StockHistory> stockHistories)
        {
            _context.StockHistories.AddRange(stockHistories);
            _context.SaveChanges();
        }

        public List<ListModel.StockHistory> GetList()
        {
            return _context.StockHistories.Select(sh => new ListModel.StockHistory
            {
                Key = sh.Key,
                Product = sh.Product.Name,
                Quantity = sh.Quantity,
                Type = sh.Type,
                CreatedAt = sh.CreatedAt
            }).ToList();
        }
    }
}