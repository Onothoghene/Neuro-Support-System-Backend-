using Application.DTOs.CommonNodes;
using System;

namespace Application.DTOs.OrganizationUsersInvite
{
    public class ValidateInviteTokenVM
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
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
        public string FirstName { get; set; }  
        public string LastName { get; set; }   
        public string Email { get; set; }   
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}
