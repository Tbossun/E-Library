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

        [HttpGet]
        public async Task<ActionResult<ResponseDto<IEnumerable<BookDto>>>> GetBooksAsync(int pageNumber, int pageSize)
        {
            var response = await _bookService.GetBooksAsync(pageNumber, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<BookDto>>> GetBookAsync(string id)
        {
            var response = await _bookService.GetBookAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<BookDto>>> CreateBookAsync([FromBody] AddBookDto bookDto)
        {
            var response = await _bookService.CreateBookAsync(bookDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<BookDto>>> UpdateBookAsync(string id, [FromBody] UpdateBookDto bookDto)
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
        public async Task<ActionResult<ResponseDto<IEnumerable<BookDto>>>> SearchBooksAsync(
            string isbn, string? categoryId, string? subcategoryId, string? publisherId,
            string searchTerm, int pageNumber, int pageSize)
        {
            var response = await _bookService.SearchBooksAsync(isbn, categoryId, subcategoryId, publisherId, searchTerm, pageNumber, pageSize);
            return StatusCode(response.StatusCode, response);
        }
    }
}
