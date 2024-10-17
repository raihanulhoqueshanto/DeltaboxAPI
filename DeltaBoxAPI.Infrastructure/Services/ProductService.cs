using Dapper;
using DeltaBoxAPI.Application.Common.Models;
using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Constants;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product.Queries;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product;
using DeltaboxAPI.Domain.Entities.DeltaBox.Product;
using DeltaBoxAPI.Infrastructure.Data;
using DeltaboxAPI.Infrastructure.Utils;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeltaboxAPI.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly MysqlDbContext _mysqlContext;
        private readonly IHostEnvironment _hostEnvironment;

        public ProductService(ApplicationDbContext context, MysqlDbContext mysqlContext, IHostEnvironment hostEnvironment)
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

        public async Task<Result> CreateOrUpdateProductCategory(ProductCategory request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return Result.Failure("Failed", "500", new[] { "Name is required!" }, null);
                }
                var normalizedName = request.Name.Replace(" ", "").ToLower();

                if (!ImageDirectory.IsFileExists(_hostEnvironment, request.Image))
                {
                    var img = Helper.SaveSingleImage(request.Image, PathConstant.PRODUCT_CATEGORY_PATH, _hostEnvironment);
                    if (!img.Result.Succeed)
                    {
                        return Result.Failure("Failed", "500", new[] { "Product category image not saved. Please try again!" }, null);
                    }
                    else
                    {
                        request.Image = img.Result.Status;
                    }
                }

                if (request.Id > 0)
                {
                    var productCategoryObj = await _context.ProductCategories.FindAsync(request.Id);

                    if (productCategoryObj == null)
                    {
                        return Result.Failure("Failed", "404", new[] { "Resource not found!" }, null);
                    }

                    var existingCategory = await _context.ProductCategories.FirstOrDefaultAsync(c => c.Name.Replace(" ", "").ToLower() == normalizedName && c.Id != request.Id);

                    if (existingCategory != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Name already exists!" }, null);
                    }

                    // Update existing ProductCategory object
                    productCategoryObj.Name = request.Name;
                    productCategoryObj.Image = request.Image;
                    productCategoryObj.IsPopular = request.IsPopular;
                    productCategoryObj.IsActive = request.IsActive;

                    _context.ProductCategories.Update(productCategoryObj);

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                         ? Result.Success("Success", "200", new[] { "Updated Successfully" }, null)
                         : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
                }
                else
                {
                    var existingCategory = await _context.ProductCategories.FirstOrDefaultAsync(c => c.Name.Replace(" ", "").ToLower() == normalizedName);

                    if (existingCategory != null)
                    {
                        return Result.Failure("Failed", "409", new[] { "Duplicate data found. Name already exists." }, null);
                    }

                    await _context.ProductCategories.AddAsync(request);

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

        public async Task<PagedList<ProductCategoryVM>> GetProductCategory(GetProductCategory request)
        {
            string conditionClause = " ";
            var queryBuilder = new StringBuilder();
            var parameter = new DynamicParameters();

            queryBuilder.AppendLine("SELECT product_category.*, count(*) over() as TotalItems FROM product_category ");

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

            if (!string.IsNullOrEmpty(request.IsPopular))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} is_popular = @IsPopular");
                conditionClause = " WHERE ";
                parameter.Add("IsPopular", request.IsPopular, DbType.String, ParameterDirection.Input);
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
            var result = await _mysqlContext.GetPagedListAsync<ProductCategoryVM>(request.CurrentPage, request.ItemsPerPage, query, parameter);
            return result;
        }

        public async Task<Result> CreateOrUpdateProduct(CreateOrUpdateProductRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return Result.Failure("Failed", "500", new[] { "Product name is required!" }, null);
                }

                if (!ImageDirectory.IsFileExists(_hostEnvironment, request.ThumbnailImage))
                {
                    var img = await Helper.SaveSingleImage(request.ThumbnailImage, PathConstant.PRODUCT_THUMBNAIL_IMAGE_PATH, _hostEnvironment);
                    if (!img.Succeed)
                    {
                        return Result.Failure("Failed", "500", new[] { "Product thumbnail image not saved. Please try again!" }, null);
                    }
                    else
                    {
                        request.ThumbnailImage = img.Status;
                    }
                }

                ProductProfile productProfile;

                if (request.Id.HasValue && request.Id.Value > 0)
                {
                    // Update existing product
                    productProfile = await _context.ProductProfiles.FindAsync(request.Id.Value);

                    if (productProfile == null)
                    {
                        return Result.Failure("Failed", "404", new[] { "Product not found!" }, null);
                    }

                    // Update ProductProfile
                    productProfile.Name = request.Name;
                    productProfile.CategoryId = request.CategoryId;
                    productProfile.Description = request.Description;
                    productProfile.ThumbnailImage = request.ThumbnailImage;
                    productProfile.IsActive = request.IsActive;

                    _context.ProductProfiles.Update(productProfile);
                }
                else
                {
                    // Create new product
                    productProfile = new ProductProfile
                    {
                        Name = request.Name,
                        CategoryId = request.CategoryId,
                        Description = request.Description,
                        ThumbnailImage = request.ThumbnailImage,
                        IsActive = request.IsActive
                    };

                    await _context.ProductProfiles.AddAsync(productProfile);
                }

                await _context.SaveChangesAsync(); // Save to get the ProductProfile ID if it's a new product

                // Process variant groups
                foreach (var group in request.ColorGroups)
                {
                    await ProcessVariantGroup(productProfile.Id, group);
                }

                // Remove variants that are not in the updated list
                var allVariantIds = request.ColorGroups
                    .SelectMany(g => g.Variants)
                    .Where(v => v.Id.HasValue)
                    .Select(v => v.Id.Value)
                    .ToList();

                var variantsToRemove = await _context.ProductVariants
                    .Where(v => v.ProductId == productProfile.Id && !allVariantIds.Contains(v.Id))
                    .ToListAsync();

                _context.ProductVariants.RemoveRange(variantsToRemove);

                await _context.SaveChangesAsync();

                return Result.Success("Success", "200", new[] { "Product saved successfully" }, null);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Result.Failure("Failed", "500", new[] { errorMessage }, null);
            }
        }

        private async Task ProcessVariantGroup(int productId, ColorwiseVariant colorwiseVariant)
        {
            foreach (var variantRequest in colorwiseVariant.Variants)
            {
                await CreateOrUpdateProductVariant(productId, variantRequest, colorwiseVariant.ColorName, colorwiseVariant.Images);
            }
        }

        private async Task CreateOrUpdateProductVariant(int productId, ProductVariantRequest variantRequest, string colorName, List<string>? images)
        {
            ProductVariant variant;

            if (variantRequest.Id.HasValue && variantRequest.Id.Value > 0)
            {
                variant = await _context.ProductVariants.FindAsync(variantRequest.Id.Value);

                if (variant == null)
                {
                    throw new Exception($"Variant with ID {variantRequest.Id.Value} not found.");
                }

                // Update existing variant
                UpdateVariantProperties(variant, variantRequest);
                _context.ProductVariants.Update(variant);
            }
            else
            {
                // Create new variant
                variant = new ProductVariant
                {
                    ProductId = productId
                };
                UpdateVariantProperties(variant, variantRequest);

                await _context.ProductVariants.AddAsync(variant);
            }

            await _context.SaveChangesAsync(); // Save to get the Variant ID if it's a new variant

            // Handle Images
            await HandleProductImages(colorName, images);

            // Handle Attributes
            await HandleProductAttributes(variant.Id, variantRequest.Attributes);
        }

        private void UpdateVariantProperties(ProductVariant variant, ProductVariantRequest request)
        {
            variant.Name = request.Name;
            variant.SKU = request.SKU;
            variant.DpPrice = request.DpPrice;
            variant.Price = request.Price;
            variant.StockQuantity = request.StockQuantity;
            variant.DiscountAmount = request.DiscountAmount;
            variant.DiscountStartDate = request.DiscountStartDate;
            variant.DiscountEndDate = request.DiscountEndDate;
            variant.IsActive = request.IsActive;
        }

        private async Task HandleProductImages(string colorName, List<string>? imageRequests)
        {
            if (imageRequests == null || !imageRequests.Any())
            {
                // If no images are provided, we might want to remove any existing images for this variant
                var existedImages = await _context.ProductImages.Where(c => c.ColorName.Equals(colorName.Trim())).ToListAsync();

                _context.ProductImages.RemoveRange(existedImages);
                return;
            }

            var existingImages = await _context.ProductImages.Where(c => c.ColorName.Equals(colorName.Trim())).ToListAsync();

            foreach (var imageRequest in imageRequests)
            {
                var existingImage = existingImages.FirstOrDefault(c => c.Image == imageRequest);
                if (existingImage != null)
                {
                    // Image already exists, no need to update
                    existingImages.Remove(existingImage);
                }
                else
                {
                    // Save the new image to the designated folder
                    var img = await Helper.SaveSingleImage(imageRequest, PathConstant.PRODUCT_IMAGE_PATH, _hostEnvironment);
                    if (!img.Succeed)
                    {
                        throw new Exception("Product image not saved. Please try again.");
                    }
                    else
                    {
                        // Create new image with saved path
                        var newImage = new ProductImage
                        {
                            ColorName = colorName,
                            Image = img.Status, // Path of the saved image
                            IsActive = "Y"
                        };
                        await _context.ProductImages.AddAsync(newImage);
                    }
                }
            }

            // Remove images that are not in the updated list
            _context.ProductImages.RemoveRange(existingImages);
        }

        private async Task HandleProductAttributes(int variantId, List<ProductAttributeRequest> attributeRequests)
        {
            if (attributeRequests == null)
            {
                return;
            }
            
            var existingAttributes = await _context.ProductAttributes.Where(a => a.VariantId == variantId).ToListAsync();

            foreach (var attributeRequest in attributeRequests)
            {
                ProductAttribute attribute;

                if (attributeRequest.Id.HasValue && attributeRequest.Id.Value > 0)
                {
                    attribute = existingAttributes.FirstOrDefault(a => a.Id == attributeRequest.Id.Value);
                    if (attribute != null)
                    {
                        // Update existing attribute
                        attribute.AttributeName = attributeRequest.AttributeName;
                        attribute.AttributeValue = attributeRequest.AttributeValue;
                        attribute.IsActive = attributeRequest.IsActive;
                        _context.ProductAttributes.Update(attribute);
                        existingAttributes.Remove(attribute);
                    }
                }
                else
                {
                    // Create new attribute
                    attribute = new ProductAttribute
                    {
                        VariantId = variantId,
                        AttributeName = attributeRequest.AttributeName,
                        AttributeValue = attributeRequest.AttributeValue,
                        IsActive = attributeRequest.IsActive
                    };
                    await _context.ProductAttributes.AddAsync(attribute);
                }
            }

            // Remove attributes that are not in the updated list
            _context.ProductAttributes.RemoveRange(existingAttributes);
        }
    }
}