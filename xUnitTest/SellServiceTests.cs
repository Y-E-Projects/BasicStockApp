using System.Linq.Expressions;
using BL.Abstract;
using BL.Managers;
using DAL.Abstract;
using DTO.Models;
using EL.Concrete;
using Moq;

namespace xUnitTest
{
    public class SellManagerTests
    {
        private readonly Mock<ISellDal> _sellDalMock;
        private readonly ISellService _sellService;

        public SellManagerTests()
        {
            _sellDalMock = new Mock<ISellDal>();

            var sells = new List<Sell>
            {
                new Sell
                {
                    Key = Guid.NewGuid(),
                    SellCode = "S001",
                    TotalAmount = 100,
                    NetAmount = 90,
                    Items = new List<SellItem> { new SellItem { Key = Guid.NewGuid(), ProductKey = Guid.NewGuid(), Quantity = 1, UnitPrice = 100, LineTotal = 100 } },
                    CreatedAt = new DateTime(2025,8,20)
                },
                new Sell
                {
                    Key = Guid.NewGuid(),
                    SellCode = "S002",
                    TotalAmount = 200,
                    NetAmount = 180,
                    Items = new List<SellItem> { new SellItem { Key = Guid.NewGuid(), ProductKey = Guid.NewGuid(), Quantity = 2, UnitPrice = 100, LineTotal = 200 } },
                    CreatedAt = new DateTime(2025,8,20)
                }
            };

            _sellDalMock.Setup(x => x.GetAll(It.IsAny<Expression<Func<Sell, bool>>>()))
                        .Returns<Expression<Func<Sell, bool>>>(expr => sells.Where(expr.Compile()).ToList());

            _sellDalMock.Setup(x => x.FullAttached()).Returns(sells);

            _sellDalMock.Setup(x => x.GetByKey(It.IsAny<Guid>()))
                        .Returns<Guid>(key => sells.FirstOrDefault(s => s.Key == key));

            _sellDalMock.Setup(x => x.GetDetailWithCode(It.IsAny<string>()))
                        .Returns<string>(code => sells
                            .Where(s => s.SellCode == code)
                            .Select(s => new DetailModel.Sell
                            {
                                Key = s.Key,
                                SellCode = s.SellCode,
                                SellDate = s.CreatedAt,
                                TotalAmount = s.TotalAmount,
                                NetAmount = s.NetAmount,
                                Items = s.Items.Select(i => new DetailModel.SellItemDetail
                                {
                                    Key = i.Key,
                                    ProductKey = i.ProductKey,
                                    ProductName = "Product",
                                    UnitPrice = i.UnitPrice,
                                    Quantity = i.Quantity,
                                    LineTotal = i.LineTotal
                                }).ToList(),
                                ReturnSells = new List<DetailModel.ReturnSellWithSellDetail>()
                            }).FirstOrDefault());

            _sellDalMock.Setup(x => x.GetList())
                        .Returns(() => sells.Select(s => new ListModel.Sell
                        {
                            SellCode = s.SellCode,
                            TotalAmount = s.TotalAmount,
                            NetAmount = s.NetAmount,
                            ProductCount = s.Items.Count,
                            Date = s.CreatedAt
                        }).ToList());

            _sellService = new SellManager(_sellDalMock.Object);
        }

        [Fact]
        public void GetList_ShouldReturnAllSells()
        {
            var list = _sellService.GetList();

            Assert.NotNull(list);
            Assert.Equal(2, list.Count);
            Assert.Contains(list, s => s.SellCode == "S001");
            Assert.Contains(list, s => s.SellCode == "S002");
        }

        [Fact]
        public void DashboardCount_ShouldReturnCorrectCount()
        {
            var count = _sellService.DashboardCount();
            Assert.Equal(2, count);
        }

        [Fact]
        public void DashboardAmount_ShouldReturnCorrectSum()
        {
            var total = _sellService.DashboardAmount();
            Assert.Equal(300, total);
        }

        [Fact]
        public void GetDetailWithCode_ShouldReturnCorrectSell()
        {
            var detail = _sellService.GetDetailWithCode("S001");
            Assert.NotNull(detail);
            Assert.Equal("S001", detail.SellCode);
            Assert.Single(detail.Items);
        }

        [Fact]
        public void GetDetailWithCode_ShouldReturnNull_ForInvalidCode()
        {
            var detail = _sellService.GetDetailWithCode("INVALID");
            Assert.Null(detail);
        }
    }
}
