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
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAll();
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
                Name = model.Name
            };

            _categoryService.Add(category);
            return CreatedAtAction(nameof(GetAllCategories), new { id = category.Key }, category);
        }
    }
}
