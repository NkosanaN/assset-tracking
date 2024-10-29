namespace Persistence.DbInitializer;

public class DbInitializer(DataContext db) : IDbInitializer
{
	private readonly DataContext _db = db;

	public void Initialize()
	{
		try
		{
			bool dbExists = _db.Database.CanConnect();

			if (!dbExists)
			{
				bool createDB = _db.Database.EnsureCreated();

				if (createDB)
				{
					//Seed(_db);
				}
			}

		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
		return;
	}
	//public static void Seed(DataContext context)
	//{

	//}
}

