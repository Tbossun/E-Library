using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Models
{
    public class Tag : BaseEntity
    {
        public string TagName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
