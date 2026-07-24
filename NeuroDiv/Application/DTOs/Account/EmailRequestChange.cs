using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Account
{
     public class RequestEmailChangeRequest
    {
        /// <summary>The new email address the user wants to switch to.</summary>
        public required string NewEmail { get; set; }

        /// <summary>
        /// Current password — confirms it's really the account owner
        /// making this request.
        /// </summary>
        public required string Password { get; set; }
    }

    public class ConfirmEmailChangeRequest
    {
        /// <summary>OTP sent to the new email address.</summary>
        public int Otp { get; set; }
    }
}
