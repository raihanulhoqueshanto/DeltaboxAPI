using DeltaBoxAPI.Application.Common.Models;
using DeltaBoxAPI.Domain.Entities.ContactManagement.Contact;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaBoxAPI.Application.Requests.ContactManagement.Contacts.Commands
{
    public class CreateOrUpdateContact : IRequest<Result>
    {
        public Contact Contact { get; set; }

        public CreateOrUpdateContact(Contact contact)
        {
            Contact = contact;
        }
    }

    public class CreateOrUpdateContactHandler : IRequestHandler<CreateOrUpdateContact, Result>
    {
        private readonly IContactService _contactService;

        public CreateOrUpdateContactHandler(IContactService contactService)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        }

        public async Task<Result> Handle(CreateOrUpdateContact request, CancellationToken cancellationToken)
        {
            var result = await _contactService.CreateOrUpdateContact(request.Contact);
            return result;
        }
    }
}
