using BL.Abstract;
using DTO.Models;
using EL.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IResourceLocalizer _localizer;
        private readonly IStockHistoryService _stockHistoryService;
        private readonly IProductService _productService;

        public StockController(
            IResourceLocalizer localizer, 
            IStockHistoryService stockHistoryService, 
            IProductService productService)
        {
            _localizer = localizer;
            _stockHistoryService = stockHistoryService;
            _productService = productService;
        }

        [HttpPost]
        public IActionResult Add(List<AddModel.Stock> models)
        {
            if (models == null || models.Count == 0)
                return BadRequest(new
                {
                    message = _localizer.Localize("EmptyStockList")
                });

            var duplicateKey = models
                .GroupBy(m => m.ProductKey)
                .FirstOrDefault(g => g.Count() > 1);

            if (duplicateKey != null)
            {
                return BadRequest(new
                {
                    message = _localizer.Localize("DuplicateProductNotAllowed")
                });
            }

            var productKeys = models.Select(m => m.ProductKey).ToList();
            var products = _productService.GetByKeys(productKeys) 
                             .ToDictionary(p => p.Key, p => p);

            var stockHistories = new List<StockHistory>();
            var messages = new List<string>();

            int index = 1;
            foreach (var model in models)
            {
                if (!products.TryGetValue(model.ProductKey, out var product))
                    return NotFound(new
                    {
                        message = _localizer.Localize("ProductNotFound")
                    });

                if (!Enum.IsDefined(typeof(StockHistoryType), model.Type))
                    return BadRequest(new
                    {
                        message = $"{index++}. {product.Name}: {_localizer.Localize("InvalidStockType")}"
                    });

                if (model.Quantity <= 0)
                    return BadRequest(new
                    {
                        message = $"{index++}. {product.Name}: {_localizer.Localize("InvalidQuantity")}"
                    });

                if (model.Type == StockHistoryType.Decrease && product.Quantity < model.Quantity)
                {
                    var actionName = _localizer.Localize(
                        model.Type == StockHistoryType.Adding ? "Adding" : "Reducing"
                    );

                    return BadRequest(new
                    {
                        message = string.Format(
                            _localizer.Localize("InsufficientStock"),
                            product.Name,
                            actionName
                        )
                    });
                }

                stockHistories.Add(new StockHistory
                {
                    Type = model.Type,
                    ProductKey = model.ProductKey,
                    Note = model.Note,
                    Quantity = model.Quantity
                });

                var actionWord = model.Type == StockHistoryType.Adding
                    ? _localizer.Localize("Added")
                    : _localizer.Localize("Reduced");

                messages.Add($"{index++}. {product.Name}: " +
                             string.Format(_localizer.Localize("StockUpdateMessage"), actionWord));
            }

            _stockHistoryService.AddRange(stockHistories);

            _productService.UpdateQuantities(models);

            return Ok(new
            {
                messages
            });
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_stockHistoryService.GetList());
        }
    }
}
