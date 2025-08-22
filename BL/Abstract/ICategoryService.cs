using BL.Generics;
using DTO.Models;
using EL.Concrete;

namespace BL.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        List<ListModel.Category> GetList();
    }
}