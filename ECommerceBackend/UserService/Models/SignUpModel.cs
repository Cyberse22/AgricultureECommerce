﻿using System.ComponentModel.DataAnnotations;
using UserService.Helpers;

namespace UserService.Models
{
    public class SignUpModel
    {
        [Required]
        public string? FirstName {  get; set; }
        [Required]
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required, Compare("Password")] public string? PasswordConfirmation { get; set; }
    }
}
