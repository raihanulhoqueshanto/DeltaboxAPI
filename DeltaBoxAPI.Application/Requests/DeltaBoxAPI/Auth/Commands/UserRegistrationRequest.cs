using DeltaboxAPI.Application.Common.Models.DTO;
using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Auth.Commands
{
    public class UserRegistrationRequest : IRequest<Result>
    {
        public RegistrationModel RegistrationModel { get; set; }

        public UserRegistrationRequest(RegistrationModel registrationModel)
        {
            RegistrationModel = registrationModel;
        }
    }

    public class RegistrationRequestHandler : IRequestHandler<UserRegistrationRequest, Result>
    {
        private readonly IAuthService _authService;

        public RegistrationRequestHandler(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<Result> Handle(UserRegistrationRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.UserRegistrationRequest(request.RegistrationModel);
            return result;
        }
    }
}
