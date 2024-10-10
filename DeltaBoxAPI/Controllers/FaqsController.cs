using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Queries;
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
        [Authorize(Roles = "Admin")]
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetFaqs(int? id, string? title, string? isActive, string? getAll, int currentPage, int itemsPerPage)
        {
            try
            {
                var result = await _mediator.Send(new GetFaqs(id, title, isActive, getAll, currentPage, itemsPerPage));

                PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOrUpdateGeneralQuestion(GeneralQuestion command)
        {
            try
            {
                var result = await _mediator.Send(new CreateOrUpdateGeneralQuestion(command));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGeneralQuestions(int? id, string? question, string? isActive, string? getAll, int currentPage, int itemsPerPage)
        {
            try
            {
                var result = await _mediator.Send(new GetGeneralQuestions(id, question, isActive, getAll, currentPage, itemsPerPage));

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
