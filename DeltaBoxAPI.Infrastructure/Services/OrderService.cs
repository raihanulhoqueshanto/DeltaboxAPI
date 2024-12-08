using DeltaboxAPI.Application.Common.Interfaces;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Commands;
using DeltaboxAPI.Domain.Entities.DeltaBox.Order;
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
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly MysqlDbContext _mysqlContext;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ICurrentUserService _currentUserService;

        public OrderService(ApplicationDbContext context, MysqlDbContext mysqlContext, IHostEnvironment hostEnvironment, ICurrentUserService currentUserService)
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

        public async Task<Result> AddInWishlist(Wishlist request)
        {
            try
            {
                var customerId = _currentUserService.UserId;
                var existingWishlist = await _context.Wishlists.FirstOrDefaultAsync(c => c.CustomerId == customerId && c.ProductId == request.ProductId && c.Sku.Replace(" ", "").ToLower() == request.Sku.Replace(" ", "").ToLower() && c.IsActive == "Y");

                if (existingWishlist != null)
                {
                    return Result.Failure("Failed", "409", new[] { "Already added in wishlist." }, null);
                }

                request.CustomerId = customerId;
                request.IsActive = "Y";
                await _context.Wishlists.AddAsync(request);

                int result = await _context.SaveChangesAsync();

                return result > 0
                     ? Result.Success("Success", "200", new[] { "Added in wishlist." }, null)
                     : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Result.Failure("Failed", "500", new[] { errorMessage }, null);
            }
        }

        public async Task<Result> RemoveFromWishlist(RemoveFromWishlist request)
        {
            try
            {
                var customerId = _currentUserService.UserId;
                var wishlistObj = await _context.Wishlists.FirstOrDefaultAsync(c => c.Id == request.Id && c.CustomerId == customerId && c.IsActive == "Y");
                if (wishlistObj != null)
                {
                    wishlistObj.IsActive = "N";
                }
                else
                {
                    return Result.Failure("Failed", "404", new[] { "Not found in wishlist." }, null);
                }

                 _context.Wishlists.Update(wishlistObj);

                int result = await _context.SaveChangesAsync();

                return result > 0
                     ? Result.Success("Success", "200", new[] { "Removed Successfully" }, null)
                     : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);

            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Result.Failure("Failed", "500", new[] { errorMessage }, null);
            }
        }

        public async Task<List<WishlistVM>> GetWishlist()
        {
            var customerId = _currentUserService.UserId;
            var currentDate = DateTime.Now;

            var wishlistQuery = await (from w in _context.Wishlists
                                       join pp in _context.ProductProfiles on w.ProductId equals pp.Id
                                       join pv in _context.ProductVariants on w.Sku equals pv.SKU
                                       where w.CustomerId == customerId
                                             && w.IsActive == "Y"
                                             && pp.IsActive == "Y"
                                             && pv.IsActive == "Y"
                                       select new WishlistVM
                                       {
                                           Id = w.Id,
                                           ProductId = pp.Id,
                                           ProductName = pp.Name,
                                           VariantId = pv.Id,
                                           VariantName = pv.Name,
                                           Price = pv.Price.ToString("F2"),
                                           FinalPrice = (pv.Price -
                                               (currentDate >= pv.DiscountStartDate && currentDate <= pv.DiscountEndDate
                                                   ? pv.DiscountAmount
                                                   : 0m)).ToString("F2"),
                                           StockStatus = pv.StockQuantity > 0 ? "In Stock" : "Out of Stock",
                                           ThumbnailImage = pp.ThumbnailImage
                                       }).ToListAsync();

            return wishlistQuery;
        }

        public async Task<Result> AddToCart(Cart request)
        {
            try
            {
                var customerId = _currentUserService.UserId;
                var existingCart = await _context.Carts.FirstOrDefaultAsync(c => c.CustomerId == customerId && c.ProductId == request.ProductId && c.Sku.Replace(" ", "").ToLower() == request.Sku.Replace(" ", "").ToLower() && c.IsActive == "Y");

                if (existingCart != null)
                {
                    return Result.Failure("Failed", "409", new[] { "Already added in cart." }, null);
                }

                request.CustomerId = customerId;
                request.IsActive = "Y";
                await _context.Carts.AddAsync(request);

                int result = await _context.SaveChangesAsync();

                return result > 0
                     ? Result.Success("Success", "200", new[] { "Added to cart." }, null)
                     : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Result.Failure("Failed", "500", new[] { errorMessage }, null);
            }
        }
    }
}
