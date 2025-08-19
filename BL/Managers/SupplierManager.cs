using BL.Abstract;
using DAL.Abstract;
using DTO.Models;
using EL.Concrete;

namespace BL.Managers
{
    public class SupplierManager : ISupplierService
    {
        private readonly ISupplierDal _supplierDal;

        public SupplierManager(ISupplierDal supplierDal)
        {
            _supplierDal = supplierDal;
        }

        public void Add(Supplier entity) => _supplierDal.Create(entity);

        public void Delete(Supplier entity) => _supplierDal.Remove(entity);

        public List<Supplier> GetAll() => _supplierDal.GetAll();

        public Supplier GetByKey(Guid key) => _supplierDal.GetByKey(key);

        public void Update(Supplier entity) => _supplierDal.Update(entity);

        public List<ListModel.Supplier> GetList() => _supplierDal.GetList();
    }
}