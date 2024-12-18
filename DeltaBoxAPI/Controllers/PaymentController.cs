using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment.Queries;
using DeltaboxAPI.Domain.Entities.DeltaBox.Payment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeltaBoxAPI.Application.Common.Models;

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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InitiatePayment([FromBody] UddoktaPaymentRequest command)
        {
            try
            {
                // Set redirect and webhook URLs
                command.RedirectUrl = "https://deltaboxit.vercel.app/order/cancel";
                command.CancelUrl = "https://deltaboxit.vercel.app/order/cancel";
                command.WebhookUrl = "";

                var result = await _mediator.Send(new InitiatePayment(command));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result.Failure("Payment Initiation Error", "500", new[] { ex.Message }));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> VerifyPayment([FromBody] string invoiceId)
        {
            try
            {
                var result = await _mediator.Send(new VerifyPayment(invoiceId));

                if (result)
                {
                    return Ok(new { status = true, message = "Payment verified successfully." });
                }

                return BadRequest(new { status = false, message = "Payment verification failed." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result.Failure("Payment Verification Error", "500", new[] { ex.Message }));
            }
        }
    }
}
