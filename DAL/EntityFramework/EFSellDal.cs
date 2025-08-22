using AutoMapper;
using AutoMapper.QueryableExtensions;
using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using DTO.Models;
using EL.Concrete;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFramework
{
    public class EFSellDal : GenericRep<Sell>, ISellDal
    {
        private readonly IMapper _mapper;

        public EFSellDal(
            MainDbContext context, 
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public List<Sell> FullAttached()
        {
            var values = _context.Sells
                .Include(s => s.Items)
                    .ThenInclude(si => si.Product)
                .ToList();

            return values;
        }

        public List<ListModel.Sell> GetList()
        {
            return _context.Sells
                .ProjectTo<ListModel.Sell>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public DetailModel.Sell? GetDetailWithCode(string code)
        {
            return _context.Sells
                .Where(s => s.SellCode == code)
                .ProjectTo<DetailModel.Sell>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }
    }
}
