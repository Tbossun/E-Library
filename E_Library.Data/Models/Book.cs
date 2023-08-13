using E_Library.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Models
{
    public class Book  : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public string PublisherId { get; set; }

        public string ISBN { get; set; }  
        public string ImageURL { get; set; } = string.Empty;
        public DateOnly YearPublished { get; set; }
        public string CategoryId { get; set; }

        public BookLanguage Language { get; set; }
        public BookFormat Format { get; set; }
        public BookAvailability Availability { get; set; }


        public Publisher Publisher { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<SubCategory> Subcategories { get; set; }
        public ICollection<Reviews> Reviews { get; set; }
    }
}
