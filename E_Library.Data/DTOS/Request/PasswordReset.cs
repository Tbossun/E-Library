﻿using System.ComponentModel.DataAnnotations;

namespace E_Library.API.Controllers
{
    public class PasswordReset
    {
        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = null;
        public string Email { get; set; } = null;
        public string Token { get; set; } = null;
    }
}