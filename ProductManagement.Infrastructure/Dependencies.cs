using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Application.Common;
using ProductManagement.Application.Identity.Interfaces;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Infrastructure.Identity.Helpers;
using ProductManagement.Infrastructure.Identity.Interfaces;
using ProductManagement.Infrastructure.Identity.Models;
using ProductManagement.Infrastructure.Identity.Services;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.Infrastructure.Persistence.Core;
using ProductManagement.Infrastructure.Persistence.Interfaces;
using ProductManagement.Infrastructure.Persistence.Repositories;
using ProductManagement.Infrastructure.Services;

namespace ProductManagement.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContextFactory<ApplicationDbContext>();
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IDateTime, DateTimeService>();
        services.AddScoped(typeof(IUserAccessor), typeof(UserAccessor));
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IIdentityUserCreator), typeof(IdentityUserCreator));
        services.AddScoped(typeof(ILoginService), typeof(LoginService));
        services.AddScoped(typeof(IRegisterService), typeof(RegisterService));
        services.AddScoped(typeof(ITokenService), typeof(TokenService));
        
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<ApplicationUser>>();
        
        return services;
    }
    
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<ApplicationUser>>();
        
        return services;
    }
    
    public static IServiceCollection ConfigureJwt(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperNonReachableSecret"))
            };
        });

        services.AddAuthorization();
        
        return services;
    }
    
    
}