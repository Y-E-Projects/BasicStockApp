using BL.Generics;
using DTO.Models;
using EL.Concrete;

namespace BL.Abstract
{
    public interface ISupplierService : IGenericService<Supplier>
    {
        List<ListModel.Supplier> GetList();
    }
}