using AutoMapper;
using E_Library.Core.Repositories.IRepositories;
using E_Library.Data.DTOS.Request;
using E_Library.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
       private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult> GetBooks(int pageNumber = 1, int pageSize = 10)
        {
            var totalBooks = await _unitOfWork.BookRepo.CountAsync();
            var books = await _unitOfWork.BookRepo.GetPageAsync(pageNumber, pageSize);

            return Ok(new { TotalCount = totalBooks, Books = books });
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBook(string id)
        {
            var book =  _unitOfWork.BookRepo.Get(i => i.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult> CreateBook(AddBookDto book)
        {
            var newbook = _mapper.Map<Book>(book);
             _unitOfWork.BookRepo.Add(newbook);
             _unitOfWork.Save();

            return CreatedAtAction(nameof(GetBook), new { id = book.Title }, book);
        }
    }
}
