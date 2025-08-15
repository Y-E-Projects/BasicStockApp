using BL.Abstract;
using DTO.Models;
using EL.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellController : ControllerBase
    {
        private readonly IResourceLocalizer _localizer;
        private readonly IProductService _productService;
        private readonly ISellService _sellService;
        private readonly ISellItemService _sellItemService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SellController(
            IProductService productService,
            ISellService sellService,
            ISellItemService sellItemService,
            IResourceLocalizer localizer,
            IServiceScopeFactory serviceScopeFactory)
        {
            _productService = productService;
            _sellService = sellService;
            _sellItemService = sellItemService;
            _localizer = localizer;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpPost]
        public IActionResult Create(AddModel.CreateSellRequest request)
        {
            if (request == null || request.Items == null || !request.Items.Any())
                return BadRequest(new
                {
                    message = _localizer.Localize("EmptySellItemList"),
                });

            var duplicateProduct = request.Items
                .GroupBy(i => i.ProductKey)
                .FirstOrDefault(g => g.Count() > 1);

            if (duplicateProduct != null)
                return BadRequest(new
                {
                    message = _localizer.Localize("DuplicateProductNotAllowed"),
                });

            try
            {
                var sell = new Sell
                {
                    SellCode = $"S{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8]}",
                    Items = new List<SellItem>()
                };

                decimal totalAmount = 0;

                foreach (var item in request.Items)
                {
                    var product = _productService.GetByKey(item.ProductKey);
                    if (product == null)
                        return NotFound(new
                        {
                            message = _localizer.Localize("ProductNotFound")
                        });

                    if (product.Quantity <= 0)
                        return BadRequest(new
                        {
                            message = _localizer.Localize("ProductOutOfStock"),
                        });

                    if (item.Quantity <= 0)
                        return BadRequest(new
                        {
                            message = _localizer.Localize("InvalidQuantity"),
                        });

                    if (product.Quantity < item.Quantity)
                        return BadRequest(new
                        {
                            message = _localizer.Localize("InsufficientProductQuantity"),
                        });

                    var lineTotal = product.Price * item.Quantity;
                    totalAmount += lineTotal;

                    sell.Items.Add(new SellItem
                    {
                        ProductKey = product.Key,
                        UnitPrice = product.Price,
                        Quantity = item.Quantity,
                        LineTotal = lineTotal
                    });
                }

                sell.TotalAmount = totalAmount;
                sell.NetAmount = totalAmount;

                _sellService.Add(sell);

                Task.Run(() =>
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

                    foreach (var item in sell.Items)
                    {
                        productService.DecreaseQuantity(item.ProductKey, item.Quantity);
                    }
                });

                return Ok(new
                {
                    message = _localizer.Localize("SellCreatedSuccessfully"),
                });
            }
            catch (Exception ex)
            {
                string messageTemplate = _localizer.Localize("SellCreationError");
                string message = string.Format(messageTemplate, ex.Message);
                return BadRequest(new
                {
                    message,
                });
            }
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var sells = _sellService.GetList();
            return Ok(sells);
        }

        [HttpGet("GetWithCode")]
        public IActionResult GetSellByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest(new
                {
                    message = _localizer.Localize("SellCodeCannotBeEmpty")
                });

            var value = _sellService.GetByCode(code);

            if (value == null)
                return NotFound(new
                {
                    message = _localizer.Localize("SellNotFound")
                });

            return Ok(value);
        }
    }
}
