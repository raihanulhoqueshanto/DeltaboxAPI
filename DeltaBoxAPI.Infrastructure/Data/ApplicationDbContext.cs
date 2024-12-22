using DeltaboxAPI.Application.Common.Interfaces;
using DeltaboxAPI.Domain.Entities.DeltaBox.Common.Models;
using DeltaboxAPI.Domain.Entities.DeltaBox.Common;
using DeltaboxAPI.Domain.Entities.DeltaBox.Faqs;
using DeltaboxAPI.Domain.Entities.DeltaBox.Payment;
using DeltaboxAPI.Domain.Entities.DeltaBox.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeltaboxAPI.Domain.Entities.DeltaBox.Banner;
using DeltaboxAPI.Domain.Entities.DeltaBox.Brand;
using DeltaboxAPI.Domain.Entities.DeltaBox.Order;
using DeltaboxAPI.Domain.Entities.DeltaBox.Offer;
using DeltaboxAPI.Domain.Entities.DeltaBox.Blog;

namespace DeltaBoxAPI.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _currentUserService = currentUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<TokenInfo> TokenInfos { get; set; }
        public DbSet<FaqsSetup> FaqsSetups { get; set; }
        public DbSet<GeneralQuestion> GeneralQuestions { get; set; }
        public DbSet<PaymentProof> PaymentProofs { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductProfile> ProductProfiles { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<AdsBanner> AdsBanners { get; set; }
        public DbSet<AssociateBrand> AssociateBrands { get; set; }
        public DbSet<ProductFaq> ProductFaqs { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<PasswordResetOTP> PasswordResetOTPs { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<PromotionCode> PromotionCodes { get; set; }
        public DbSet<CommonImage> CommonImages { get; set; }
        public DbSet<OrderProfile> OrderProfiles { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<RewardPoint> RewardPoints { get; set; }
        public DbSet<PaymentInformation> PaymentInformations { get; set; }
        public DbSet<Article> Articles { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;

                foreach (var entry in ChangeTracker.Entries<CommonEntity>())
                {
                    // Get client IP from headers (Cloudflare and Proxy aware)
                    string ip = httpContext?.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? httpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? httpContext?.Connection.RemoteIpAddress?.ToString();

                    // Handle loopback (::1) for localhost
                    if (ip == "::1")
                    {
                        ip = "127.0.0.1";
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.Ip = ip;
                            entry.Entity.CreatedBy = _currentUserService.UserId;
                            entry.Entity.CreatedDate = DateTime.Now;
                            break;

                        case EntityState.Modified:
                            entry.Entity.Ip = ip;
                            entry.Entity.UpdatedBy = _currentUserService.UserId;
                            entry.Entity.UpdatedDate = DateTime.Now;
                            break;
                    }
                }

                return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the error (you can add logging here)
                throw;
            }
            catch (Exception ex)
            {
                // Log the error (you can add logging here)
                throw;
            }
        }
    }
}