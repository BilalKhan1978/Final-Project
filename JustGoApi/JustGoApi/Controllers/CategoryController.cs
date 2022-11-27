using JustGoApi.Services;
using JustGoApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace JustGoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        public readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneCategory([FromRoute] int id)
        {
           var category = await _categoryService.GetOneCategory(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryRequest addCategoryRequest)
        {
           await _categoryService.AddCategory(addCategoryRequest);
           return Ok();
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, UpdateCategoryRequest updateCategoryRequest)
        {
            try
            {
                await _categoryService.UpdateCategory(id, updateCategoryRequest);
                return Ok();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Not found"))
                {
                    return NotFound();
                }
                throw new Exception(e.Message);
            }
        }    
    }
}
