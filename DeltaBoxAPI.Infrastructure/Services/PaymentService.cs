using Dapper;
using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Constants;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment.Queries;
using DeltaboxAPI.Domain.Entities.DeltaBox.Payment;
using DeltaboxAPI.Infrastructure.Utils;
using DeltaBoxAPI.Application.Common.Models;
using DeltaBoxAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<PagedList<PaymentProofVM>> GetPaymentProof(GetPaymentProof request)
        {
            string conditionClause = " ";
            var queryBuilder = new StringBuilder();
            var parameter = new DynamicParameters();

            queryBuilder.AppendLine("SELECT payment_proof.*, count(*) over() as TotalItems FROM payment_proof ");

            if (request.Id != null)
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} id = @Id");
                conditionClause = " WHERE ";
                parameter.Add("Id", request.Id, DbType.String, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} name = @Name");
                conditionClause = " WHERE ";
                parameter.Add("Name", request.Name, DbType.String, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(request.IsActive))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} is_active = @IsActive");
                conditionClause = " WHERE ";
                parameter.Add("IsActive", request.IsActive, DbType.String, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(request.GetAll) && request.GetAll.ToUpper() == "Y")
            {
                request.ItemsPerPage = 0;
            }
            else
            {
                queryBuilder.AppendLine("LIMIT @Offset, @ItemsPerPage");
                parameter.Add("Offset", (request.CurrentPage - 1) * request.ItemsPerPage, DbType.Int32, ParameterDirection.Input);
                parameter.Add("ItemsPerPage", request.ItemsPerPage, DbType.Int32, ParameterDirection.Input);
            }

            string query = queryBuilder.ToString();
            var result = await _mysqlContext.GetPagedListAsync<PaymentProofVM>(request.CurrentPage, request.ItemsPerPage, query, parameter);
            return result;
        }
    }
}
