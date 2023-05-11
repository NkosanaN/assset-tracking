using API.Extensions;
using API.Middleware;
using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Persistence;
using Serilog;
//using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceService(builder.Configuration);
builder.Services.ConfigureApiServices(builder.Configuration);
builder.Services.ConfigureIdentityService(builder.Configuration);

var _logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(
    ((ctx, lc) => 
    lc.ReadFrom.Configuration(ctx.Configuration)));


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

app.UseHttpsRedirection();
//SeedDatabase();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSerilogRequestLogging();

app.Run();

//void SeedDatabase()
//{
//    using var scope = app!.Services.CreateScope();
//    var services = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
//    services.Initialize();
//}
