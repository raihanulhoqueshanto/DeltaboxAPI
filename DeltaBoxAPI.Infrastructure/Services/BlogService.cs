using DeltaboxAPI.Application.Constants;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Blog;
using DeltaboxAPI.Domain.Entities.DeltaBox.Blog;
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
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _context;
        private readonly MysqlDbContext _mysqlContext;
        private readonly IHostEnvironment _hostEnvironment;

        public BlogService(ApplicationDbContext context, MysqlDbContext mysqlContext, IHostEnvironment hostEnvironment)
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

        public async Task<Result> CreateOrUpdateArticle(Article request)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return Result.Failure("Failed", "500", new[] { "Name is required!" }, null);
                }

                if (string.IsNullOrWhiteSpace(request.WriterName))
                {
                    return Result.Failure("Failed", "500", new[] { "Writer name is required!" }, null);
                }

                var normalizedName = request.Name.Replace(" ", "").ToLower();

                // Handle image upload if new image is provided
                if (!string.IsNullOrEmpty(request.Image) && !ImageDirectory.IsFileExists(_hostEnvironment, request.Image))
                {
                    var img = await Helper.SaveSingleImage(request.Image, PathConstant.BLOG_IMAGE_PATH, _hostEnvironment);
                    if (!img.Succeed)
                    {
                        return Result.Failure("Failed", "500", new[] { "Article image not saved. Please try again!" }, null);
                    }
                    request.Image = img.Status;
                }

                // Update existing article
                if (request.Id > 0)
                {
                    var existingArticle = await _context.Articles.FindAsync(request.Id);

                    if (existingArticle == null)
                    {
                        return Result.Failure("Failed", "404", new[] { "Article not found!" }, null);
                    }

                    // Check for duplicate name excluding current article
                    var duplicateArticle = await _context.Articles
                        .FirstOrDefaultAsync(a => a.Name.Replace(" ", "").ToLower() == normalizedName && a.Id != request.Id);

                    if (duplicateArticle != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Name already exists!" }, null);
                    }

                    // Update article properties
                    existingArticle.Name = request.Name;
                    existingArticle.WriterName = request.WriterName;
                    existingArticle.Image = request.Image;
                    existingArticle.Description = request.Description;
                    existingArticle.IsActive = request.IsActive;

                    _context.Articles.Update(existingArticle);

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                        ? Result.Success("Success", "200", new[] { "Updated Successfully" }, null)
                        : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
                }
                // Create new article
                else
                {
                    // Check for duplicate name
                    var duplicateArticle = await _context.Articles
                        .FirstOrDefaultAsync(a => a.Name.Replace(" ", "").ToLower() == normalizedName);

                    if (duplicateArticle != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Duplicate data found. Name already exists." }, null);
                    }

                    await _context.Articles.AddAsync(request);

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
