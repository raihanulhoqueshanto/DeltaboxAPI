using DeltaboxAPI.Application.Common.Models.DTO;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Auth.Commands;
using DeltaBoxAPI.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Auth
{
    public interface IAuthService : IDisposable
    {
        Task<Result> LoginRequest(LoginModel request);
        Task<Result> UserRegistrationRequest(RegistrationModel request);
        Task<Result> AdminRegistrationRequest(RegistrationModel request);
        Task<Result> ChangePasswordRequest(ChangePasswordModel request);
    }
}
