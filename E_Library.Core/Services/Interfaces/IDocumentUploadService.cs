using E_Library.Data.Domains;
using Microsoft.AspNetCore.Http;

namespace E_Library.Core.Services.Interfaces
{
    public interface IDocumentUploadService
    {
        Task<DocumentUploadResult> UploadFileAsync(string documentContent);
        Task<DocumentUploadResult> UploadImageAsync(IFormFile image);
    }
}
