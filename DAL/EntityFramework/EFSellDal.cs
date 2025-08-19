using DAL.Abstract;
using DAL.Context;
using DAL.Generics;
using DTO.Models;
using EL.Concrete;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFramework
{
    public class EFSellDal : GenericRep<Sell>, ISellDal
    {
        public EFSellDal(MainDbContext context) : base(context)
        {
        }

        public List<Sell> FullAttached()
        {
            var values = _context.Sells
                .Include(s => s.Items)
                    .ThenInclude(si => si.Product)
                .ToList();

            return values;
        }

        public DetailModel.Sell? GetDetailWithCode(string code)
        {
            return _context.Sells
                .Where(s => s.SellCode == code)
                .Select(s => new DetailModel.Sell
                {
                    Key = s.Key,
                    SellCode = s.SellCode,
                    SellDate = s.CreatedAt,
                    TotalAmount = s.TotalAmount,
                    NetAmount = s.NetAmount,

                    Items = s.Items.Select(item => new DetailModel.SellItemDetail
                    {
                        Key = item.Key,
                        ProductKey = item.ProductKey,
                        ProductName = item.Product.Name,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        LineTotal = item.LineTotal
                    }).ToList(),

                    ReturnSells = s.Items.SelectMany(i => i.ReturnHistories).OrderBy(x => x.CreatedAt).Select(rs => new DetailModel.ReturnSellWithSellDetail
                    {
                        Key = rs.Key,
                        Product = rs.Product.Name,
                        Quantity = rs.Quantity,
                        UnitPrice = rs.UnitPrice,
                        Reason = rs.Reason,
                    }).ToList()
                })
                .FirstOrDefault();
        }
    }
}