using BL.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceHistoryController : ControllerBase
    {
        private readonly IResourceLocalizer _localizer;
        private readonly IPriceHistoryService _priceHistoryService;
        private readonly IProductService _productService;

        public PriceHistoryController(
            IPriceHistoryService priceHistoryService,
            IProductService productService,
            IResourceLocalizer localizer)
        {
            _priceHistoryService = priceHistoryService;
            _productService = productService;
            _localizer = localizer;
        }

        [HttpGet("GetWithProduct")]
        public IActionResult GetWithProduct(Guid key)
        {
            if (_productService.GetByKey(key) == null)
                return BadRequest(new
                {
                    message = _localizer.Localize("ProductNotFound")
                });

            var value = _priceHistoryService.GetWithProduct(key);
            if (value == null)
                return BadRequest(new
                {
                    message = _localizer.Localize("PriceHistoryNotFound")
                });

            return Ok(value);
        }
    }
}
