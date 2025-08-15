using BL.Generics;
using DTO.Models;
using EL.Concrete;

namespace BL.Abstract
{
    public interface ISellService : IGenericService<Sell>
    {
        DetailModel.Sell? GetByCode(string code);
        int DashboardCount(DateTime? start = null, DateTime? end = null);
        decimal DashboardAmount(DateTime? start = null, DateTime? end = null);
        List<ListModel.Sell> GetList();
    }
}