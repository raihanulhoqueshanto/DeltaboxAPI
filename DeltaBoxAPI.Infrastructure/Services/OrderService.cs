using DeltaboxAPI.Application.Common.Interfaces;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order;
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
    }
}
