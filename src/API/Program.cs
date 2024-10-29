using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Persistence.DbInitializer;
using Serilog;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt =>
{
	var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
	opt.Filters.Add(new AuthorizeFilter(policy));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureIdentityService(builder.Configuration);
builder.Services.ConfigurePersistenceService(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var _logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(_logger);

var logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");

SeedDatabase();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSerilogRequestLogging();

app.Run();



void SeedDatabase()
{
	using var scope = app!.Services.CreateScope();
	var services = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
	services.Initialize();
}

public partial class Program { }
