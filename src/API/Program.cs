using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;
using Persistence.DbInitializer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//SeedDatabase();


//try
//{
//    var context = services.GetRequiredService<DbContext>();
//    await context.Database.MigrateAsync();
//}
//catch (Exception ex)
//{
//    var  logger = services.GetRequiredService<ILogger<Program>>();
//    logger.LogError(ex,"Error occur during migration");
//}

app.Run();

void SeedDatabase()
{
    using var scope = app!.Services.CreateScope();
    var services = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    services.Initialize();
}
