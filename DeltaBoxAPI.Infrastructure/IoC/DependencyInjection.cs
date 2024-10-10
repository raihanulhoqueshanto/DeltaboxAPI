using DeltaboxAPI.Application.Common.Interfaces;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Auth;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Token;
using DeltaboxAPI.Infrastructure.Services;
using DeltaBoxAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DeltaboxAPI.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)),
                    options => options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                    ));
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<MysqlDbContext>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IFaqsService, FaqsService>();
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }
    }
}