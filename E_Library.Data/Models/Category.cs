using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Models
{
    public class Category  : BaseEntity
    {
        public string CategoryName { get; set; }
        public ICollection<SubCategory> Subcategories { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
