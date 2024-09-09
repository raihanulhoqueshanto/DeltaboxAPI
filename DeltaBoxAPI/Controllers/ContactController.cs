using DeltaBoxAPI.Application.Requests.ContactManagement.Contacts.Commands;
using DeltaBoxAPI.Application.Requests.ContactManagement.Contacts.Queries;
using DeltaBoxAPI.Domain.Entities.ContactManagement.Contact;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeltaboxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> CreateOrUpdateContact(Contact command)
        {
            var result = await _mediator.Send(new CreateOrUpdateContact(command));
            return Ok(result);
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> GetContactList(Guid id, string getAll)
        {
            var result = await _mediator.Send(new GetContactList(id, getAll));
            return Ok(result);
        }

        [HttpDelete]
        [Route("[Action]")]
        public async Task<IActionResult> DeleteContact(Guid id, string deleteAll)
        {
            var result = await _mediator.Send(new DeleteContact(id, deleteAll));
            return Ok(result);
        }
    }
}
