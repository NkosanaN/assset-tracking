using Application;
using Application.Contracts.Persistence;
using Application.Core;
using Application.ItemEmployeeAssignments;
using Application.Items;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Security;
using MediatR;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection ConfigureApplicationServices(
			this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Asset-Tracking API", Version = "v1" }));

		services.AddCors(opt => opt.AddPolicy("CorsPolicy",
							policy => policy
											.AllowAnyMethod()
											.AllowAnyHeader()
											.AllowCredentials()
											.WithOrigins("http://localhost:3000")));

		//services.AddScoped<IDbInitializer, DbInitializer>();
		services.AddMediatR(typeof(GetItemAssignementHandler), typeof(GetItemDetailsHandler));
		services.AddAutoMapper(typeof(MappingProfiles).Assembly);
		services.AddFluentValidationAutoValidation();
		//services.AddValidatorsFromAssemblyContaining<Create>();
		services.AddHttpContextAccessor();
		services.AddScoped<IUserAccessor, UserAccessor>();
		//services.AddScoped<IPhotoAccessor, PhotoAccessor>();
		//services.AddScoped<IEmailSender, EmailSender>();
		//services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));
		//services.AddSingleton(new EmailService("smtp.example.com", 587, "username", "password"));
		services.AddApplication();
		return services;
	}
}

