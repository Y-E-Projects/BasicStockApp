using BL.Abstract;
using DTO.Models;
using EL.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IResourceLocalizer _localizer;
        private readonly ICategoryService _categoryService;

        public CategoryController(
            IResourceLocalizer localizer,
            ICategoryService categoryService)
        {
            _localizer = localizer;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var categories = _categoryService.GetList();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult AddCategory(AddModel.Category model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Name))
                return BadRequest(new
                {
                    message = _localizer.Localize("InvalidCategoryData")
                });

            var category = new Category
            {
                Name = model.Name,
                IsVisible = model.IsVisible,
            };

            _categoryService.Add(category);
            return Ok(new
            {
                message = _localizer.Localize("CategoryAdded"),
                categoryKey = category.Key,
            });
        }

        [HttpPatch("ChangeVisible")]
        public IActionResult ChangeVisible(Guid key)
        {
            var category = _categoryService.GetByKey(key);
            if (category == null)
                return NotFound(new
                {
                    message = _localizer.Localize("CategoryNotFound")
                });

            category.IsVisible = !category.IsVisible;
            _categoryService.Update(category);

            string visibilityStatus = category.IsVisible
                ? _localizer.Localize("CategoryVisible")
                : _localizer.Localize("CategoryInvisible");

            string messageTemplate = _localizer.Localize("CategoryVisibility");
            string message = string.Format(messageTemplate, visibilityStatus);

            return Ok(new
            {
                message
            });
        }
    }
}
