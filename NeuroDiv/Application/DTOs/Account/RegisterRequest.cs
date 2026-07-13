using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account
{
    public class RegisterRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? OtherName { get; set; }
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public required string Email { get; set; }

        [MinLength(6)]
        public required string Password { get; set; }

        [Compare("Password")]
        public required string ConfirmPassword { get; set; }
    }
}
