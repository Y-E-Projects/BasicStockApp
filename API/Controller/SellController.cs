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

        public SellController(
            IProductService productService,
            ISellService sellService,
            ISellItemService sellItemService,
            IResourceLocalizer localizer)
        {
            _productService = productService;
            _sellService = sellService;
            _sellItemService = sellItemService;
            _localizer = localizer;
        }

        [HttpPost]
        public IActionResult CreateSellAsync([FromBody] AddModel.CreateSellRequest request)
        {
            if (request == null || request.Items == null || !request.Items.Any())
                return BadRequest(new
                {
                    message = _localizer.Localize("EmptySellItemList"),
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
