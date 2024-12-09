using DeltaboxAPI.Application.Common.Interfaces;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer;
using DeltaboxAPI.Domain.Entities.DeltaBox.Offer;
using DeltaBoxAPI.Application.Common.Models;
using DeltaBoxAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Infrastructure.Services
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _context;
        private readonly MysqlDbContext _mysqlContext;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ICurrentUserService _currentUserService;

        public OfferService(ApplicationDbContext context, MysqlDbContext mysqlContext, IHostEnvironment hostEnvironment, ICurrentUserService currentUserService)
        {
            _context = context;
            _mysqlContext = mysqlContext;
            _hostEnvironment = hostEnvironment;
            _currentUserService = currentUserService;
        }

        public void Dispose()
        {
            _context.Dispose();
            _mysqlContext.Dispose();
        }

        public async Task<Result> CreateOrUpdatePromotionCode(PromotionCode request)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrEmpty(request.Code))
                {
                    return Result.Failure("Failed", "500", new[] { "Promotion Code is required!" }, null);
                }

                if (request.Amount <= 0)
                {
                    return Result.Failure("Failed", "500", new[] { "Amount must be greater than zero!" }, null);
                }

                if (request.PromotionStartDate >= request.PromotionEndDate)
                {
                    return Result.Failure("Failed", "500", new[] { "Promotion end date must be after start date!" }, null);
                }

                var normalizedCode = request.Code.Replace(" ", "").ToLower();

                if (request.Id > 0)
                {
                    // Update existing promotion code
                    var existingPromotionCode = await _context.PromotionCodes.FindAsync(request.Id);

                    if (existingPromotionCode == null)
                    {
                        return Result.Failure("Failed", "404", new[] { "Promotion Code not found!" }, null);
                    }

                    // Check for duplicate unique code (excluding current record)
                    var duplicateCode = await _context.PromotionCodes
                        .FirstOrDefaultAsync(c =>
                            c.Code.Replace(" ", "").ToLower() == normalizedCode &&
                            c.Id != request.Id);

                    if (duplicateCode != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Promotion Code must be unique!" }, null);
                    }

                    // Update existing PromotionCode object
                    existingPromotionCode.Name = request.Name;
                    existingPromotionCode.Code = request.Code;
                    existingPromotionCode.Amount = request.Amount;
                    existingPromotionCode.PromotionStartDate = request.PromotionStartDate;
                    existingPromotionCode.PromotionEndDate = request.PromotionEndDate;
                    existingPromotionCode.OneTime = request.OneTime;
                    existingPromotionCode.IsActive = request.IsActive;

                    _context.PromotionCodes.Update(existingPromotionCode);

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                         ? Result.Success("Success", "200", new[] { "Promotion Code Updated Successfully" }, null)
                         : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
                }
                else
                {
                    // Create new promotion code
                    var existingCode = await _context.PromotionCodes
                        .FirstOrDefaultAsync(c => c.Code.Replace(" ", "").ToLower() == normalizedCode);

                    if (existingCode != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Duplicate Promotion Code. Code must be unique." }, null);
                    }

                    await _context.PromotionCodes.AddAsync(request);

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                         ? Result.Success("Success", "200", new[] { "Promotion Code Saved Successfully" }, null)
                         : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Result.Failure("Failed", "500", new[] { errorMessage }, null);
            }
        }
    }
}
