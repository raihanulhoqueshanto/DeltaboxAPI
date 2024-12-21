using Dapper;
using DeltaboxAPI.Application.Common.Interfaces;
using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer.Queries;
using DeltaboxAPI.Domain.Entities.DeltaBox.Offer;
using DeltaboxAPI.Infrastructure.Utils;
using DeltaBoxAPI.Application.Common.Models;
using DeltaBoxAPI.Infrastructure.Data;
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

        public async Task<PagedList<PromotionCodeVM>> GetPromotionCode(GetPromotionCode request)
        {
            string conditionClause = " ";
            var queryBuilder = new StringBuilder();
            var parameter = new DynamicParameters();

            queryBuilder.AppendLine("SELECT promotion_code.*, count(*) over() as TotalItems FROM promotion_code ");

            if (request.Id != null)
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} id = @Id");
                conditionClause = " WHERE ";
                parameter.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} name LIKE @Name");
                conditionClause = " WHERE ";
                parameter.Add("Name", $"%{request.Name}%", DbType.String, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(request.Code))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} code LIKE @Code");
                conditionClause = " WHERE ";
                parameter.Add("Code", $"%{request.Code}%", DbType.String, ParameterDirection.Input);
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
            var result = await _mysqlContext.GetPagedListAsync<PromotionCodeVM>(request.CurrentPage, request.ItemsPerPage, query, parameter);
            return result;
        }

        public async Task<RewardPointVM> GetRewardPoints()
        {
            decimal rewardPoints = 0;
            var customerId = _currentUserService.UserId;
            var rewardPointObj = await _context.RewardPoints.FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (rewardPointObj != null)
            {
                rewardPoints = rewardPointObj.Point - rewardPointObj.RedeemedPoint ?? 0;
            }
            else
            {
                rewardPoints = 0;
            }

            RewardPointVM rewardPoint = new RewardPointVM()
            {
                RewardPointBalance = rewardPoints
            };

            return rewardPoint;
        }

        public async Task<Result> ApplyPromotionCode(PromotionCodeRequest request)
        {
            if (string.IsNullOrEmpty(request.Code))
            {
                return Result.Failure("Failed", "500", new[] { "Promotion code is required!" }, null);
            }

            // Check if promotion code exists and is valid
            var promotionCode = await _context.PromotionCodes
                .FirstOrDefaultAsync(c =>
                    c.Code == request.Code &&
                    c.IsActive == "Y" &&
                    c.PromotionStartDate <= DateTime.Now &&
                    DateTime.Now <= c.PromotionEndDate);

            if (promotionCode == null)
            {
                return Result.Failure("Failed", "404", new[] { "Invalid promotion code." }, null);
            }

            // Check usage count for one-time codes
            if (promotionCode.OneTime == "Y")
            {
                var usageCount = await _context.OrderProfiles
                    .CountAsync(o => o.CustomerId == _currentUserService.UserId.ToString() &&
                                    o.PromotionCode == request.Code);

                if (usageCount > 0)
                {
                    return Result.Failure("Failed", "406", new[] { "Already used." }, null);
                }
            }

            // Calculate total and return success response
            var response = new PromotionCodeResponse
            {
                PromotionCodeAmount = promotionCode.Amount,
                SubTotal = request.SubTotal,
                RedeemedPoint = request.RedeemedPoint,
                Total = request.SubTotal - request.RedeemedPoint - promotionCode.Amount
            };

            return Result.Success("Success", "200", new[] { "Promotion code applied successfully." }, response);
        }

        public Task<Result> ApplyRedeemedPoints(RedeemedPointRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
