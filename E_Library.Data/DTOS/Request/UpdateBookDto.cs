using E_Library.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace E_Library.Core.Services.Implementations
{
    public class UpdateBookDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublisherId { get; set; }

        public string ISBN { get; set; }
        public IFormFile File { get; set; } = null;
        public DateOnly YearPublished { get; set; }
        public string CategoryId { get; set; }

        public BookLanguage Language { get; set; }
        public BookFormat Format { get; set; }
        public BookAvailability Availability { get; set; }
    }
}