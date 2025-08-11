using BL.Generics;
using DTO.Models;
using EL.Concrete;

namespace BL.Abstract
{
    public interface ISellService : IGenericService<Sell>
    {
        DetailModel.Sell? GetByCode(string code);
    }
}