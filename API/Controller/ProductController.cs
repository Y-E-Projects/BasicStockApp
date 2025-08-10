using BL.Abstract;
using DTO.Models;
using EL.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IPriceHistoryService _priceHistoryService;

        public ProductController(
            IProductService productService, 
            ICategoryService categoryService, 
            IPriceHistoryService priceHistoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _priceHistoryService = priceHistoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddModel.Product model)
        {
            if (_productService.CheckBarcodeExists(model.Barcode))
                return BadRequest(new
                {
                    message = "Bu barkodu içeren bir ürün zaten var.",
                });

            if (_categoryService.GetByKey(model.CategoryKey) == null)
                return BadRequest(new
                {
                    message = "Geçersiz kategori değeri.",
                });

            Product newProduct = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Barcode = model.Barcode,
                CategoryKey = model.CategoryKey,
            };

            _productService.Add(newProduct);
            return Ok(new
            {
                message = "Ürün başarıyla eklendi.",
                productKey = newProduct.Key,
            });
        }

        [HttpGet("GetWithKey")]
        public IActionResult GetWithKey(Guid key)
        {
            var product = _productService.GetByKey(key);
            if (product == null)
                return NotFound(new
                {
                    message = "Ürün bulunamadı.",
                });

            return Ok(product);
        }

        [HttpGet("GetWithBarcode")]
        public IActionResult GetWithBarcode(string barcode)
        {
            var product = _productService.GetWithBarcode(barcode);
            if (product == null)
                return NotFound(new
                {
                    message = "Ürün bulunamadı.",
                });

            return Ok(product);
        }

        [HttpPatch("UpdatePrice")]
        public IActionResult UpdatePrice([FromBody] AddModel.PriceHistory model)
        {
            var product = _productService.GetByKey(model.ProductKey);
            if (product == null)
                return NotFound(new
                {
                    message = "Ürün bulunamadı.",
                });

            decimal backPrice = product.Price;

            product.Price = model.NewPrice;
            _productService.Update(product);

            _priceHistoryService.Add(new PriceHistory
            {
                ProductKey = product.Key,
                NewPrice = product.Price,
                BackPrice = backPrice
            });

            return Ok(new
            {
                message = "Fiyat güncellendi.",
            });
        }
    }
}
