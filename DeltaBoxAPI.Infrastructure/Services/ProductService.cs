﻿using Dapper;
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

            queryBuilder.AppendLine("SELECT product_category.*, LOWER(REPLACE(REPLACE(REPLACE(product_category.name, ' ', '-'), '&', 'and'), ',', '')) AS Slug, count(*) over() as TotalItems FROM product_category ");

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
        //First product upload version
        //public async Task<Result> CreateOrUpdateProduct(CreateOrUpdateProductRequest request)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(request.Name))
        //        {
        //            return Result.Failure("Failed", "500", new[] { "Product name is required!" }, null);
        //        }

        //        if (!ImageDirectory.IsFileExists(_hostEnvironment, request.ThumbnailImage))
        //        {
        //            var img = await Helper.SaveSingleImage(request.ThumbnailImage, PathConstant.PRODUCT_THUMBNAIL_IMAGE_PATH, _hostEnvironment);
        //            if (!img.Succeed)
        //            {
        //                return Result.Failure("Failed", "500", new[] { "Product thumbnail image not saved. Please try again!" }, null);
        //            }
        //            else
        //            {
        //                request.ThumbnailImage = img.Status;
        //            }
        //        }

        //        ProductProfile productProfile;

        //        if (request.Id.HasValue && request.Id.Value > 0)
        //        {
        //            // Update existing product
        //            productProfile = await _context.ProductProfiles.FindAsync(request.Id.Value);

        //            if (productProfile == null)
        //            {
        //                return Result.Failure("Failed", "404", new[] { "Product not found!" }, null);
        //            }

        //            // Update ProductProfile
        //            productProfile.Name = request.Name;
        //            productProfile.CategoryId = request.CategoryId;
        //            productProfile.Description = request.Description;
        //            productProfile.ThumbnailImage = request.ThumbnailImage;
        //            productProfile.IsActive = request.IsActive;

        //            _context.ProductProfiles.Update(productProfile);
        //        }
        //        else
        //        {
        //            // Create new product
        //            productProfile = new ProductProfile
        //            {
        //                Name = request.Name,
        //                CategoryId = request.CategoryId,
        //                Description = request.Description,
        //                ThumbnailImage = request.ThumbnailImage,
        //                IsActive = request.IsActive
        //            };

        //            await _context.ProductProfiles.AddAsync(productProfile);
        //        }

        //        await _context.SaveChangesAsync(); // Save to get the ProductProfile ID if it's a new product

        //        // Process variant groups
        //        foreach (var group in request.ColorGroups)
        //        {
        //            await ProcessVariantGroup(productProfile.Id, group);
        //        }

        //        // Remove variants that are not in the updated list
        //        var allVariantIds = request.ColorGroups
        //            .SelectMany(g => g.Variants)
        //            .Where(v => v.Id.HasValue)
        //            .Select(v => v.Id.Value)
        //            .ToList();

        //        var variantsToRemove = await _context.ProductVariants
        //            .Where(v => v.ProductId == productProfile.Id && !allVariantIds.Contains(v.Id))
        //            .ToListAsync();

        //        _context.ProductVariants.RemoveRange(variantsToRemove);

        //        await _context.SaveChangesAsync();

        //        return Result.Success("Success", "200", new[] { "Product saved successfully" }, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorMessage = ex.InnerException?.Message ?? ex.Message;
        //        return Result.Failure("Failed", "500", new[] { errorMessage }, null);
        //    }
        //}

        //private async Task ProcessVariantGroup(int productId, ColorwiseVariant colorwiseVariant)
        //{
        //    foreach (var variantRequest in colorwiseVariant.Variants)
        //    {
        //        await CreateOrUpdateProductVariant(productId, variantRequest, colorwiseVariant.ColorName, colorwiseVariant.Images);
        //    }
        //}

        //private async Task CreateOrUpdateProductVariant(int productId, ProductVariantRequest variantRequest, string colorName, List<string>? images)
        //{
        //    ProductVariant variant;

        //    if (variantRequest.Id.HasValue && variantRequest.Id.Value > 0)
        //    {
        //        variant = await _context.ProductVariants.FindAsync(variantRequest.Id.Value);

        //        if (variant == null)
        //        {
        //            throw new Exception($"Variant with ID {variantRequest.Id.Value} not found.");
        //        }

        //        // Update existing variant
        //        UpdateVariantProperties(variant, variantRequest);
        //        _context.ProductVariants.Update(variant);
        //    }
        //    else
        //    {
        //        // Create new variant
        //        variant = new ProductVariant
        //        {
        //            ProductId = productId
        //        };
        //        UpdateVariantProperties(variant, variantRequest);

        //        await _context.ProductVariants.AddAsync(variant);
        //    }

        //    await _context.SaveChangesAsync(); // Save to get the Variant ID if it's a new variant

        //    // Handle Images
        //    await HandleProductImages(colorName, images);

        //    // Handle Attributes
        //    await HandleProductAttributes(variant.Id, variantRequest.Attributes);
        //}

        //private void UpdateVariantProperties(ProductVariant variant, ProductVariantRequest request)
        //{
        //    variant.Name = request.Name;
        //    variant.SKU = request.SKU;
        //    variant.DpPrice = request.DpPrice;
        //    variant.Price = request.Price;
        //    variant.StockQuantity = request.StockQuantity;
        //    variant.DiscountAmount = request.DiscountAmount;
        //    variant.DiscountStartDate = request.DiscountStartDate;
        //    variant.DiscountEndDate = request.DiscountEndDate;
        //    variant.IsActive = request.IsActive;
        //}

        //private async Task HandleProductImages(string colorName, List<string>? imageRequests)
        //{
        //    if (imageRequests == null || !imageRequests.Any())
        //    {
        //        // If no images are provided, we might want to remove any existing images for this variant
        //        var existedImages = await _context.ProductImages.Where(c => c.ColorName.Equals(colorName.Trim())).ToListAsync();

        //        _context.ProductImages.RemoveRange(existedImages);
        //        return;
        //    }

        //    var existingImages = await _context.ProductImages.Where(c => c.ColorName.Equals(colorName.Trim())).ToListAsync();

        //    foreach (var imageRequest in imageRequests)
        //    {
        //        var existingImage = existingImages.FirstOrDefault(c => c.Image == imageRequest);
        //        if (existingImage != null)
        //        {
        //            // Image already exists, no need to update
        //            existingImages.Remove(existingImage);
        //        }
        //        else
        //        {
        //            // Save the new image to the designated folder
        //            var img = await Helper.SaveSingleImage(imageRequest, PathConstant.PRODUCT_IMAGE_PATH, _hostEnvironment);
        //            if (!img.Succeed)
        //            {
        //                throw new Exception("Product image not saved. Please try again.");
        //            }
        //            else
        //            {
        //                // Create new image with saved path
        //                var newImage = new ProductImage
        //                {
        //                    ColorName = colorName,
        //                    Image = img.Status, // Path of the saved image
        //                    IsActive = "Y"
        //                };
        //                await _context.ProductImages.AddAsync(newImage);
        //            }
        //        }
        //    }

        //    // Remove images that are not in the updated list
        //    _context.ProductImages.RemoveRange(existingImages);
        //}

        //private async Task HandleProductAttributes(int variantId, List<ProductAttributeRequest> attributeRequests)
        //{
        //    if (attributeRequests == null)
        //    {
        //        return;
        //    }

        //    var existingAttributes = await _context.ProductAttributes.Where(a => a.VariantId == variantId).ToListAsync();

        //    foreach (var attributeRequest in attributeRequests)
        //    {
        //        ProductAttribute attribute;

        //        if (attributeRequest.Id.HasValue && attributeRequest.Id.Value > 0)
        //        {
        //            attribute = existingAttributes.FirstOrDefault(a => a.Id == attributeRequest.Id.Value);
        //            if (attribute != null)
        //            {
        //                // Update existing attribute
        //                attribute.AttributeName = attributeRequest.AttributeName;
        //                attribute.AttributeValue = attributeRequest.AttributeValue;
        //                attribute.IsActive = attributeRequest.IsActive;
        //                _context.ProductAttributes.Update(attribute);
        //                existingAttributes.Remove(attribute);
        //            }
        //        }
        //        else
        //        {
        //            // Create new attribute
        //            attribute = new ProductAttribute
        //            {
        //                VariantId = variantId,
        //                AttributeName = attributeRequest.AttributeName,
        //                AttributeValue = attributeRequest.AttributeValue,
        //                IsActive = attributeRequest.IsActive
        //            };
        //            await _context.ProductAttributes.AddAsync(attribute);
        //        }
        //    }

        //    // Remove attributes that are not in the updated list
        //    _context.ProductAttributes.RemoveRange(existingAttributes);
        //}

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
                    productProfile.ShortDescription = request.ShortDescription;
                    productProfile.Description = request.Description;
                    productProfile.ThumbnailImage = request.ThumbnailImage;
                    productProfile.IsActive = request.IsActive;
                    productProfile.LatestOffer = request.LatestOffer;

                    _context.ProductProfiles.Update(productProfile);
                }
                else
                {
                    // Create new product
                    productProfile = new ProductProfile
                    {
                        Name = request.Name,
                        CategoryId = request.CategoryId,
                        ShortDescription = request.ShortDescription,
                        Description = request.Description,
                        ThumbnailImage = request.ThumbnailImage,
                        IsActive = request.IsActive,
                        LatestOffer = request.LatestOffer
                    };

                    await _context.ProductProfiles.AddAsync(productProfile);
                }

                await _context.SaveChangesAsync(); // Save to get the ProductProfile ID if it's a new product

                // Process variant groups
                var updatedVariantIds = new List<int>();
                foreach (var group in request.ColorGroups)
                {
                    var groupVariantIds = await ProcessVariantGroup(productProfile.Id, group);
                    updatedVariantIds.AddRange(groupVariantIds);
                }

                // Remove variants that are no longer associated with the product
                var variantsToRemove = await _context.ProductVariants
                    .Where(v => v.ProductId == productProfile.Id && !updatedVariantIds.Contains(v.Id))
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

        private async Task<List<int>> ProcessVariantGroup(int productId, ColorwiseVariant colorwiseVariant)
        {
            var variantIds = new List<int>();
            foreach (var variantRequest in colorwiseVariant.Variants)
            {
                var variantId = await CreateOrUpdateProductVariant(productId, variantRequest, colorwiseVariant.ColorName, colorwiseVariant.Images);
                variantIds.Add(variantId);
            }
            return variantIds;
        }

        private async Task<int> CreateOrUpdateProductVariant(int productId, ProductVariantRequest variantRequest, string colorName, List<string>? images)
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
            await HandleProductImages(productId, colorName, images);

            // Handle Attributes
            await HandleProductAttributes(variant.Id, variantRequest.Attributes);

            return variant.Id;
        }

        private void UpdateVariantProperties(ProductVariant variant, ProductVariantRequest request)
        {
            variant.Name = request.Name;
            variant.CategoryId = request.CategoryId;
            variant.SKU = request.SKU;
            variant.DpPrice = request.DpPrice;
            variant.Price = request.Price;
            variant.StockQuantity = request.StockQuantity;
            variant.DiscountAmount = request.DiscountAmount;
            variant.DiscountStartDate = request.DiscountStartDate;
            variant.DiscountEndDate = request.DiscountEndDate;
            variant.IsActive = request.IsActive;
        }

        private async Task HandleProductImages(int productId, string colorName, List<string>? imageRequests)
        {
            if (imageRequests == null || !imageRequests.Any())
            {
                // If no images are provided, we might want to remove any existing images for this variant
                var existedImages = await _context.ProductImages.Where(c => c.ColorName.Equals(colorName.Trim()) && c.ProductId == productId).ToListAsync();

                _context.ProductImages.RemoveRange(existedImages);
                return;
            }

            var existingImages = await _context.ProductImages.Where(c => c.ColorName.Equals(colorName.Trim()) && c.ProductId == productId).ToListAsync();

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
                            ProductId = productId,
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

        public async Task<PagedList<ProductVM>> GetProduct(GetProduct request)
        {
            string conditionClause = " ";
            var queryBuilder = new StringBuilder();
            var parameter = new DynamicParameters();

            queryBuilder.AppendLine(@"SELECT p.*, pc.name as category_name, LOWER(REPLACE(REPLACE(REPLACE(p.name, ' ', '-'), '&', 'and'), ',', '')) AS Slug, COUNT(*) OVER() as TotalItems FROM product_profile p LEFT JOIN product_category pc ON p.category_id = pc.id");

            if (request.Id != null)
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} p.id = @Id");
                conditionClause = " WHERE ";
                parameter.Add("Id", request.Id, DbType.Int32, ParameterDirection.Input);
            }

            if (request.CategoryId != null)
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} p.category_id = @CategoryId");
                conditionClause = " WHERE ";
                parameter.Add("CategoryId", request.CategoryId, DbType.Int32, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} p.name LIKE @Name");
                conditionClause = " WHERE ";
                parameter.Add("Name", $"%{request.Name}%", DbType.String, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(request.IsActive))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} p.is_active = @IsActive");
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
            var products = await _mysqlContext.GetPagedListAsync<ProductVM>(request.CurrentPage, request.ItemsPerPage, query, parameter);

            // Load related data for each product
            foreach (var product in products)
            {
                // Get color groups with images
                var colorGroups = await _context.ProductImages
                    .Where(pi => pi.ColorName != null && pi.ProductId == product.Id)
                    .GroupBy(pi => pi.ColorName)
                    .Select(g => new ColorwiseVariantVM
                    {
                        ColorName = g.Key,
                        Images = g.Select(pi => pi.Image).ToList(),
                        Variants = new List<ProductVariantVM>()
                    })
                    .ToListAsync();

                // Get variants
                var variants = await _context.ProductVariants
                    .Where(v => v.ProductId == product.Id)
                    .Select(v => new ProductVariantVM
                    {
                        Id = v.Id,
                        Name = v.Name,
                        CategoryId = v.CategoryId,
                        SKU = v.SKU,
                        DpPrice = v.DpPrice,
                        Price = v.Price,
                        StockQuantity = v.StockQuantity,
                        DiscountAmount = v.DiscountAmount,
                        DiscountStartDate = v.DiscountStartDate,
                        DiscountEndDate = v.DiscountEndDate,
                        IsActive = v.IsActive,
                        Attributes = _context.ProductAttributes
                            .Where(a => a.VariantId == v.Id)
                            .Select(a => new ProductAttributeVM
                            {
                                Id = a.Id,
                                AttributeName = a.AttributeName,
                                AttributeValue = a.AttributeValue,
                                IsActive = a.IsActive
                            })
                            .ToList()
                    })
                    .ToListAsync();

                // Group variants by color
                foreach (var variant in variants)
                {
                    var colorAttribute = variant.Attributes.FirstOrDefault(a => a.AttributeName.ToLower() == "color");
                    if (colorAttribute != null)
                    {
                        var colorGroup = colorGroups.FirstOrDefault(cg => cg.ColorName.ToLower() == colorAttribute.AttributeValue.ToLower());
                        if (colorGroup != null)
                        {
                            colorGroup.Variants.Add(variant);
                        }
                    }
                }

                product.ColorGroups = colorGroups;
            }

            return products;
        }

        public async Task<PagedList<FilterProductVM>> GetFilterProducts(GetFilterProducts request)
        {
            string conditionClause = "";
            var queryBuilder = new StringBuilder();
            var parameter = new DynamicParameters();
            var currentDate = DateTime.Now;

            queryBuilder.AppendLine("WITH RankedVariants AS (");
            queryBuilder.AppendLine("    SELECT ");
            queryBuilder.AppendLine("        pp.id AS ProductId, ");
            queryBuilder.AppendLine("        pp.name AS Name, ");
            queryBuilder.AppendLine("        LOWER(REPLACE(REPLACE(REPLACE(pp.name, ' ', '-'), '&', 'and'), ',', '')) AS Slug, ");
            queryBuilder.AppendLine("        pp.thumbnail_image AS ThumbnailImage, ");
            queryBuilder.AppendLine("        pv.sku AS Sku, ");
            queryBuilder.AppendLine("        pv.price AS Price, ");
            queryBuilder.AppendLine("        CASE WHEN @CurrentDate BETWEEN pv.discount_start_date AND pv.discount_end_date ");
            queryBuilder.AppendLine("             THEN pv.price - pv.discount_amount ");
            queryBuilder.AppendLine("             ELSE pv.price END AS FinalPrice, ");
            queryBuilder.AppendLine("        CASE WHEN @CurrentDate BETWEEN pv.discount_start_date AND pv.discount_end_date ");
            queryBuilder.AppendLine("             THEN pv.discount_amount ");
            queryBuilder.AppendLine("             ELSE 0 END AS DiscountAmount, ");
            queryBuilder.AppendLine("        pv.stock_quantity AS StockQuantity, ");
            queryBuilder.AppendLine("        ROW_NUMBER() OVER (PARTITION BY pp.id ORDER BY pv.price ASC) AS VariantRank ");
            queryBuilder.AppendLine("    FROM product_profile pp ");
            queryBuilder.AppendLine("    LEFT JOIN product_variant pv ON pp.id = pv.product_id ");
            queryBuilder.AppendLine("    LEFT JOIN product_category pc ON pp.category_id = pc.id ");
            queryBuilder.AppendLine("    LEFT JOIN product_attribute pa ON pv.id = pa.variant_id ");

            // Add current date parameter first
            parameter.Add("CurrentDate", currentDate, DbType.DateTime);

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} (pp.name LIKE @Keyword OR pp.description LIKE @Keyword OR pv.sku LIKE @Keyword OR pc.name LIKE @Keyword)");
                conditionClause = "WHERE";
                parameter.Add("Keyword", $"%{request.Keyword}%", DbType.String);
            }

            if (request.Id.HasValue)
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pp.id = @Id");
                conditionClause = "WHERE";
                parameter.Add("Id", request.Id.Value, DbType.Int32);
            }

            if (!string.IsNullOrEmpty(request.CategoryId))
            {
                var categoryIds = request.CategoryId.Split(',')
                                                  .Select((v, i) => new { Value = v.Trim(), ParamName = $"@CategoryId{i}" })
                                                  .ToList();

                var inClause = string.Join(", ", categoryIds.Select(c => c.ParamName));
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pp.category_id IN ({inClause})");
                conditionClause = "WHERE";

                foreach (var categoryId in categoryIds)
                {
                    parameter.Add(categoryId.ParamName, int.Parse(categoryId.Value), DbType.Int32);
                }
            }

            if (!string.IsNullOrEmpty(request.LatestOffer))
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pp.latest_offer = @LatestOffer");
                conditionClause = " WHERE ";
                parameter.Add("LatestOffer", request.LatestOffer, DbType.String, ParameterDirection.Input);
            }

            if (request.MinPrice.HasValue && request.MaxPrice.HasValue)
            {
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pv.price BETWEEN @MinPrice AND @MaxPrice");
                conditionClause = "WHERE";
                parameter.Add("MinPrice", request.MinPrice.Value, DbType.Decimal);
                parameter.Add("MaxPrice", request.MaxPrice.Value, DbType.Decimal);
            }

            if (!string.IsNullOrEmpty(request.Stock))
            {
                if (request.Stock.ToLower() == "instock")
                {
                    queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pv.stock_quantity > 0");
                    conditionClause = "WHERE";
                }
                else if (request.Stock.ToLower() == "outofstock")
                {
                    queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pv.stock_quantity <= 0");
                    conditionClause = "WHERE";
                }
            }

            // Handle both plan and duration values together
            if (!string.IsNullOrEmpty(request.Plan) || !string.IsNullOrEmpty(request.Duration))
            {
                var attributeConditions = new List<string>();

                // Handle plan values
                if (!string.IsNullOrEmpty(request.Plan))
                {
                    var planValues = request.Plan.Split(',')
                                               .Select((v, i) => new {
                                                   Value = v.Replace(" ", "").ToLower(),
                                                   ParamName = $"@Plan{i}"
                                               })
                                               .ToList();

                    var planInClause = string.Join(", ", planValues.Select(p => p.ParamName));
                    attributeConditions.Add($"LOWER(REPLACE(pa.attribute_value, ' ', '')) IN ({planInClause})");

                    foreach (var planValue in planValues)
                    {
                        parameter.Add(planValue.ParamName, planValue.Value, DbType.String);
                    }
                }

                // Handle duration values
                if (!string.IsNullOrEmpty(request.Duration))
                {
                    var durationValues = request.Duration.Split(',')
                                                       .Select((v, i) => new {
                                                           Value = v.Replace(" ", "").ToLower(),
                                                           ParamName = $"@Duration{i}"
                                                       })
                                                       .ToList();

                    var durationInClause = string.Join(", ", durationValues.Select(d => d.ParamName));
                    attributeConditions.Add($"LOWER(REPLACE(pa.attribute_value, ' ', '')) IN ({durationInClause})");

                    foreach (var durationValue in durationValues)
                    {
                        parameter.Add(durationValue.ParamName, durationValue.Value, DbType.String);
                    }
                }

                // Combine conditions with OR
                queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} ({string.Join(" OR ", attributeConditions)})");
                conditionClause = "WHERE";
            }

            queryBuilder.AppendLine("), FinalProductList AS (");
            queryBuilder.AppendLine("    SELECT ");
            queryBuilder.AppendLine("        ProductId AS Id, ");
            queryBuilder.AppendLine("        MAX(Name) AS Name, ");
            queryBuilder.AppendLine("        MAX(Slug) AS Slug, ");
            queryBuilder.AppendLine("        MAX(ThumbnailImage) AS ThumbnailImage, ");
            queryBuilder.AppendLine("        MAX(Sku) AS Sku, ");
            queryBuilder.AppendLine("        MAX(Price) AS Price, ");
            queryBuilder.AppendLine("        MAX(FinalPrice) AS FinalPrice, ");
            queryBuilder.AppendLine("        MAX(DiscountAmount) AS DiscountAmount, ");
            queryBuilder.AppendLine("        CASE WHEN SUM(StockQuantity) > 0 THEN 'In Stock' ELSE 'Out of Stock' END AS StockStatus, ");
            queryBuilder.AppendLine("        COUNT(*) OVER() AS TotalItems ");
            queryBuilder.AppendLine("    FROM RankedVariants ");
            queryBuilder.AppendLine("    WHERE VariantRank = 1 ");
            queryBuilder.AppendLine("    GROUP BY ProductId ");

            // Sorting
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                switch (request.SortBy.ToUpper())
                {
                    case "HTOL":
                        queryBuilder.AppendLine("    ORDER BY MAX(Price) DESC ");
                        break;
                    case "LTOH":
                        queryBuilder.AppendLine("    ORDER BY MAX(Price) ASC ");
                        break;
                    case "ATOZ":
                        queryBuilder.AppendLine("    ORDER BY MAX(Name) ASC ");
                        break;
                    case "ZTOA":
                        queryBuilder.AppendLine("    ORDER BY MAX(Name) DESC ");
                        break;
                    default:
                        queryBuilder.AppendLine("    ORDER BY ProductId ");
                        break;
                }
            }
            else
            {
                queryBuilder.AppendLine("    ORDER BY ProductId ");
            }

            queryBuilder.AppendLine(")");

            // Pagination
            if (string.IsNullOrEmpty(request.GetAll) || request.GetAll.ToUpper() != "Y")
            {
                queryBuilder.AppendLine("SELECT * FROM (");
                queryBuilder.AppendLine("    SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum ");
                queryBuilder.AppendLine("    FROM FinalProductList");
                queryBuilder.AppendLine(") AS PagedResults ");
                queryBuilder.AppendLine("WHERE RowNum BETWEEN @Offset + 1 AND @Offset + @ItemsPerPage ");
            }
            else
            {
                queryBuilder.AppendLine("SELECT * FROM FinalProductList");
            }

            // Pagination parameters
            if (string.IsNullOrEmpty(request.GetAll) || request.GetAll.ToUpper() != "Y")
            {
                parameter.Add("Offset", (request.CurrentPage - 1) * request.ItemsPerPage, DbType.Int32);
                parameter.Add("ItemsPerPage", request.ItemsPerPage, DbType.Int32);
            }

            var query = queryBuilder.ToString();
            var result = await _mysqlContext.GetPagedListAsync<FilterProductVM>(
                request.CurrentPage,
                request.ItemsPerPage,
                query,
                parameter
            );

            return result;
        }

        // Previous One
        //public async Task<PagedList<FilterProductVM>> GetFilterProducts(GetFilterProducts request)
        //{
        //    string conditionClause = "";
        //    var queryBuilder = new StringBuilder();
        //    var parameter = new DynamicParameters();
        //    var currentDate = DateTime.Now;

        //    queryBuilder.AppendLine("SELECT pp.id AS Id, ");
        //    queryBuilder.AppendLine("pp.name AS Name, ");
        //    queryBuilder.AppendLine("LOWER(REPLACE(REPLACE(REPLACE(pp.name, ' ', '-'), '&', 'and'), ',', '')) AS Slug, ");
        //    queryBuilder.AppendLine("pp.thumbnail_image AS ThumbnailImage, ");
        //    queryBuilder.AppendLine("MIN(pv.price) AS Price, ");
        //    queryBuilder.AppendLine("MIN(CASE WHEN @CurrentDate BETWEEN pv.discount_start_date AND pv.discount_end_date ");
        //    queryBuilder.AppendLine("     THEN pv.price - pv.discount_amount ");
        //    queryBuilder.AppendLine("     ELSE pv.price END) AS FinalPrice, ");
        //    queryBuilder.AppendLine("MIN(CASE WHEN @CurrentDate BETWEEN pv.discount_start_date AND pv.discount_end_date ");
        //    queryBuilder.AppendLine("     THEN pv.discount_amount ");
        //    queryBuilder.AppendLine("     ELSE 0 END) AS DiscountAmount, ");
        //    queryBuilder.AppendLine("CASE WHEN SUM(pv.stock_quantity) > 0 THEN 'In Stock' ELSE 'Out of Stock' END AS StockStatus, ");
        //    queryBuilder.AppendLine("COUNT(*) OVER() AS TotalItems ");
        //    queryBuilder.AppendLine("FROM product_profile pp ");
        //    queryBuilder.AppendLine("LEFT JOIN product_variant pv ON pp.id = pv.product_id ");
        //    queryBuilder.AppendLine("LEFT JOIN product_category pc ON pp.category_id = pc.id ");
        //    queryBuilder.AppendLine("LEFT JOIN product_attribute pa ON pv.id = pa.variant_id ");

        //    // Add current date parameter first
        //    parameter.Add("CurrentDate", currentDate, DbType.DateTime);

        //    if (!string.IsNullOrEmpty(request.Keyword))
        //    {
        //        queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} (pp.name LIKE @Keyword OR pp.description LIKE @Keyword OR pv.sku LIKE @Keyword OR pc.name LIKE @Keyword)");
        //        conditionClause = "WHERE";
        //        parameter.Add("Keyword", $"%{request.Keyword}%", DbType.String);
        //    }

        //    if (request.Id.HasValue)
        //    {
        //        queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pp.id = @Id");
        //        conditionClause = "WHERE";
        //        parameter.Add("Id", request.Id.Value, DbType.Int32);
        //    }

        //    if (!string.IsNullOrEmpty(request.CategoryId))
        //    {
        //        var categoryIds = request.CategoryId.Split(',')
        //                                          .Select((v, i) => new { Value = v.Trim(), ParamName = $"@CategoryId{i}" })
        //                                          .ToList();

        //        var inClause = string.Join(", ", categoryIds.Select(c => c.ParamName));
        //        queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pp.category_id IN ({inClause})");
        //        conditionClause = "WHERE";

        //        foreach (var categoryId in categoryIds)
        //        {
        //            parameter.Add(categoryId.ParamName, int.Parse(categoryId.Value), DbType.Int32);
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(request.LatestOffer))
        //    {
        //        queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pp.latest_offer = @LatestOffer");
        //        conditionClause = " WHERE ";
        //        parameter.Add("LatestOffer", request.LatestOffer, DbType.String, ParameterDirection.Input);
        //    }

        //    if (request.MinPrice.HasValue && request.MaxPrice.HasValue)
        //    {
        //        queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pv.price BETWEEN @MinPrice AND @MaxPrice");
        //        conditionClause = "WHERE";
        //        parameter.Add("MinPrice", request.MinPrice.Value, DbType.Decimal);
        //        parameter.Add("MaxPrice", request.MaxPrice.Value, DbType.Decimal);
        //    }

        //    if (!string.IsNullOrEmpty(request.Stock))
        //    {
        //        if (request.Stock.ToLower() == "instock")
        //        {
        //            queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pv.stock_quantity > 0");
        //            conditionClause = "WHERE";
        //        }
        //        else if (request.Stock.ToLower() == "outofstock")
        //        {
        //            queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} pv.stock_quantity <= 0");
        //            conditionClause = "WHERE";
        //        }
        //    }

        //    // Handle both plan and duration values together
        //    if (!string.IsNullOrEmpty(request.Plan) || !string.IsNullOrEmpty(request.Duration))
        //    {
        //        var attributeConditions = new List<string>();

        //        // Handle plan values
        //        if (!string.IsNullOrEmpty(request.Plan))
        //        {
        //            var planValues = request.Plan.Split(',')
        //                                       .Select((v, i) => new {
        //                                           Value = v.Replace(" ", "").ToLower(),
        //                                           ParamName = $"@Plan{i}"
        //                                       })
        //                                       .ToList();

        //            var planInClause = string.Join(", ", planValues.Select(p => p.ParamName));
        //            attributeConditions.Add($"LOWER(REPLACE(pa.attribute_value, ' ', '')) IN ({planInClause})");

        //            foreach (var planValue in planValues)
        //            {
        //                parameter.Add(planValue.ParamName, planValue.Value, DbType.String);
        //            }
        //        }

        //        // Handle duration values
        //        if (!string.IsNullOrEmpty(request.Duration))
        //        {
        //            var durationValues = request.Duration.Split(',')
        //                                               .Select((v, i) => new {
        //                                                   Value = v.Replace(" ", "").ToLower(),
        //                                                   ParamName = $"@Duration{i}"
        //                                               })
        //                                               .ToList();

        //            var durationInClause = string.Join(", ", durationValues.Select(d => d.ParamName));
        //            attributeConditions.Add($"LOWER(REPLACE(pa.attribute_value, ' ', '')) IN ({durationInClause})");

        //            foreach (var durationValue in durationValues)
        //            {
        //                parameter.Add(durationValue.ParamName, durationValue.Value, DbType.String);
        //            }
        //        }

        //        // Combine conditions with OR
        //        queryBuilder.AppendLine($"{Helper.GetSqlCondition(conditionClause, "AND")} ({string.Join(" OR ", attributeConditions)})");
        //        conditionClause = "WHERE";
        //    }

        //    queryBuilder.AppendLine("GROUP BY pp.id, pp.name, pp.thumbnail_image ");

        //    if (!string.IsNullOrEmpty(request.SortBy))
        //    {
        //        switch (request.SortBy.ToUpper())
        //        {
        //            case "HTOL":
        //                queryBuilder.AppendLine("ORDER BY MIN(pv.price) DESC ");
        //                break;
        //            case "LTOH":
        //                queryBuilder.AppendLine("ORDER BY MIN(pv.price) ASC ");
        //                break;
        //            case "ATOZ":
        //                queryBuilder.AppendLine("ORDER BY pp.name ASC ");
        //                break;
        //            case "ZTOA":
        //                queryBuilder.AppendLine("ORDER BY pp.name DESC ");
        //                break;
        //            default:
        //                queryBuilder.AppendLine("ORDER BY pp.id ");
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        queryBuilder.AppendLine("ORDER BY pp.id ");
        //    }

        //    if (string.IsNullOrEmpty(request.GetAll) || request.GetAll.ToUpper() != "Y")
        //    {
        //        queryBuilder.AppendLine("LIMIT @Offset, @ItemsPerPage");
        //        parameter.Add("Offset", (request.CurrentPage - 1) * request.ItemsPerPage, DbType.Int32);
        //        parameter.Add("ItemsPerPage", request.ItemsPerPage, DbType.Int32);
        //    }

        //    var query = queryBuilder.ToString();
        //    var result = await _mysqlContext.GetPagedListAsync<FilterProductVM>(
        //        request.CurrentPage,
        //        request.ItemsPerPage,
        //        query,
        //        parameter
        //    );

        //    return result;
        //}

        public async Task<FilterOptionVM> GetFilterOptions(GetFilterOptions request)
        {
            try
            {
                // Get availability options
                var availability = new List<FilterAvailability>
                {
                    new FilterAvailability
                    {
                        Name = "In Stock",
                        Value = "instock"
                    },
                    new FilterAvailability
                    {
                        Name = "Out of Stock",
                        Value = "outofstock"
                    }
                };

                // Get price range
                var maxPrice = await _context.ProductVariants
                    .Where(pv => pv.IsActive == "Y")
                    .MaxAsync(pv => (decimal?)pv.Price) ?? 0;

                var priceRange = new FilterPrice
                {
                    MinPrice = 0,
                    MaxPrice = maxPrice
                };

                // Get active categories
                var categories = await _context.ProductCategories
                    .Where(pc => pc.IsActive == "Y")
                    .Select(pc => new FilterCategory
                    {
                        Id = pc.Id,
                        Name = pc.Name
                    })
                    .ToListAsync();

                // Get subscription plans (colors)
                var plansQuery = await _context.ProductAttributes
                    .Where(pa => pa.IsActive == "Y" &&
                               pa.AttributeName.ToLower() == "color")
                    .Select(pa => pa.AttributeValue)
                    .Distinct()
                    .ToListAsync();

                var plans = plansQuery
                    .Select(value => new FilterPlan
                    {
                        Name = value,
                        Value = value.Replace(" ", "").ToLower()
                    })
                    .ToList();

                // Get subscription durations (non-colors)
                var durationsQuery = await _context.ProductAttributes
                    .Where(pa => pa.IsActive == "Y" &&
                               pa.AttributeName.ToLower() != "color")
                    .Select(pa => pa.AttributeValue)
                    .Distinct()
                    .ToListAsync();

                var durations = durationsQuery
                    .Select(value => new FilterDuration
                    {
                        Name = value,
                        Value = value.Replace(" ", "").ToLower()
                    })
                    .ToList();

                // Combine all filter options
                var filterOptions = new FilterOptionVM
                {
                    Availability = availability,
                    Price = priceRange,
                    Categories = categories,
                    Subscriptions = new FilterSubscription
                    {
                        Plan = plans,
                        Duration = durations
                    }
                };

                return filterOptions;
            }
            catch (Exception ex)
            {
                // Log the exception if you have logging configured
                throw new Exception("Error retrieving filter options", ex);
            }

        }

        public async Task<ProductDetailsVM> GetProductDetails(GetProductDetails request)
        {
            if (!request.Id.HasValue)
            {
                throw new ArgumentNullException(nameof(request.Id), "Product ID must be provided");
            }

            var queryBuilder = new StringBuilder();
            var parameter = new DynamicParameters();

            // First, retrieve basic product details
            queryBuilder.AppendLine(@"
    SELECT 
        pp.id AS Id, 
        pp.name AS Name, 
        pp.category_id AS CategoryId,
        pc.name AS CategoryName,
        LOWER(REPLACE(REPLACE(REPLACE(pc.name, ' ', '-'), '&', 'and'), ',', '')) AS Slug,
        pp.short_description AS ShortDescription, 
        pp.description AS Description, 
        pp.thumbnail_image AS ThumbnailImage, 
        COALESCE(AVG(pr.rating), 0) AS Rating, 
        COUNT(DISTINCT pr.id) AS ReviewCount
    FROM 
        product_profile pp
    JOIN
        product_category pc
    ON
        pp.category_id = pc.id AND pc.is_active = 'Y'
    LEFT JOIN 
        product_review pr 
    ON 
        pp.id = pr.product_id AND pr.is_active = 'Y'
    WHERE 
        pp.id = @ProductId AND pp.is_active = 'Y'
    GROUP BY 
        pp.id, pp.name, pp.short_description, pp.description, pp.thumbnail_image");

            parameter.Add("ProductId", request.Id.Value, DbType.Int32);

            // Execute the first query to get basic product details
            var productDetails = await _mysqlContext.GetFirstOrDefaultAsync<ProductDetailsVM>(queryBuilder.ToString(), parameter);

            if (productDetails == null)
            {
                throw new KeyNotFoundException($"Product with ID {request.Id.Value} not found");
            }

            // Retrieve unique product images only.
            var images = await _context.ProductImages
                .Where(pi => pi.ProductId == request.Id.Value
                && pi.IsActive == "Y"
                && pi.Image != "/Images/Product/ProductImage/default.png") // Skip specific path
                .Select(pi => pi.Image)
                .Distinct() // Ensure uniqueness
                .ToListAsync();

            productDetails.Images = images;

            // Retrieve product variants
            var variants = await _context.ProductVariants
                .Where(pv => pv.ProductId == request.Id.Value && pv.IsActive == "Y")
                .Select(pv => new ProductDetailsVariant
                {
                    Id = pv.Id,
                    Name = pv.Name,
                    Sku = pv.SKU,
                    StockStatus = pv.StockQuantity > 0 ? "In Stock" : "Out of Stock",
                    Price = pv.Price,
                    FinalPrice = pv.DiscountStartDate <= DateTime.Now && pv.DiscountEndDate >= DateTime.Now
                        ? pv.Price - pv.DiscountAmount
                        : pv.Price,
                    DiscountAmount = pv.DiscountStartDate <= DateTime.Now && pv.DiscountEndDate >= DateTime.Now
                        ? pv.DiscountAmount
                        : 0
                })
                .ToListAsync();

            productDetails.Variants = variants;

            return productDetails;
        }
    }
}