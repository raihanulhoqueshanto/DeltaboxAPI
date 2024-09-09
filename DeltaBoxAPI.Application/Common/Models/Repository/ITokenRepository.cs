using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaBoxAPI.Application.Common.Models.Repository
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user);
    }
}
