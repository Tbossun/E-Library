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
        private readonly IDocumentUploadService _uploadService;

        public BookServices(IUnitOfWork unitOfWork, IMapper mapper, IDocumentUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
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



        public async Task<ResponseDto<IEnumerable<BookDto>>> SearchBooksAsync(string searchTerm, int pageNumber, int pageSize)
        {
            try
            {
                Expression<Func<Book, bool>> filter = b =>
                    b.ISBN.Contains(searchTerm) ||
                    b.Title.Contains(searchTerm) ||
                    b.Authors.Any(a => a.AuthorName.Contains(searchTerm)) || // Check any author's name
                    b.Category.CategoryName.Contains(searchTerm);

                var includeProperties = new Expression<Func<Book, object>>[]
                {
            b => b.Authors,
            b => b.Category
                };

                var books = await _unitOfWork.BookRepo.SearchAsync(filter, includeProperties);
                var booksDto = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books);

                // Apply paging if needed
                if (pageNumber > 0 && pageSize > 0)
                {
                    booksDto = booksDto.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                }

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

        string imageUrl = existingBook.ImageURL; // Preserve existing image URL

        if (bookDto.File != null)
        {
            var bookImage = await _uploadService.UploadImageAsync(bookDto.File);
            imageUrl = bookImage.Url.ToString();
        }

        _mapper.Map(bookDto, existingBook);
        existingBook.ImageURL = imageUrl; // Update the image URL
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
                string imageUrl = string.Empty;

                if (book.File != null)
                {
                    var bookImage = await _uploadService.UploadImageAsync(book.File);
                    imageUrl = bookImage.Url.ToString();
                }

                var newBook = _mapper.Map<Book>(book);
                newBook.ImageURL = imageUrl;
                _unitOfWork.BookRepo.Add(newBook);
                _unitOfWork.Save();

                var createdBookDto = _mapper.Map<BookDto>(newBook);

                var response = new ResponseDto<BookDto>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    DisplayMessage = "Book created successfully.",
                    Result = createdBookDto
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




        public async Task<ResponseDto<IEnumerable<GetCategory>>> GetCategories()
        {
            try
            {
                var cat =  _unitOfWork.CategoryRepo.GetAll(); // Assuming you have an asynchronous GetAllAsync method
                var categoriesDto = _mapper.Map<IEnumerable<Category>, IEnumerable<GetCategory>>(cat);

                var response = new ResponseDto<IEnumerable<GetCategory>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Categories retrieved successfully.",
                    Result = categoriesDto
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<IEnumerable<GetCategory>>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return response;
            }
        }


        public async Task<ResponseDto<GetCategory>> GetACategory(string id)
        {
            try
            {
                var Category = _unitOfWork.CategoryRepo.Get(b => b.Id == id);
                if (Category == null)
                {
                    var notFoundResponse = new ResponseDto<GetCategory>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        DisplayMessage = "Category not found.",
                        Result = null
                    };

                    return notFoundResponse;
                }

                var category = _mapper.Map<GetCategory>(Category);

                var response = new ResponseDto<GetCategory>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Category retrieved successfully.",
                    Result = category
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<GetCategory>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return response;
            }
        }


        public async Task<ResponseDto<CreateCategory>> CreateACategory(CreateCategory category)
        {
            try
            {
                var newCategory = _mapper.Map<Category>(category);
                _unitOfWork.CategoryRepo.Add(newCategory);
                _unitOfWork.Save();

                var createdBookDto = _mapper.Map<CreateCategory>(newCategory);

                var response = new ResponseDto<CreateCategory>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    DisplayMessage = "New Category created successfully.",
                    Result = createdBookDto
                };

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<CreateCategory>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return await Task.FromResult(response);
            }
        }

        public Task<ResponseDto<CreateCategory>> UpdateCategory(string id, CreateCategory category)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<bool>> DeleteCategory(string id)
        {
            try
            {
                var existingCategory = _unitOfWork.CategoryRepo.Get(b => b.Id == id);
                if (existingCategory == null)
                {
                    var notFoundResponse = new ResponseDto<bool>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        DisplayMessage = "Category not found.",
                        Result = false
                    };

                    return notFoundResponse;
                }

                _unitOfWork.CategoryRepo.Remove(existingCategory);
                _unitOfWork.Save();

                var response = new ResponseDto<bool>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Category deleted successfully.",
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

        public Task<ResponseDto<IEnumerable<GetCategory>>> SearchCategory(string isbn, string? categoryId, string? subcategoryId, string? publisherId, string searchTerm, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<IEnumerable<BookDto>>> GetBooksByCategoryAsync(string categoryId, int? pageNumber, int? pageSize)
        {
            try
            {
                var books = _unitOfWork.BookRepo.GetBooksByCategory(categoryId);
                var booksDto = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books);

                // Apply paging if needed
                if (pageNumber.HasValue && pageSize.HasValue)
                {
                    booksDto = booksDto.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                var response = new ResponseDto<IEnumerable<BookDto>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Books retrieved successfully.",
                    Result = booksDto
                };

                return await Task.FromResult(response); // Wrap the response in Task.FromResult
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<IEnumerable<BookDto>>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return await Task.FromResult(response); // Wrap the response in Task.FromResult
            }
        }

    }
}
