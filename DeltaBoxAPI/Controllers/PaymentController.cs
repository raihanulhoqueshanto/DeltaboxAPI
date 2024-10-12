using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment.Queries;
using DeltaboxAPI.Domain.Entities.DeltaBox.Payment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaboxAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOrUpdatePaymentProof(PaymentProof command)
        {
            try
            {
                var result = await _mediator.Send(new CreateOrUpdatePaymentProof(command));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaymentProof(int? id, string? name, string? isActive, string? getAll, int currentPage, int itemsPerPage)
        {
            try
            {
                var result = await _mediator.Send(new GetPaymentProof(id, name, isActive, getAll, currentPage, itemsPerPage));

                PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
