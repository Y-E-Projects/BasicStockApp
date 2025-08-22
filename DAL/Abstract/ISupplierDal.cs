using DAL.Generics;
using DTO.Models;
using EL.Concrete;

namespace DAL.Abstract
{
    public interface ISupplierDal : IGenericDal<Supplier>
    {
        List<ListModel.Supplier> GetList();
    }
}