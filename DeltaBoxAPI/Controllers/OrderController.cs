using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Commands;
using DeltaboxAPI.Domain.Entities.DeltaBox.Order;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeltaboxAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddInWishlist(Wishlist command)
        {
            try
            {
                var result = await _mediator.Send(new AddInWishlist(command));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
