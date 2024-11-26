using System.Text.Json.Serialization;
using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace ItemManagementSystem.Tests.Integrationx;
public abstract class TestBase : IClassFixture<TestWebApplicationFactory>
{
	private readonly IServiceScope _scope;
	protected readonly ISender Sender;
	protected readonly DataContext Context;

	public TestBase(TestWebApplicationFactory factory)
	{
		_scope = factory.Services.CreateScope();
		Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
		Context = _scope.ServiceProvider.GetRequiredService<DataContext>();
		Context.Database.MigrateAsync();
	}
	internal static JsonSerializerOptions TestSerializerOptions => new JsonSerializerOptions
	{
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		PropertyNameCaseInsensitive = true
	};

}
