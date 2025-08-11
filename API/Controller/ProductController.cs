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
        private readonly IResourceLocalizer _localizer;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IPriceHistoryService _priceHistoryService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IPriceHistoryService priceHistoryService,
            IResourceLocalizer localizer)
        {
            _productService = productService;
            _categoryService = categoryService;
            _priceHistoryService = priceHistoryService;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var products = _productService.GetList();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddModel.Product model)
        {
            if (_productService.CheckBarcodeExists(model.Barcode))
                return BadRequest(new
                {
                    message = _localizer.Localize("BarcodeAlreadyExists"),
                });

            if (_categoryService.GetByKey(model.CategoryKey) == null)
                return BadRequest(new
                {
                    message = _localizer.Localize("InvalidCategory"),
                });

            if (_categoryService.GetByKey(model.CategoryKey).IsVisible == false)
                return BadRequest(new
                {
                    message = _localizer.Localize("CategoryNotVisible"),
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
                message = _localizer.Localize("ProductAddedSuccessfully"),
                productKey = newProduct.Key,
            });
        }

        [HttpGet("GetDetail")]
        public IActionResult GetDetail(Guid key)
        {
            var product = _productService.GetDetailWithKey(key);
            if (product == null)
                return NotFound(new
                {
                    message = _localizer.Localize("ProductNotFound"),
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
                    message = _localizer.Localize("ProductNotFound"),
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
                    message = _localizer.Localize("ProductNotFound"),
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
                message = _localizer.Localize("PriceUpdatedSuccessfully"),
            });
        }

        [HttpGet("GetListWithCategory")]
        public IActionResult GetProductsWithCategory(Guid key)
        {
            if (_categoryService.GetByKey(key) == null)
                return BadRequest(new
                {
                    message = _localizer.Localize("CategoryNotFound"),
                });

            var products = _productService.GetListWithCategory(key);
            return Ok(products);
        }
    }
}
