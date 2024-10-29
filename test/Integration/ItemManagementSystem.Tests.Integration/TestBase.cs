//using Testcontainers.MsSql;

//namespace ItemManagementSystem.Tests.Integration;
//public class TestBase
//{
//	private MsSqlContainer _sqlContainer;

//	protected TestWebApplicationFactory? AppFactory { get; private set; }

//	[SetUp]
//	public virtual async Task OnSetup()
//	{
//		_sqlContainer = new MsSqlBuilder()
//			.WithImage("mcr.microsoft.com/mssql/server:2022-latest")
//			.WithPassword($"Password{DateTime.UtcNow.GetHashCode()}")
//			.WithName($"ItemTransfer{DateTime.UtcNow.GetHashCode()}")
//			.Build();

//		await _sqlContainer.StartAsync().ConfigureAwait(false);

//		AppFactory = new TestWebApplicationFactory(_sqlContainer.GetConnectionString());
//	}

//	[TearDown]
//	public async Task OnTeardown()
//	{
//		if (_sqlContainer != null)
//		{
//			await _sqlContainer.DisposeAsync();
//		}
//	}

//}
