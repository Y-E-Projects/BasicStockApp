using BL.Generics;
using DTO.Models;
using EL.Concrete;

namespace BL.Abstract
{
    public interface IPriceHistoryService : IGenericService<PriceHistory>
    {
        DetailModel.PriceHistoryWithProduct? GetWithProduct(Guid productKey);
    }
}