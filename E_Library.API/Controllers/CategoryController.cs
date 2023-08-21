/*using E_Library.Core.Services.Implementations;
using E_Library.Core.Services.Interfaces;
using E_Library.Data.DTOS.Request;
using E_Library.Data.DTOS.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<IEnumerable<GetCategory>>>> GetCategories(int pageNumber, int pageSize)
        {
            var response = await _categoryService.GetCategories(pageNumber, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<GetCategory>>> GetACategory(string id)
        {
            var response = await _categoryService.GetACategory(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<GetCategory>>> CreateBookAsync([FromBody] CreateCategory category)
        {
            var response = await _categoryService.CreateACategory(category);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<CreateCategory>>> UpdateBookAsync(string id, [FromBody] CreateCategory bookDto)
        {
            var response = await _categoryService.UpdateCategory(id, bookDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteBookAsync(string id)
        {
            var response = await _categoryService.DeleteCategory(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ResponseDto<IEnumerable<GetCategory>>>> SearchBooksAsync(
            string isbn, string? categoryId, string? subcategoryId, string? publisherId,
            string searchTerm, int pageNumber, int pageSize)
        {
            var response = await _categoryService.SearchCategory(isbn, categoryId, subcategoryId, publisherId, searchTerm, pageNumber, pageSize);
            return StatusCode(response.StatusCode, response);
        }
    }
}
*/