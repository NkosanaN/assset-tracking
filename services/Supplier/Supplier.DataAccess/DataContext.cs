using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Supplier.DataAccess.Interface;
using System.Data;

namespace Supplier.DataAccess;

public class DataContext(IConfiguration configaration) : IDataContext
{
    private readonly string _connectionString = configaration.GetConnectionString("DefaultConnection")!;

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    
}
