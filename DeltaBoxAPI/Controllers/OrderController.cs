using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Queries;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveFromWishlist(int id)
        {
            try
            {
                var result = await _mediator.Send(new RemoveFromWishlist(id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetWishlist()
        {
            try
            {
                var result = await _mediator.Send(new GetWishlist());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(Cart command)
        {
            try
            {
                var result = await _mediator.Send(new AddToCart(command));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            try
            {
                var result = await _mediator.Send(new RemoveFromCart(id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateCartQuantity(UpdateCartQuantityRequest command)
        {
            try
            {
                var result = await _mediator.Send(new UpdateCartQuantity(command));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var result = await _mediator.Send(new GetCart());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
