using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Brand.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Brand.Queries;
using DeltaboxAPI.Domain.Entities.DeltaBox.Brand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaboxAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOrUpdateBrand(AssociateBrand command)
        {
            try
            {
                var result = await _mediator.Send(new CreateOrUpdateBrand(command));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBrand(int? id, string? name, string? isActive, string? getAll, int currentPage, int itemsPerPage)
        {
            try
            {
                var result = await _mediator.Send(new GetBrand(id, name, isActive, getAll, currentPage, itemsPerPage));

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
