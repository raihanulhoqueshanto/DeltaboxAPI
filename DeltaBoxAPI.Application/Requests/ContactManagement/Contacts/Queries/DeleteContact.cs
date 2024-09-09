using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaBoxAPI.Application.Requests.ContactManagement.Contacts.Queries
{
    public class DeleteContact : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string DeleteAll { get; set; }

        public DeleteContact(Guid id, string deleteAll)
        {
            Id = id;
            DeleteAll = deleteAll;
        }
    }

    public class DeleteContactHandler : IRequestHandler<DeleteContact, Result>
    {
        private readonly IContactService _contactService;

        public DeleteContactHandler(IContactService contactService)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        }

        public async Task<Result> Handle(DeleteContact request, CancellationToken cancellationToken)
        {
            var result = await _contactService.DeleteContact(request);
            return result;
        }
    }
}
