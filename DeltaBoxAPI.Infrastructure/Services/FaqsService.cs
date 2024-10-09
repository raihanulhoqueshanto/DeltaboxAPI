using Dapper;
using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Queries;
using DeltaboxAPI.Domain.Entities.DeltaBox.Faqs;
using DeltaboxAPI.Infrastructure.Utils;
using DeltaBoxAPI.Application.Common.Models;
using DeltaBoxAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Infrastructure.Services
{
    public class FaqsService : IFaqsService
    {
        private readonly ApplicationDbContext _context;
        private readonly MysqlDbContext _mysqlContext;

        public FaqsService(ApplicationDbContext context, MysqlDbContext mysqlContext)
        {
            _context = context;
            _mysqlContext = mysqlContext;
        }

        public void Dispose()
        {
            _context.Dispose();
            _mysqlContext.Dispose();
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

                if (request.Id > 0)
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

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                         ? Result.Success("Success", "200", new[] { "Updated Successfully" }, null)
                         : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
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

        public async Task<PagedList<FaqsVM>> GetFaqs(GetFaqs request)
        {
            string conditionClause = " ";
            var queryBuilder = new StringBuilder();
            var parameter = new DynamicParameters();

            queryBuilder.AppendLine("SELECT faqs_setup.*, count(*) over() as TotalItems FROM faqs_setup ");

            if (request.Id != null)
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} id = @Id");
                conditionClause = " WHERE ";
                parameter.Add("Id", request.Id, DbType.String, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} title = @Title");
                conditionClause = " WHERE ";
                parameter.Add("Title", request.Title, DbType.String, ParameterDirection.Input);
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
            var result = await _mysqlContext.GetPagedListAsync<FaqsVM>(request.CurrentPage, request.ItemsPerPage, query, parameter);
            return result;
        }
    }
}
