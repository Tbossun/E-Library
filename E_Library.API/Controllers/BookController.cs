using AutoMapper;
using E_Library.Core.Services.Implementations;
using E_Library.Core.Services.Interfaces;
using E_Library.Data.DTOS.Request;
using E_Library.Data.DTOS.Response;
using E_Library.Data.Models;
using E_Library.Data.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("All-Books")]
        public async Task<ActionResult<ResponseDto<IEnumerable<BookDto>>>> GetBooksAsync(int pageNumber, int pageSize)
        {
            var response = await _bookService.GetBooksAsync(pageNumber, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Book-By{id}")]
        public async Task<ActionResult<ResponseDto<BookDto>>> GetBookAsync(string id)
        {
            var response = await _bookService.GetBookAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Add-A-Book")]
        public async Task<ActionResult<ResponseDto<BookDto>>> CreateBookAsync([FromForm] AddBookDto bookDto)
        {
            var response = await _bookService.CreateBookAsync(bookDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("Update-Book{id}")]
        public async Task<ActionResult<ResponseDto<BookDto>>> UpdateBookAsync(string id, [FromForm] UpdateBookDto bookDto)
        {
            var response = await _bookService.UpdateBookAsync(id, bookDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteBookAsync(string id)
        {
            var response = await _bookService.DeleteBookAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ResponseDto<IEnumerable<BookDto>>>> SearchBooksAsync(string searchTerm, int pageNumber, int pageSize)
        {
            var response = await _bookService.SearchBooksAsync(searchTerm, pageNumber, pageSize);
            return StatusCode(response.StatusCode, response);                                             
        }


        [HttpGet("All-Categories")]
        public async Task<ActionResult<ResponseDto<IEnumerable<GetCategory>>>> GetCategories()
        {
            var response = await _bookService.GetCategories();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Category{id}")]
        public async Task<ActionResult<ResponseDto<GetCategory>>> GetACategory(string id)
        {
            var response = await _bookService.GetACategory(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Category")]
        public async Task<ActionResult<ResponseDto<GetCategory>>> CreateCategoryAsync([FromBody] CreateCategory category)
        {
            var response = await _bookService.CreateACategory(category);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("categories/{categoryId}")]
        public async Task<IActionResult> GetBooksByCategory(string categoryId, int? pageNumber, int? pageSize)
        {
            var response = await _bookService.GetBooksByCategoryAsync(categoryId, pageNumber, pageSize);
            return StatusCode(response.StatusCode, response);
        }

    }
}
