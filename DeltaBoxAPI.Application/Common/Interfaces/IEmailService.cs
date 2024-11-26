using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendPasswordResetOTPAsync(string email, string otp);
    }
}
