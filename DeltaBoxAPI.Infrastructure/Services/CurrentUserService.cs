using DeltaboxAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            RoleId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
            Username = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        }
        public string UserId { get; }
        public string RoleId { get; }
        public string Username { get; }
    }
}
