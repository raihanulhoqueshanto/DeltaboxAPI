using DeltaboxAPI.Application.Constants;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment;
using DeltaboxAPI.Domain.Entities.DeltaBox.Payment;
using DeltaboxAPI.Infrastructure.Utils;
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
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly MysqlDbContext _mysqlContext;
        private readonly IHostEnvironment _hostEnvironment;

        public PaymentService(ApplicationDbContext context, MysqlDbContext mysqlContext, IHostEnvironment hostEnvironment)
        {
            _context = context;
            _mysqlContext = mysqlContext;
            _hostEnvironment = hostEnvironment;
        }

        public void Dispose()
        {
            _context.Dispose();
            _mysqlContext.Dispose();
        }

        public async Task<Result> CreateOrUpdatePaymentProof(PaymentProof request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return Result.Failure("Failed", "500", new[] { "Title is required!" }, null);
                }
                var normalizedName = request.Name.Replace(" ", "").ToLower();

                if (!ImageDirectory.IsFileExists(_hostEnvironment, request.Image))
                {
                    var img = Helper.SaveSingleImage(request.Image, PathConstant.PAYMENT_PROOF_PATH, _hostEnvironment);
                    if (!img.Result.Succeed) 
                    {
                        return Result.Failure("Failed", "500", new[] { "Payment proof image not saved. Please try again!" }, null);
                    }
                    else
                    {
                        request.Image = img.Result.Status; /*img.Result.Message;*/
                    }
                }

                if (request.Id > 0)
                {
                    var paymentProofObj = await _context.PaymentProofs.FindAsync(request.Id);

                    if (paymentProofObj == null)
                    {
                        return Result.Failure("Failed", "404", new[] { "Resource not found!" }, null);
                    }

                    var existingProof = await _context.PaymentProofs.FirstOrDefaultAsync(c => c.Name.Replace(" ", "").ToLower() == normalizedName && c.Id != request.Id);

                    if (existingProof != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Title already exists!" }, null);
                    }

                    // Update existing Proof object
                    paymentProofObj.Name = request.Name;
                    paymentProofObj.IsActive = request.IsActive;

                    _context.PaymentProofs.Update(paymentProofObj);

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                         ? Result.Success("Success", "200", new[] { "Updated Successfully" }, null)
                         : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
                }
                else
                {
                    var existingProof = await _context.PaymentProofs.FirstOrDefaultAsync(c => c.Name.Replace(" ", "").ToLower() == normalizedName);

                    if (existingProof != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Duplicate data found. Title already exists." }, null);
                    }

                    await _context.PaymentProofs.AddAsync(request);

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                         ? Result.Success("Success", "200", new[] { "Saved Successfully" }, null)
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
