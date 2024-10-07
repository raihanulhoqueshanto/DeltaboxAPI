using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Commands;
using DeltaboxAPI.Domain.Entities.DeltaBox.Faqs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeltaboxAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FaqsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FaqsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateFaqs(FaqsSetup command)
        {
            try
            {
                var result = await _mediator.Send(new CreateOrUpdateFaqs(command));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
