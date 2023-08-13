using AutoMapper;
using E_Library.Core.Services.Interfaces;
using E_Library.Data.DTOS.Request;
using E_Library.Data.DTOS.Response;
using E_Library.Data.Models;
using E_Library.Data.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Core.Services.Implementations
{
    public class BookServices : IBookService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<BookDto>>> GetBooksAsync(int? pageNumber, int? pageSize)
        {
            int defaultPageSize = 10;
            int defaultPageNumber = 1;

            if (!pageNumber.HasValue || pageNumber <= 0)
            {
                pageNumber = defaultPageNumber;
            }

            if (!pageSize.HasValue || pageSize <= 0)
            {
                pageSize = defaultPageSize;
            }

            try
            {
                var books = await _unitOfWork.BookRepo.GetPageAsync(pageNumber.Value, pageSize.Value);
                var booksDto = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books);

                var response = new ResponseDto<IEnumerable<BookDto>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Books retrieved successfully.",
                    Result = booksDto
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<IEnumerable<BookDto>>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return response;
            }
        }



        public async Task<ResponseDto<bool>> DeleteBookAsync(string id)
        {
            try
            {
                var existingBook = _unitOfWork.BookRepo.Get(b => b.Id == id);
                if (existingBook == null)
                {
                    var notFoundResponse = new ResponseDto<bool>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        DisplayMessage = "Book not found.",
                        Result = false
                    };

                    return notFoundResponse;
                }

                _unitOfWork.BookRepo.Remove(existingBook);
                _unitOfWork.Save();

                var response = new ResponseDto<bool>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Book deleted successfully.",
                    Result = true
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<bool>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = false
                };

                return response;
            }
        }

        public async Task<ResponseDto<BookDto>> GetBookAsync(string id)
        {
            try
            {
                var book =  _unitOfWork.BookRepo.Get(b => b.Id == id);
                if (book == null)
                {
                    var notFoundResponse = new ResponseDto<BookDto>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        DisplayMessage = "Book not found.",
                        Result = null
                    };

                    return notFoundResponse;
                }

                var bookDto = _mapper.Map<BookDto>(book);

                var response = new ResponseDto<BookDto>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Book retrieved successfully.",
                    Result = bookDto
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<BookDto>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return response;
            }
        }



        public async Task<ResponseDto<IEnumerable<BookDto>>> SearchBooksAsync(
    string isbn, string? categoryId, string? subcategoryId, string? publisherId,
    string searchTerm, int pageNumber, int pageSize)
        {
            try
            {
                Expression<Func<Book, bool>> filter = b =>
                    (string.IsNullOrEmpty(isbn) || b.ISBN.Contains(isbn)) &&
                    (string.IsNullOrEmpty(categoryId) || b.CategoryId == categoryId) &&
                    (string.IsNullOrEmpty(publisherId) || b.PublisherId == publisherId) &&
                    (string.IsNullOrEmpty(searchTerm) || b.Title.Contains(searchTerm));

                var books = await _unitOfWork.BookRepo.SearchAsync(filter);
                var booksDto = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books);

                var response = new ResponseDto<IEnumerable<BookDto>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Books retrieved successfully.",
                    Result = booksDto
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<IEnumerable<BookDto>>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return response;
            }
        }


        public async Task<ResponseDto<BookDto>> UpdateBookAsync(string id, UpdateBookDto bookDto)
        {
            try
            {
                var existingBook =  _unitOfWork.BookRepo.Get(b => b.Id == id);
                if (existingBook == null)
                {
                    var notFoundResponse = new ResponseDto<BookDto>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        DisplayMessage = "Book not found.",
                        Result = null
                    };

                    return notFoundResponse;
                }

                _mapper.Map(bookDto, existingBook);
                _unitOfWork.BookRepo.Update(existingBook);
                _unitOfWork.Save();

                var updatedBookDto = _mapper.Map<BookDto>(existingBook);

                var response = new ResponseDto<BookDto>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Book updated successfully.",
                    Result = updatedBookDto
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<BookDto>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return response;
            }
        }

        public async Task<ResponseDto<BookDto>> CreateBookAsync(AddBookDto book)
        {
            try
            {
                var newBook = _mapper.Map<Book>(book);
                _unitOfWork.BookRepo.Add(newBook);
                _unitOfWork.Save();

                var createdBookDto = _mapper.Map<BookDto>(newBook);

                var response = new ResponseDto<BookDto>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    DisplayMessage = "Book created successfully.",
                    Result = createdBookDto
                };

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<BookDto>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return await Task.FromResult(response);
            }
        }


    }
}
