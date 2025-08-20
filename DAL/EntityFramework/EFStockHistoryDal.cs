using AutoMapper;
using AutoMapper.QueryableExtensions;
using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using DTO.Models;
using EL.Concrete;

namespace DAL.EntityFramework
{
    public class EFStockHistoryDal : GenericRep<StockHistory>, IStockHistoryDal
    {
        private readonly IMapper _mapper;

        public EFStockHistoryDal(
            MainDbContext context, 
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void AddRange(List<StockHistory> stockHistories)
        {
            _context.StockHistories.AddRange(stockHistories);
            _context.SaveChanges();
        }

        public List<ListModel.StockHistory> GetList() => _context.StockHistories.ProjectTo<ListModel.StockHistory>(_mapper.ConfigurationProvider).ToList();
    }
}