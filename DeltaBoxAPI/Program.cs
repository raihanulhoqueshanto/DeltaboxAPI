using DeltaBoxAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using DeltaboxAPI.Domain.Entities.DeltaBox.Common;
using DeltaboxAPI.Infrastructure.IoC;
using DeltaboxAPI.Application.IoC;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add HttpContextAccessor service
builder.Services.AddHttpContextAccessor();

// Use the configuration from the builder
IConfiguration Configuration = builder.Configuration;

// Configure MySQL Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mysqlOptions =>
        {
            // Add retry on failure for MySQL
            mysqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);

            // Enable detailed logging (optional, remove in production)
            mysqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        }
    )
    // Optional: Enable sensitive data logging for debugging
    .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
    .EnableDetailedErrors(builder.Environment.IsDevelopment());
});

// Register custom services
builder.Services.AddInfrastructure(Configuration);
builder.Services.AddApplication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "DeltaBox IT API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// For Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password complexity settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; // Consider setting to true in production
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = Configuration["Jwt:Audience"],
        ValidIssuer = Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero // Remove delay of token when expire
    };
});

// Build the app.
var app = builder.Build();

// Configure the HTTP request pipeline.
// Swagger configuration
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeltaBox IT API V1");
    c.RoutePrefix = "swagger"; // This allows accessing Swagger UI at the root URL
});

// Use HTTPS redirection
app.UseHttpsRedirection();

// Apply CORS policy
app.UseCors("AllowAll");

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Static file serving
app.UseStaticFiles();

// Serve images from a specific directory
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
    RequestPath = new PathString("/Images")
});

// Map controllers
app.MapControllers();

// Run the application
app.Run();


// PREVIOUS ONE
//using DeltaBoxAPI.Infrastructure.Data;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.OpenApi.Models;
//using DeltaboxAPI.Domain.Entities.DeltaBox.Common;
//using DeltaboxAPI.Infrastructure.IoC;
//using DeltaboxAPI.Application.IoC;
//using Microsoft.Extensions.FileProviders;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();

//// Add HttpContextAccessor service
//builder.Services.AddHttpContextAccessor();

//// Use the configuration from the builder
//IConfiguration Configuration = builder.Configuration;

//// Register custom services
//builder.Services.AddInfrastructure(Configuration);
//builder.Services.AddApplication();

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo { Title = "DeltaBox IT API", Version = "v1" });
//    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = JwtBearerDefaults.AuthenticationScheme
//    });

//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = JwtBearerDefaults.AuthenticationScheme
//                },
//                Scheme = "Oauth2",
//                Name = JwtBearerDefaults.AuthenticationScheme,
//                In = ParameterLocation.Header
//            },
//            new List<string>()
//        }
//    });
//});

//// For Identity
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

//// Adding Authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})

//// Adding Jwt Bearer
//.AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = true;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});

//// Build the app.
//var app = builder.Build();

//// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}

//app.UseSwagger();
//app.UseSwaggerUI();

//app.UseHttpsRedirection();

//app.UseCors(options => options.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//app.UseAuthentication();
//app.UseAuthorization();

//app.UseStaticFiles();

//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
//    RequestPath = new PathString("/Images")
//});

//app.MapControllers();

//app.Run();