using BL.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IResourceLocalizer _localizer;
        private readonly ISellService _sellService;
        private readonly IProductService _productService;
        private readonly ISellItemService _sellItemService;
        private readonly IPriceHistoryService _priceHistoryService;

        public DashboardController(
            ISellService sellService,
            IProductService productService,
            ISellItemService sellItemService,
            IPriceHistoryService priceHistoryService,
            IResourceLocalizer localizer)
        {
            _sellService = sellService;
            _productService = productService;
            _sellItemService = sellItemService;
            _priceHistoryService = priceHistoryService;
            _localizer = localizer;
        }

        [HttpGet("GetBasicDashboardData")]
        public IActionResult GetBasicDashboardData(DateTime? start = null, DateTime? end = null)
        {
            start ??= DateTime.UtcNow.AddHours(3).Date;
            end ??= DateTime.UtcNow.AddHours(3).Date;

            if (end < start)
            {
                return BadRequest(new
                {
                    message = _localizer.Localize("EndDateMustBeAfterOrEqualStartDate"),
                });
            }

            var totalSellsCount = _sellService.DashboardCount(start, end);
            var totalSellsAmount = _sellService.DashboardAmount(start, end);

            var bestSellingProductKey = _sellItemService.GetBestSellingProductKey(start, end);

            object bestSellProduct;
            if (bestSellingProductKey != null)
            {
                var product = _productService.GetByKey(bestSellingProductKey.ProductKey);

                bestSellProduct = new
                {
                    Key = bestSellingProductKey.ProductKey,
                    product.Name,
                    bestSellingProductKey.TotalQuantity,
                };
            }
            else
                bestSellProduct = _localizer.Localize("ProductNotFound");

            var dashboardData = new
            {
                TotalSellsCount = totalSellsCount,
                TotalSellsAmount = totalSellsAmount,
                BestSellProduct = bestSellProduct,
            };

            return Ok(dashboardData);
        }

        [HttpGet("GetTopProducts")]
        public IActionResult GetTopProducts(int count = 5)
        {
            if (count <= 0)
                return BadRequest(new
                {
                    message = _localizer.Localize("CountMustBeGreaterThanZero"),
                });

            var topProducts = _productService.GetTopProducts(count);
            return Ok(topProducts);
        }
    }
}
