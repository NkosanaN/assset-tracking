using System.Data;

namespace Supplier.DataAccess.Interface;

public interface IDataContext
{
    IDbConnection CreateConnection();
}
