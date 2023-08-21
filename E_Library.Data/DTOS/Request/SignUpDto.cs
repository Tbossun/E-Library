using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace E_Library.API.Controllers
{
    public class SignUpDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }
    }
}