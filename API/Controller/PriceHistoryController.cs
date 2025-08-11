using BL.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceHistoryController : ControllerBase
    {
        private readonly IPriceHistoryService _priceHistoryService;
        private readonly IProductService _productService;

        public PriceHistoryController(
            IPriceHistoryService priceHistoryService, 
            IProductService productService)
        {
            _priceHistoryService = priceHistoryService;
            _productService = productService;
        }

        [HttpGet("GetWithProduct")]
        public IActionResult GetWithProduct(Guid key)
        {
            if (_productService.GetByKey(key) == null)
                return BadRequest(new
                {
                    message = "İlgili ürün bulunamadı."
                });

            var value = _priceHistoryService.GetWithProduct(key);
            if (value == null)
                return BadRequest(new
                {
                    message = "İlgili ürün için fiyat geçmişi bulunamadı."
                });

            return Ok(value);
        }
    }
}
