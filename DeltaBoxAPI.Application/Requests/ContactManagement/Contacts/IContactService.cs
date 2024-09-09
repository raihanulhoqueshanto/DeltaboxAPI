using DeltaBoxAPI.Application.Common.Models;
using DeltaBoxAPI.Application.Requests.ContactManagement.Contacts.Queries;
using DeltaBoxAPI.Domain.Entities.ContactManagement.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaBoxAPI.Application.Requests.ContactManagement.Contacts
{
    public interface IContactService : IDisposable
    {
        Task<Result> CreateOrUpdateContact(Contact request);
        Task<List<Contact>> GetContactList(GetContactList request);
        Task<Result> DeleteContact(DeleteContact request);
    }
}
