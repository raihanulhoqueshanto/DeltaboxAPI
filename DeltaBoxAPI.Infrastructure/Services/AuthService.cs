using DeltaboxAPI.Application.Common.Interfaces;
using DeltaboxAPI.Application.Common.Models.DTO;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Auth;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Auth.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Token;
using DeltaboxAPI.Domain.Entities.DeltaBox.Common;
using DeltaBoxAPI.Application.Common.Models;
using DeltaBoxAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeltaboxAPI.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AuthService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, IEmailService emailService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<Result> LoginRequest(LoginModel request)
        {
            var emailValidator = new EmailAddressAttribute();

            bool isEmail = emailValidator.IsValid(request.UsernameOrEmail);

            var user = isEmail 
                ? await userManager.FindByEmailAsync(request.UsernameOrEmail) 
                : await userManager.FindByNameAsync(request.UsernameOrEmail);

            //var user = await userManager.FindByNameAsync(request.UsernameOrEmail);
            //var user = await userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user != null && await userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenService.GetToken(authClaims);
                var refreshToken = _tokenService.GetRefreshToken();
                var tokenInfo = _context.TokenInfos.FirstOrDefault(a => a.Username == user.UserName);

                if (tokenInfo == null)
                {
                    var info = new TokenInfo
                    {
                        Username = user.UserName,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiry = DateTime.Now.AddDays(1)
                    };
                    _context.TokenInfos.Add(info);
                }
                else
                {
                    tokenInfo.RefreshToken = refreshToken;
                    tokenInfo.RefreshTokenExpiry = DateTime.Now.AddDays(1);
                }
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Result.Failure("Failed", "500", new string[] { "Invalid info. try again!" }, null);
                }

                var succeedData = new LoginResponse
                {
                    Name = user.Name,
                    Username = user.UserName,
                    Token = token.TokenString,
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                };

                return Result.Success("Success", "200", new string[] { "Successfully logged in!" }, succeedData);
            }

            //login failed condition
            var failedData = new LoginResponse
            {
                Name = "",
                Username = "",
                Token = "",
                RefreshToken = "",
                Expiration = null
            };

            return Result.Failure("Failed", "500", new string[] { "Invalid Username or Password!" }, null);
        }

        public async Task<Result> UserRegistrationRequest(RegistrationModel request)
        {
            // check if user exists
            var userExists = await userManager.FindByNameAsync(request.Username);
            if (userExists != null)
            {
                return Result.Failure("Failed", "500", new string[] { "Username already exists!" }, null);
            }
            var user = new ApplicationUser
            {
                UserName = request.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = request.Email,
                Name = request.Name
            };
            // create a user here
            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return Result.Failure("Failed", "500", new string[] { "User creation failed!" }, null);
            }

            // add roles here
            // for admin registration UserRoles.Admin instead of UserRoles.Roles
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return Result.Success("Success", "200", new string[] { "Successfully Registered!" }, null);
        }

        public async Task<Result> ChangePasswordRequest(ChangePasswordModel request)
        {
            // lets find the user
            var user = await userManager.FindByNameAsync(request.Username);
            if (user is null)
            {
                return Result.Failure("Failed", "500", new string[] { "Invalid username!" }, null);
            }
            // check current password
            if (!await userManager.CheckPasswordAsync(user, request.CurrentPassword))
            {
                return Result.Failure("Failed", "500", new string[] { "Invalid current password!" }, null);
            }

            // change password here
            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return Result.Failure("Failed", "500", new string[] { "Failed to change password!" }, null);
            }

            return Result.Success("Success", "200", new string[] { "Password has changed successfully!" }, null);
        }

        public async Task<Result> AdminRegistrationRequest(RegistrationModel request)
        {
            // check if user exists
            var userExists = await userManager.FindByNameAsync(request.Username);
            if (userExists != null)
            {
                return Result.Failure("Failed", "500", new string[] { "Username already exists!" }, null);
            }
            var user = new ApplicationUser
            {
                UserName = request.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = request.Email,
                Name = request.Name
            };
            // create a user here
            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return Result.Failure("Failed", "500", new string[] { "User creation failed!" }, null);
            }

            // add roles here
            // for admin registration UserRoles.Admin instead of UserRoles.Roles
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Result.Success("Success", "200", new string[] { "Successfully Registered!" }, null);
        }

        public async Task<Result> ForgotPasswordRequest(ForgotPasswordModel request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Result.Failure("Failed", "404", new[] { "User not found" }, null);
            }

            // Generate OTP
            var otp = GenerateOTP();

            // Remove any existing OTPs for this email
            var existingOTPs = _context.PasswordResetOTPs
                .Where(x => x.Email == request.Email && !x.IsUsed);
            _context.PasswordResetOTPs.RemoveRange(existingOTPs);

            // Create new OTP record
            var otpEntity = new PasswordResetOTP
            {
                Email = request.Email,
                OTP = otp,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false
            };
            _context.PasswordResetOTPs.Add(otpEntity);
            await _context.SaveChangesAsync();

            // Send OTP via email
            await _emailService.SendPasswordResetOTPAsync(request.Email, otp);

            return Result.Success("Success", "200", new[] { "OTP sent to email" }, null);
        }

        public async Task<Result> ResetPasswordRequest(ResetPasswordModel request)
        {
            var otpRecord = _context.PasswordResetOTPs
                .FirstOrDefault(x =>
                    x.Email == request.Email &&
                    x.OTP == request.OTP &&
                    x.ExpiresAt > DateTime.UtcNow &&
                    !x.IsUsed);

            if (otpRecord == null)
            {
                return Result.Failure("Failed", "400", new[] { "Invalid or expired OTP" }, null);
            }

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Result.Failure("Failed", "404", new[] { "User not found" }, null);
            }

            // Reset password
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (!result.Succeeded)
            {
                return Result.Failure("Failed", "500", new[] { "Password reset failed" }, null);
            }

            // Mark OTP as used
            otpRecord.IsUsed = true;
            await _context.SaveChangesAsync();

            return Result.Success("Success", "200", new[] { "Password reset successful" }, null);
        }

        private string GenerateOTP()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
