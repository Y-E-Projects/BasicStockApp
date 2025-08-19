using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using DTO.Models;
using EL.Concrete;

namespace DAL.EntityFramework
{
    public class EFSupplierDal : GenericRep<Supplier>, ISupplierDal
    {
        public EFSupplierDal(MainDbContext context) : base(context)
        {
        }

        public List<ListModel.Supplier> GetList()
        {
            return _context.Suppliers.Select(s => new ListModel.Supplier
            {
                Key = s.Key,
                Name = s.Name,
                Address = s.Address,
                Email = s.Email,
                ContactName = s.ContactName,
                Phone = s.Phone,
                Count = s.Products.Count,
            }).ToList();
        }
    }
}
