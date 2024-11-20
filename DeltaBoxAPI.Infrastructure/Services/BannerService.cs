using Dapper;
using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Constants;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Banner;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Banner.Queries;
using DeltaboxAPI.Domain.Entities.DeltaBox.Banner;
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
    public class BannerService : IBannerService
    {
        private readonly ApplicationDbContext _context;
        private readonly MysqlDbContext _mysqlContext;
        private readonly IHostEnvironment _hostEnvironment;

        public BannerService(ApplicationDbContext context, MysqlDbContext mysqlContext, IHostEnvironment hostEnvironment)
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

        public async Task<Result> CreateOrUpdateBranner(AdsBanner request)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return Result.Failure("Failed", "500", new[] { "Name is required!" }, null);
                }

                var normalizedName = request.Name.Replace(" ", "").ToLower();

                // Handle image upload if new image is provided
                if (!ImageDirectory.IsFileExists(_hostEnvironment, request.Image))
                {
                    var img = await Helper.SaveSingleImage(request.Image, PathConstant.BANNER_IMAGE_PATH, _hostEnvironment);
                    if (!img.Succeed)
                    {
                        return Result.Failure("Failed", "500", new[] { "Banner image not saved. Please try again!" }, null);
                    }
                    request.Image = img.Status;
                }

                // Update existing banner
                if (request.Id > 0)
                {
                    var existingBanner = await _context.AdsBanners.FindAsync(request.Id);

                    if (existingBanner == null)
                    {
                        return Result.Failure("Failed", "404", new[] { "Banner not found!" }, null);
                    }

                    // Check for duplicate name excluding current banner
                    var duplicateBanner = await _context.AdsBanners
                        .FirstOrDefaultAsync(b => b.Name.Replace(" ", "").ToLower() == normalizedName && b.Id != request.Id);

                    if (duplicateBanner != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Name already exists!" }, null);
                    }

                    // Update banner properties
                    existingBanner.Name = request.Name;
                    existingBanner.Image = request.Image;
                    existingBanner.Section = request.Section;
                    existingBanner.Type = request.Type;
                    existingBanner.IsActive = request.IsActive;

                    _context.AdsBanners.Update(existingBanner);

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                        ? Result.Success("Success", "200", new[] { "Updated Successfully" }, null)
                        : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
                }
                // Create new banner
                else
                {
                    // Check for duplicate name
                    var duplicateBanner = await _context.AdsBanners
                        .FirstOrDefaultAsync(b => b.Name.Replace(" ", "").ToLower() == normalizedName);

                    if (duplicateBanner != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Duplicate data found. Name already exists." }, null);
                    }

                    await _context.AdsBanners.AddAsync(request);

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

        public async Task<PagedList<AdsBannerVM>> GetBanner(GetBanner request)
        {
            string conditionClause = " ";
            var queryBuilder = new StringBuilder();
            var parameter = new DynamicParameters();

            queryBuilder.AppendLine("SELECT ads_banner.*, count(*) over() as TotalItems FROM ads_banner ");

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

            if (!string.IsNullOrEmpty(request.Section))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} section = @Section");
                conditionClause = " WHERE ";
                parameter.Add("Section", request.Section, DbType.String, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(request.Type))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} type = @Type");
                conditionClause = " WHERE ";
                parameter.Add("Type", request.Type, DbType.String, ParameterDirection.Input);
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
            var result = await _mysqlContext.GetPagedListAsync<AdsBannerVM>(request.CurrentPage, request.ItemsPerPage, query, parameter);
            return result;
        }
    }
}
