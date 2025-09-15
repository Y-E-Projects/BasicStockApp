using AutoMapper;
using AutoMapper.QueryableExtensions;
using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using DTO.Models;
using EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFramework
{
    public class EFSupplierDal : GenericRep<Supplier>, ISupplierDal
    {
        private readonly IMapper _mapper;

        public EFSupplierDal(
            MainDbContext context, 
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public List<ListModel.Supplier> GetList() => _context.Suppliers.ProjectTo<ListModel.Supplier>(_mapper.ConfigurationProvider).ToList();

        public async Task<List<Supplier>> GetByKeys(IEnumerable<Guid> keys)
        {
            return await _context.Suppliers
                .Where(s => keys.Contains(s.Key))
                .ToListAsync();
        }
    }
}
