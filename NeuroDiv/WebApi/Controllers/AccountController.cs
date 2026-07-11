using Application.DTOs.Account;
using Application.DTOs.OrganizationUsersInvite;
using Application.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Endpoint to login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            var result = await _accountService.AuthenticateAsync(request);
            SetRefreshTokenInCookie(result.Data.RefreshToken);
            return Ok(result);
        }

        /// <summary>
        /// To be used when a freelancer/independent therapist who finds the app on their own wants to register. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await _accountService.RegisterAsync(request));
        }

        /// <summary>
        /// To be used when a therapist was invited by an org admin to join the platform. 
        /// The org admin will send an invite link to the therapist's email, 
        /// and the therapist will use that link to register.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register-via-invite")]
        public async Task<IActionResult> RegisterViaInviteAsync(RegisterViaInviteRequest request)
        {
            return Ok(await _accountService.RegisterViaInviteAsync(request));
        }

        /// <summary>
        /// Validates an invite token when the invitee clicks the email link.
        /// Returns pre-filled details and whether they already have an account.
        /// Call this FIRST before showing any UI to the invitee.
        /// </summary>
        [HttpGet("validate-invite")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateInvite([FromQuery] string token)
        {
            return Ok(await _accountService.ValidateInviteTokenAsync(token));
        }

        /// <summary>
        /// Called when an already-registered user accepts an invite.
        /// User must be authenticated. Only call this if ValidateInvite
        /// returned HasExistingAccount = true and the user has logged in.
        /// </summary>
        [HttpPost("accept-invite")]
        [Authorize]
        public async Task<IActionResult> AcceptInvite([FromQuery] string token)
        {
            return Ok(await _accountService.AcceptInviteExistingUserAsync(token));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("verify")]
        [AllowAnonymous]
        public async Task<IActionResult> Verify(OtpRequest request)
        {
            return Ok(await _accountService.VerifyUser(request.Otp));
        }

        //not in use
        [HttpGet("verify-otp/{otp}")]
        public async Task<IActionResult> VerifyOtp(int otp)
        {
            return Ok(await _accountService.VerifyOtp(otp));
        }

        /// <summary>
        /// Forgot password endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            return Ok(await _accountService.ForgotPassword(model));
        }

        /// <summary>
        /// reset password endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            return Ok(await _accountService.ResetPassword(model));
        }

        /// <summary>
        /// change password endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest model)
        {
            var response = await _accountService.ChangePassword(model);
            if (response.Succeeded)
            {
                return Ok(response); // 200 OK
            }
            else
            {
                return BadRequest(response); // 400 Bad Request
            }
        }

        /// <summary>
        /// Logs out from all devices by clearing all refresh tokens.
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            return Ok(await _accountService.LogOutAsync());
        }

        /// <summary>
        /// Logs out from the current device only by revoking the specific refresh token.
        /// </summary>
        [HttpPost("logout-device")]
        [Authorize]
        public async Task<IActionResult> LogoutDevice([FromQuery] string refreshToken)
        {
            return Ok(await _accountService.LogOutAsync(refreshToken));
        }

        /// <summary>
        /// Refresh token endpoint
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string token)
        {
            var resp = await _accountService.RefreshTokenAsync(token);
            return Ok(resp);
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        /// <summary>
        /// used to create an admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdminAsync(RegisterRequest request)
        {
            return Ok(await _accountService.CreateAdmin(request));
        }
    }
}
