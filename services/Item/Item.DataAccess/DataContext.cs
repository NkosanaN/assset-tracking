using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Item.DataAccess;
public class DataContext
{
    
    public DataContext(DbContextOptions options) : base()
    {
        
    }
    public DbSet<Domain.Item> Items { get; set; }
}

//public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
//{
//    public DbSet<Domain.Item> Items { get; set; }
//}