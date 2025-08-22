using BL.Generics;
using DTO.Models;
using EL.Concrete;

namespace BL.Abstract
{
    public interface ISellItemService : IGenericService<SellItem>
    {
        DetailModel.BestSellingProduct? GetBestSellingProductKey(DateTime? start = null, DateTime? end = null);
    }
}