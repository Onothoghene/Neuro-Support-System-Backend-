using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account
{
    public class RegisterOrganizationRequest
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

        public required string OrgName { get; set; }
        public string? OrgDescription { get; set; }
        public string? OrgPhoneNumber { get; set; }
        public string? OrgAddress { get; set; }
        public string? OrgCity { get; set; }
        public string? OrgCountry { get; set; }
        public string? OrgWebsite { get; set; }

        /// <summary>
        /// Optional — only for orgs that have a private domain (e.g. "@xyz.com").
        /// Leave null if staff use personal emails.
        /// </summary>
        public string? VerifiedDomain { get; set; }
    }
}
