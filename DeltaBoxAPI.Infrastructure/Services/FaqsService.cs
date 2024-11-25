using Dapper;
using DeltaboxAPI.Application.Common.Interfaces;
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
        private readonly ICurrentUserService _currentUserService;

        public FaqsService(ApplicationDbContext context, MysqlDbContext mysqlContext, ICurrentUserService currentUserService)
        {
            _context = context;
            _mysqlContext = mysqlContext;
            _currentUserService = currentUserService;
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

        public async Task<Result> CreateOrUpdateGeneralQuestion(GeneralQuestion request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Question))
                {
                    return Result.Failure("Failed", "500", new[] { "Question is required!" }, null);
                }

                var normalizedQuestion = request.Question.Replace(" ", "").ToLower();

                if (request.Id > 0)
                {
                    var generalQuestionObj = await _context.GeneralQuestions.FindAsync(request.Id);

                    if (generalQuestionObj == null)
                    {
                        return Result.Failure("Failed", "404", new[] { "Resource not found!" }, null);
                    }

                    var existingQuestion = await _context.GeneralQuestions
                        .FirstOrDefaultAsync(c => c.Question.Replace(" ", "").ToLower() == normalizedQuestion && c.Id != request.Id);

                    if (existingQuestion != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Question already exists!" }, null);
                    }

                    // Update existing GeneralQuestion object
                    generalQuestionObj.Question = request.Question;
                    generalQuestionObj.Answer = request.Answer;
                    generalQuestionObj.IsActive = request.IsActive;

                    _context.GeneralQuestions.Update(generalQuestionObj);

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                         ? Result.Success("Success", "200", new[] { "Updated Successfully" }, null)
                         : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
                }
                else
                {
                    var existingQuestion = await _context.GeneralQuestions
                        .FirstOrDefaultAsync(c => c.Question.Replace(" ", "").ToLower() == normalizedQuestion);

                    if (existingQuestion != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Duplicate data found. Question already exists." }, null);
                    }

                    await _context.GeneralQuestions.AddAsync(request);

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

        public async Task<PagedList<GeneralQuestionVM>> GetGeneralQuestions(GetGeneralQuestions request)
        {
            string conditionClause = " ";
            var queryBuilder = new StringBuilder();
            var parameter = new DynamicParameters();

            queryBuilder.AppendLine("SELECT general_question.*, count(*) over() as TotalItems FROM general_question ");

            if (request.Id != null)
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} id = @Id");
                conditionClause = " WHERE ";
                parameter.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(request.Question))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} question LIKE @Question");
                conditionClause = " WHERE ";
                parameter.Add("Question", $"%{request.Question}%", DbType.String, ParameterDirection.Input);
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
            var result = await _mysqlContext.GetPagedListAsync<GeneralQuestionVM>(request.CurrentPage, request.ItemsPerPage, query, parameter);
            return result;
        }

        public async Task<Result> CreateOrUpdateProductFaq(ProductFaq request)
        {
            try
            {
                var role = _currentUserService.RoleId;

                if (string.IsNullOrWhiteSpace(request.Question))
                {
                    return Result.Failure("Failed", "500", new[] { "Question is required!" }, null);
                }

                // Normalize question for comparison
                var normalizedQuestion = request.Question.Replace(" ", "").ToLower();

                if (request.Id > 0)
                {
                    // Update scenario
                    if (role != "Admin")
                    {
                        return Result.Failure("Failed", "403", new[] { "Only administrators can update FAQ entries." }, null);
                    }

                    var faqObj = await _context.ProductFaqs.FindAsync(request.Id);

                    if (faqObj == null)
                    {
                        return Result.Failure("Failed", "404", new[] { "FAQ entry not found!" }, null);
                    }

                    // Check for duplicate question, excluding current FAQ
                    var existingFaq = await _context.ProductFaqs
                        .FirstOrDefaultAsync(f => f.Question.Replace(" ", "").ToLower() == normalizedQuestion
                                                && f.Id != request.Id
                                                && f.ProductId == request.ProductId);

                    if (existingFaq != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Question already exists for this product!" }, null);
                    }

                    // Update existing FAQ
                    faqObj.ProductId = request.ProductId;
                    faqObj.CustomerName = request.CustomerName;
                    faqObj.CustomerEmail = request.CustomerEmail;
                    faqObj.Question = request.Question;
                    faqObj.Answer = request.Answer;
                    faqObj.IsActive = request.IsActive;

                    _context.ProductFaqs.Update(faqObj);

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                         ? Result.Success("Success", "200", new[] { "Updated Successfully" }, null)
                         : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
                }
                else
                {
                    // Create scenario
                    var existingFaq = await _context.ProductFaqs
                        .FirstOrDefaultAsync(f => f.Question.Replace(" ", "").ToLower() == normalizedQuestion
                                                && f.ProductId == request.ProductId);

                    if (existingFaq != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Question already exists for this product!" }, null);
                    }
                    request.Answer = String.Empty;
                    await _context.ProductFaqs.AddAsync(request);

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

        public async Task<PagedList<ProductFaqVM>> GetProductFaq(GetProductFaq request)
        {
            string conditionClause = " ";
            var queryBuilder = new StringBuilder();
            var parameter = new DynamicParameters();
            var role = _currentUserService.RoleId;

            queryBuilder.AppendLine("SELECT product_faq.*, count(*) over() as TotalItems FROM product_faq ");

            if (request.Id != null)
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} id = @Id");
                conditionClause = " WHERE ";
                parameter.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            }

            if (request.ProductId != null)
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} product_id = @ProductId");
                conditionClause = " WHERE ";
                parameter.Add("ProductId", request.ProductId, DbType.Int32, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} (customer_name LIKE @Keyword OR customer_email LIKE @Keyword OR question LIKE @Keyword OR answer LIKE @Keyword)");
                conditionClause = " WHERE ";
                parameter.Add("Keyword", $"%{request.Keyword}%", DbType.String, ParameterDirection.Input);
            }

            // Handle IsActive based on role
            if (role != "Admin")
            {
                // Non-admin users can only see active records
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} is_active = 'Y'");
                conditionClause = " WHERE ";
            }
            else if (!string.IsNullOrEmpty(request.IsActive))
            {
                // Admin can filter by IsActive if specified
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
            var result = await _mysqlContext.GetPagedListAsync<ProductFaqVM>(request.CurrentPage, request.ItemsPerPage, query, parameter);
            return result;
        }
    }
}
