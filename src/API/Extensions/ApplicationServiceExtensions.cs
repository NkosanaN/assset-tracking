using API.Services;
using Application.Core;
using Application.Interfaces;
using Application.Items;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Photos;
using Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.DbInitializer;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Asset-Tracking API", Version = "v1" });
        });

        //services.AddDbContext<DataContext>(opt =>
        //    opt.UseSqlite(config.GetConnectionString("DefaultConnection"))
        //);

        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(config
                .GetConnectionString("DefaultConnection")));

        //services.AddCors(opt =>
        //{
        //    opt.AddPolicy("CorsPolicy",
        //        policy =>
        //        {
        //            policy
        //                .AllowAnyMethod()
        //                .AllowAnyHeader()
        //                .AllowCredentials()
        //                .WithOrigins("http://localhost:3000");
        //        });
        //});

        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddMediatR(typeof(List));
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Create>();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<IPhotoAccessor, PhotoAccessor>();
        //services.AddScoped<IEmailSender, EmailSender>();
        services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));

        //services.AddSingleton(new EmailService("smtp.example.com", 587, "username", "password"));
  

        return services;
    }
}

