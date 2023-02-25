using System.Text;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<DataContext>();

        //builder.Services.AddScoped<UserManager<ApplicationUser>>();
        //this allows us to query Users in Identity Store

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false, // this need work
                    ValidateIssuer = false // this need work
                };
            });

        services.AddScoped<TokenService>();

        return services;
    }
}

