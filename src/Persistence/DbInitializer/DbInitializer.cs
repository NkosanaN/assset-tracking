namespace Persistence.DbInitializer;

public class DbInitializer : IDbInitializer
{
    private readonly DataContext _db;
    public DbInitializer(DataContext db)
    {
        _db = db;
    }
    public void Initialize()
    {
        try
        {
            var dbExists = _db.Database.CanConnect();

            if (!dbExists)
            {
                var createDB = _db.Database.EnsureCreated();

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
    public static void Seed(DataContext context)
    {

    }
}

