using BL.Generics;
using EL.Concrete;

namespace BL.Abstract
{
    public interface IReturnHistoryService : IGenericService<ReturnHistory>
    {
        List<ReturnHistory> GetBySellItemKey(Guid sellItemKey);
    }
}