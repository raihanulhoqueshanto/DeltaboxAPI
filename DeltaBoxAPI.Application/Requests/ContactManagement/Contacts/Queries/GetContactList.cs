using DeltaBoxAPI.Domain.Entities.ContactManagement.Contact;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaBoxAPI.Application.Requests.ContactManagement.Contacts.Queries
{
    public class GetContactList : IRequest<List<Contact>>
    {
        public Guid Id { get; set; }
        public string GetAll { get; set; }

        public GetContactList(Guid id, string getAll)
        {
            Id = id;
            GetAll = getAll;
        }
    }

    public class GetContactListHandler : IRequestHandler<GetContactList, List<Contact>>
    {
        private readonly IContactService _contactService;

        public GetContactListHandler(IContactService contactService)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        }

        public async Task<List<Contact>> Handle(GetContactList request, CancellationToken cancellationToken)
        {
            return await _contactService.GetContactList(request);
        }
    }
}
