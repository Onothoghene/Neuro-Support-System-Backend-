using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = Convert.ToString(httpContextAccessor.HttpContext?.User?.FindFirstValue("uid"));
            Role = httpContextAccessor.HttpContext?.User?.FindFirstValue("rol");
        }

        public string UserId { get; }
        public string Role { get; }
    }
}
