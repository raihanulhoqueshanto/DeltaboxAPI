using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Commands;
using DeltaboxAPI.Domain.Entities.DeltaBox.Faqs;
using DeltaBoxAPI.Application.Common.Models;
using DeltaBoxAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Infrastructure.Services
{
    public class FaqsService : IFaqsService
    {
        private readonly ApplicationDbContext _context;

        public FaqsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<Result> CreateOrUpdateFaqs(FaqsSetup request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Title))
                {
                    return Result.Failure("Failed", "500", new[] { "Title is required!" }, null);
                }

                var normalizedTitle = request.Title.Replace(" ", "").ToLower();

                if (request.Id != Guid.Empty)
                {
                    var faqsObj = await _context.FaqsSetups.FindAsync(request.Id);

                    if (faqsObj == null)
                    {
                        return Result.Failure("Failed", "404", new[] { "Resource not found!" }, null);
                    }

                    var existingFaqs = await _context.FaqsSetups
                        .FirstOrDefaultAsync(c => c.Title.Replace(" ", "").ToLower() == normalizedTitle && c.Id != request.Id);

                    if (existingFaqs != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Title already exists!" }, null);
                    }

                    // Update existing FAQs object
                    faqsObj.Title = request.Title;
                    faqsObj.Description = request.Description;
                    faqsObj.IsActive = request.IsActive;

                    _context.FaqsSetups.Update(faqsObj);
                }
                else
                {
                    var existingFaqs = await _context.FaqsSetups
                        .FirstOrDefaultAsync(c => c.Title.Replace(" ", "").ToLower() == normalizedTitle);

                    if (existingFaqs != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Duplicate data found. Title already exists." }, null);
                    }

                    await _context.FaqsSetups.AddAsync(request);
                }

                int result = await _context.SaveChangesAsync();

                return result > 0
                    ? Result.Success("Success", "200", new[] { request.Id != Guid.Empty ? "Updated Successfully!" : "Saved Successfully!" }, null)
                    : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Result.Failure("Failed", "500", new[] { errorMessage }, null);
            }
        }

    }
}
