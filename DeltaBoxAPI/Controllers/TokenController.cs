using DeltaBoxAPI.Application.Common.Models.Repository;
using DeltaBoxAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace DeltaboxAPI.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenRepository _tokenRepository;

        public TokenController(ApplicationDbContext context, ITokenRepository tokenRepository)
        {
            _context = context;
            _tokenRepository = tokenRepository;
        }
    }
}
