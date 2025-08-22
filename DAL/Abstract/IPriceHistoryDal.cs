using DAL.Generics;
using EL.Concrete;

namespace DAL.Abstract
{
    public interface IPriceHistoryDal : IGenericDal<PriceHistory>
    {
        List<PriceHistory> FullAttached();
    }
}