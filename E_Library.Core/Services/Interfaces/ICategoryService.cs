/*using E_Library.Core.Services.Implementations;
using E_Library.Data.DTOS.Request;
using E_Library.Data.DTOS.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Core.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseDto<IEnumerable<CreateCategory>>> GetCategories(int? pageNumber, int? pageSize);
        Task<ResponseDto<CreateCategory>> GetACategory(string id);
        Task<ResponseDto<CreateCategory>> CreateACategory(CreateCategory category);
        Task<ResponseDto<CreateCategory>> UpdateCategory(string id, CreateCategory category);
        Task<ResponseDto<bool>> DeleteCategory(string id);
        Task<ResponseDto<IEnumerable<CreateCategory>>> SearchCategory(
            string isbn, string? categoryId, string? subcategoryId,
            string? publisherId, string searchTerm, int pageNumber, int pageSize);
    }
}
*/