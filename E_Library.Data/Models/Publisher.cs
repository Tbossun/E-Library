using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Models
{
    public class Publisher : BaseEntity
    {
        public string PublisherName { get; set; }
        public ICollection<Book> PublishedBooks { get; set; }
    }
}
