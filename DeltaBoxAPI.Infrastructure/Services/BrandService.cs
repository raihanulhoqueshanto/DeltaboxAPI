using DeltaboxAPI.Application.Constants;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Brand;
using DeltaboxAPI.Domain.Entities.DeltaBox.Brand;
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
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;
        private readonly MysqlDbContext _mysqlContext;
        private readonly IHostEnvironment _hostEnvironment;

        public BrandService(ApplicationDbContext context, MysqlDbContext mysqlContext, IHostEnvironment hostEnvironment)
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

        public async Task<Result> CreateOrUpdateBrand(AssociateBrand request)
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
                if (!string.IsNullOrEmpty(request.Image) && !ImageDirectory.IsFileExists(_hostEnvironment, request.Image))
                {
                    var img = await Helper.SaveSingleImage(request.Image, PathConstant.BRAND_IMAGE_PATH, _hostEnvironment);
                    if (!img.Succeed)
                    {
                        return Result.Failure("Failed", "500", new[] { "Brand image not saved. Please try again!" }, null);
                    }
                    request.Image = img.Status;
                }

                // Update existing brand
                if (request.Id > 0)
                {
                    var existingBrand = await _context.AssociateBrands.FindAsync(request.Id);

                    if (existingBrand == null)
                    {
                        return Result.Failure("Failed", "404", new[] { "Brand not found!" }, null);
                    }

                    // Check for duplicate name excluding current brand
                    var duplicateBrand = await _context.AssociateBrands
                        .FirstOrDefaultAsync(b => b.Name.Replace(" ", "").ToLower() == normalizedName && b.Id != request.Id);

                    if (duplicateBrand != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Name already exists!" }, null);
                    }

                    // Update brand properties
                    existingBrand.Name = request.Name;
                    existingBrand.Image = request.Image;
                    existingBrand.IsActive = request.IsActive;

                    _context.AssociateBrands.Update(existingBrand);

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                        ? Result.Success("Success", "200", new[] { "Updated Successfully" }, null)
                        : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
                }
                // Create new brand
                else
                {
                    // Check for duplicate name
                    var duplicateBrand = await _context.AssociateBrands
                        .FirstOrDefaultAsync(b => b.Name.Replace(" ", "").ToLower() == normalizedName);

                    if (duplicateBrand != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Duplicate data found. Name already exists." }, null);
                    }

                    await _context.AssociateBrands.AddAsync(request);

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
