using DeltaBoxAPI.Application.Common.Models;
using DeltaBoxAPI.Application.Requests.ContactManagement.Contacts;
using DeltaBoxAPI.Application.Requests.ContactManagement.Contacts.Queries;
using DeltaBoxAPI.Domain.Entities.ContactManagement.Contact;
using DeltaBoxAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaBoxAPI.Infrastructure.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;

        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<Result> CreateOrUpdateContact(Contact request)
        {
            try
            {
                if(request.Id != Guid.Empty)
                {
                    var contactObj = await _context.Contacts.FindAsync(request.Id);

                    if(contactObj != null)
                    {
                        contactObj.Name = request.Name;
                        contactObj.Email = request.Email;
                        contactObj.PhoneNumber = request.PhoneNumber;
                        contactObj.Address = request.Address;

                        _context.Contacts.Update(contactObj);

                        int result = await _context.SaveChangesAsync();

                        if (result > 0)
                        {
                            return Result.Success("Update Success", null, "200");
                        }
                        else
                        {
                            return Result.Failure(new List<string> { "Invalid info. try again!" }, null, null, "500");
                        }
                    }
                    else
                    {
                        return Result.Failure(new List<string> { "Resource is not found!" }, null, null, "404");
                    }
                }
                else
                {
                    await _context.Contacts.AddAsync(request);

                    int result = await _context.SaveChangesAsync();

                    return result > 0 ? Result.Success("Save Success", null, "200") : Result.Failure(new List<string> { "Save Failed", null, null, "500" });
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(new List<string> { ex.InnerException?.Message ?? ex.Message });
            }
        }

        public async Task<List<Contact>> GetContactList(GetContactList request)
        {
            if (request.GetAll == "Y")
            {
                return await _context.Contacts.ToListAsync();
            }
            else
            {
                var query = _context.Contacts.AsQueryable();

                if (request.Id != Guid.Empty)
                {
                    query = query.Where(c => c.Id == request.Id);
                }

                return await query.ToListAsync();
            }
        }

        public async Task<Result> DeleteContact(DeleteContact request)
        {
            try
            {
                if (request.DeleteAll == "Y")
                {
                    _context.Contacts.RemoveRange(_context.Contacts);
                    await _context.SaveChangesAsync();
                    return Result.Success();
                }
                else if (request.Id != Guid.Empty)
                {
                    var contactToDelete = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == request.Id);
                    if (contactToDelete != null)
                    {
                        _context.Contacts.Remove(contactToDelete);
                        await _context.SaveChangesAsync();
                        return Result.Success();
                    }
                    else
                    {
                        return Result.Failure(new List<string> { "Contact not found" });
                    }
                }
                else
                {
                    return Result.Failure(new List<string> { "Invalid contact ID" });
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(new List<string> { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
