using DAL.Generics;
using DTO.Models;
using EL.Concrete;

namespace DAL.Abstract
{
    public interface ISellDal : IGenericDal<Sell>
    {
        List<Sell> FullAttached();
        DetailModel.Sell? GetDetailWithCode(string code);
        List<ListModel.Sell> GetList();
    }
}