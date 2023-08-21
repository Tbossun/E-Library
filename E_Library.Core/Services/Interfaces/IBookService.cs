using E_Library.Core.Services.Implementations;
using E_Library.Data.DTOS.Request;
using E_Library.Data.DTOS.Response;
using E_Library.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Core.Services.Interfaces
{
    public interface IBookService
    {
        Task<ResponseDto<IEnumerable<BookDto>>> GetBooksAsync(int? pageNumber, int? pageSize);
        Task<ResponseDto<BookDto>> GetBookAsync(string id);
        Task<ResponseDto<BookDto>> CreateBookAsync(AddBookDto book);
        Task<ResponseDto<BookDto>> UpdateBookAsync(string id, UpdateBookDto book);
        Task<ResponseDto<bool>> DeleteBookAsync(string id);
        Task<ResponseDto<IEnumerable<BookDto>>> SearchBooksAsync(string searchTerm, int pageNumber, int pageSize);
        Task<ResponseDto<IEnumerable<GetCategory>>> GetCategories();
        Task<ResponseDto<GetCategory>> GetACategory(string id);
        Task<ResponseDto<CreateCategory>> CreateACategory(CreateCategory category);
        Task<ResponseDto<CreateCategory>> UpdateCategory(string id, CreateCategory category);
        Task<ResponseDto<bool>> DeleteCategory(string id);
        Task<ResponseDto<IEnumerable<BookDto>>> GetBooksByCategoryAsync(string categoryId, int? pageNumber, int? pageSize);
        Task<ResponseDto<IEnumerable<GetCategory>>> SearchCategory(
            string isbn, string? categoryId, string? subcategoryId,
            string? publisherId, string searchTerm, int pageNumber, int pageSize);
    }
}
