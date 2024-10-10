using DeltaboxAPI.Application.Common.Interfaces;
using DeltaboxAPI.Domain.Entities.DeltaBox.Common;
using DeltaboxAPI.Domain.Entities.DeltaBox.Common.Models;
using DeltaboxAPI.Domain.Entities.DeltaBox.Faqs;
using DeltaboxAPI.Domain.Entities.DeltaBox.Payment;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
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
        public DbSet<GeneralQuestion> GeneralQuestions { get; set; }
        public DbSet<PaymentProof> PaymentProofs { get; set; }

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
                // Log the error
                throw;
            }
            catch (Exception ex)
            {
                // Log the error
                throw;
            }
        }
    }
}