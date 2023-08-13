using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       // public string Phone { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public string ImageURL { get; set; }   = string.Empty;
        public ICollection<WishlistItem> WishlistItems { get; set; }
        public ICollection<BookRole> BookRoles { get; set; }
    }
}
