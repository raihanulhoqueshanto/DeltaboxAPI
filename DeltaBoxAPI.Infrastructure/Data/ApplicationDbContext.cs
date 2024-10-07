using DeltaboxAPI.Application.Common.Interfaces;
using DeltaboxAPI.Application.Common.Models;
using DeltaboxAPI.Domain.Entities.DeltaBox.Common;
using DeltaboxAPI.Domain.Entities.DeltaBox.Faqs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaBoxAPI.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUserService _currentUserService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<TokenInfo> TokenInfos { get; set; }
        public DbSet<FaqsSetup> FaqsSetups { get; set; }

        // Will use this method to save anything on the database
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                foreach (var entry in ChangeTracker.Entries<CommonEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedBy = _currentUserService.UserId;
                            entry.Entity.CreatedDate = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            entry.Entity.UpdatedBy = _currentUserService.UserId;
                            entry.Entity.UpdatedDate = DateTime.Now;
                            break;
                    }
                }

                return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //_logger.Error($"Concurrency error {ex.InnerException?.Message ?? ex.Message} \n {ex.InnerException?.StackTrace ?? ex.StackTrace}");
                throw; // Or handle it appropriately
            }
            catch (Exception ex)
            {
                //_logger.Error($"Save changes error {ex.InnerException?.Message ?? ex.Message} \n {ex.InnerException?.StackTrace ?? ex.StackTrace}");
                throw; // Or handle it appropriately
            }
        }
    }
}
