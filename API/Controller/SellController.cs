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
        private readonly IProductService _productService;
        private readonly ISellService _sellService;
        private readonly ISellItemService _sellItemService;

        public SellController(
            IProductService productService,
            ISellService sellService,
            ISellItemService sellItemService)
        {
            _productService = productService;
            _sellService = sellService;
            _sellItemService = sellItemService;
        }

        [HttpPost]
        public IActionResult CreateSellAsync([FromBody] AddModel.CreateSellRequest request)
        {
            if (request == null || request.Items == null || !request.Items.Any())
                return BadRequest("Satış için ürün listesi boş olamaz.");

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
                        return NotFound($"Ürün bulunamadı: {item.ProductKey}");

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
                    message = "Satış başarıyla oluşturuldu.",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Satış oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("GetWithCode")]
        public IActionResult GetSellByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Satış kodu boş olamaz.");
            
            var value = _sellService.GetByCode(code);

            if (value == null)
                return NotFound(new
                {
                    message = "Satış bulunamadı."
                });

            return Ok(value);
        }
    }
}
