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
            UserId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
            RoleId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role));
            Username = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        }
        public int UserId { get; }
        public int RoleId { get; }
        public string Username { get; }
    }
}
