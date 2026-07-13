using Application.DTOs.CommonNodes;
using System;

namespace Application.DTOs.OrganizationUsersInvite
{
    public class ValidateInviteTokenVM
    {
        public string Token { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public Guid OrganizationId { get; set; }


        /// <summary>
        /// Tells the frontend which path to show:
        /// true  → show Login screen, then call AcceptInviteExistingUser
        /// false → show Registration form pre-filled with FirstName/LastName/Email
        /// </summary>
        public bool HasExistingAccount { get; set; }
    }

    public class RegisterViaInviteRequest
    {
        public string Token { get; set; }
        public required string FirstName { get; set; }  
        public required string LastName { get; set; }   
        public required string Email { get; set; }   
        public required string? PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }

}
