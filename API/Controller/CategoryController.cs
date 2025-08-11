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
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var categories = _categoryService.GetList();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] AddModel.Category model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Name))
                return BadRequest(new
                {
                    message = "Invalid category data.",
                });

            var category = new Category
            {
                Name = model.Name,
                IsVisible = model.IsVisible,
            };

            _categoryService.Add(category);
            return Ok(new
            {
                message = "Kategori başarılı bir şekilde eklenmiştir.",
                categoryKey = category.Key,
            });
        }

        [HttpPatch("ChangeVisible")]
        public IActionResult ChangeVisible([FromBody] Guid key)
        {
            var category = _categoryService.GetByKey(key);
            if (category == null)
                return NotFound(new
                {
                    message = "Kategori bulunamadı.",
                });

            category.IsVisible = !category.IsVisible;
            _categoryService.Update(category);

            string visibilityStatus = category.IsVisible ? "görünür" : "görünmez";

            return Ok(new
            {
                message = $"Kategori görünürlüğü başarıyla {visibilityStatus} olarak değiştirildi.",
            });
        }
    }
}
