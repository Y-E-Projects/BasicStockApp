using BL.Abstract;
using DTO.Models;
using EL.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        private readonly ISupplierService _supplierService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IPriceHistoryService priceHistoryService,
            IResourceLocalizer localizer,
            ISupplierService supplierService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _priceHistoryService = priceHistoryService;
            _localizer = localizer;
            _supplierService = supplierService;
        }

        [HttpPost]
        public IActionResult Create(AddModel.Product model)
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
                Quantity = model.Quantity,
                MinimumQuantity = model.MinimumQuantity,
            };

            if (model.SupplierKey != null && model.SupplierKey != Guid.Empty)
            {
                if (_supplierService.GetByKey(model.SupplierKey.Value) == null)
                    return BadRequest(new
                    {
                        message = _localizer.Localize("InvalidSupplier"),
                    });

                newProduct.SupplierKey = model.SupplierKey;
            }

            _productService.Add(newProduct);
            return Ok(new
            {
                message = _localizer.Localize("ProductAddedSuccessfully"),
                productKey = newProduct.Key,
            });
        }

        [HttpPost("CreateRange")]
        public async Task<IActionResult> CreateRange(List<AddModel.Product> models)
        {
            List<Product> newProducts = new();
            List<string> errorMessages = new();

            int i = 1;

            foreach (var model in models)
            {
                if (_productService.CheckBarcodeExists(model.Barcode))
                    errorMessages.Add($"{i}.: {_localizer.Localize("BarcodeAlreadyExists")}");

                if (_categoryService.GetByKey(model.CategoryKey) == null)
                    errorMessages.Add($"{i}.: {_localizer.Localize("InvalidCategory")}");

                if (_categoryService.GetByKey(model.CategoryKey).IsVisible == false)
                    errorMessages.Add($"{i}.: {_localizer.Localize("CategoryNotVisible")}");

                Product newProduct = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Barcode = model.Barcode,
                    CategoryKey = model.CategoryKey,
                    Quantity = model.Quantity,
                    MinimumQuantity = model.MinimumQuantity,
                };

                newProducts.Add(newProduct);

                if (model.SupplierKey != null && model.SupplierKey != Guid.Empty)
                {
                    if (_supplierService.GetByKey(model.SupplierKey.Value) == null)
                        errorMessages.Add($"{i}.: {_localizer.Localize("InvalidSupplier")}");
                    else
                        newProduct.SupplierKey = model.SupplierKey;
                }

                i++;
            }

            await _productService.CreateRange(newProducts);

            return Ok(new
            {
                message = _localizer.Localize("ProductAddedSuccessfully"),
            });
        }

        [HttpPost("CreateRangeNew")]
        public async Task<IActionResult> CreateRangeNew(List<AddModel.Product> models)
        {
            if (models == null || models.Count == 0)
                return BadRequest(new { message = _localizer.Localize("NoProductsProvided") });

            var errorMessages = new Dictionary<int, List<string>>();
            var newProducts = new List<Product>();

            var barcodes = models.Select(m => m.Barcode).ToList();
            var existingBarcodes = await _productService.GetAllBarcodes(barcodes);
            var categories = await _categoryService.GetByKeys(models.Select(m => m.CategoryKey));
            var suppliers = await _supplierService.GetByKeys(
                models.Where(m => m.SupplierKey.HasValue && m.SupplierKey != Guid.Empty)
                .Select(m => m.SupplierKey!.Value));

            int i = 1;
            foreach (var model in models)
            {
                var currentErrors = new List<string>();

                if (existingBarcodes.Contains(model.Barcode))
                    currentErrors.Add(_localizer.Localize("BarcodeAlreadyExists"));

                var category = categories.FirstOrDefault(c => c.Key == model.CategoryKey);
                if (category == null)
                    currentErrors.Add(_localizer.Localize("InvalidCategory"));
                else if (!category.IsVisible)
                    currentErrors.Add(_localizer.Localize("CategoryNotVisible"));

                Guid? supplierKey = null;
                if (model.SupplierKey.HasValue && model.SupplierKey != Guid.Empty)
                {
                    if (!suppliers.Any(s => s.Key == model.SupplierKey))
                        currentErrors.Add(_localizer.Localize("InvalidSupplier"));
                    else
                        supplierKey = model.SupplierKey;
                }

                if (currentErrors.Any())
                    errorMessages[i] = currentErrors;
                else
                    newProducts.Add(new Product
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Barcode = model.Barcode,
                        CategoryKey = model.CategoryKey,
                        Quantity = model.Quantity,
                        MinimumQuantity = model.MinimumQuantity,
                        SupplierKey = supplierKey
                    });

                i++;
            }

            if (errorMessages.Any())
                return BadRequest(new { errors = errorMessages });

            await _productService.CreateRange(newProducts);

            return Ok(new
            {
                message = _localizer.Localize("ProductAddedSuccessfully"),
                count = newProducts.Count
            });
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var products = _productService.GetList();
            return Ok(products);
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

        [HttpGet("GetProductsWithSupplier")]
        public IActionResult GetProductsWithSupplier(Guid key)
        {
            if (_supplierService.GetByKey(key) == null)
                return BadRequest(new
                {
                    message = _localizer.Localize("SupplierNotFound"),
                });

            var products = _productService.GetListWithCategory(key);
            return Ok(products);
        }

        [HttpGet("GetListWithLowStock")]
        public IActionResult GetListWithLowStock()
        {
            var products = _productService.GetListWithLowStock();
            return Ok(products);
        }

        [HttpGet("GetStock")]
        public IActionResult GetStock(Guid key)
        {
            var product = _productService.GetByKey(key);
            if (product == null)
                return NotFound(new
                {
                    message = _localizer.Localize("ProductNotFound"),
                });

            return Ok(product.Quantity);
        }

        [HttpGet("GetPrice")]
        public IActionResult GetPrice(Guid key)
        {
            var product = _productService.GetByKey(key);
            if (product == null)
                return NotFound(new
                {
                    message = _localizer.Localize("ProductNotFound"),
                });

            return Ok(product.Price);
        }

        [HttpPatch("ChangeVisible")]
        public IActionResult ChangeVisible(Guid key)
        {
            var product = _productService.GetByKey(key);
            if (product == null)
                return NotFound(new
                {
                    message = _localizer.Localize("ProductNotFound"),
                });

            product.IsVisible = !product.IsVisible;
            _productService.Update(product);

            string visibilityStatus = product.IsVisible
                ? _localizer.Localize("Visible")
                : _localizer.Localize("Invisible");

            string messageTemplate = _localizer.Localize("ProductVisibility");
            string message = string.Format(messageTemplate, visibilityStatus);

            return Ok(new
            {
                message
            });
        }

        [HttpPatch("UpdatePrice")]
        public IActionResult UpdatePrice(UpdateModel.ProductPrice model)
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

        [HttpPatch("UpdateName")]
        public IActionResult UpdateName(UpdateModel.ProductName model)
        {
            var product = _productService.GetByKey(model.ProductKey);
            if (product == null)
                return NotFound(new
                {
                    message = _localizer.Localize("ProductNotFound"),
                });

            decimal backPrice = product.Price;

            product.Name = model.Name;
            _productService.Update(product);

            return Ok(new
            {
                message = _localizer.Localize("ProductNameUpdatedSuccessfully"),
            });
        }

        [HttpPut("Update")]
        public IActionResult Update(UpdateModel.Product model)
        {
            var product = _productService.GetByKey(model.Key);
            if (product == null)
                return NotFound(new
                {
                    message = _localizer.Localize("ProductNotFound"),
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

            product.Barcode = model.Barcode;
            product.CategoryKey = model.CategoryKey;
            product.MinimumQuantity = model.MinimumQuantity;

            if (model.SupplierKey != null && model.SupplierKey != Guid.Empty)
            {
                if (_supplierService.GetByKey(model.SupplierKey.Value) == null)
                    return BadRequest(new
                    {
                        message = _localizer.Localize("InvalidSupplier"),
                    });

                product.SupplierKey = model.SupplierKey;
            }
            else
                product.SupplierKey = null;

            _productService.Update(product);
            return Ok(new
            {
                message = _localizer.Localize("ProductUpdatedSuccessfully"),
            });
        }
    }
}
