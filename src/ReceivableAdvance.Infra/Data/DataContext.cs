using System.Data;
using System.Data.Common;

namespace ReceivableAdvance.Infra.Data;

public class DataContext
{
    private readonly Lazy<DbConnection> _lazyConnection;
    private readonly DbProviderFactory _factory;
    private readonly DbConnectionStringBuilder _connectionStringBuilder;

    public DataContext(DbProviderFactory factory, DbConnectionStringBuilder connectionStringBuilder)
    {
        _lazyConnection = new Lazy<DbConnection>(CreateConnection);
        _factory = factory;
        _connectionStringBuilder = connectionStringBuilder;
    }

    public DataContext(DbConnection connection, DbProviderFactory factory, DbConnectionStringBuilder connectionStringBuilder)
    {
        _lazyConnection = new Lazy<DbConnection>(connection);
        _factory = factory;
        _connectionStringBuilder = connectionStringBuilder;
    }

    public DbConnection Connection => _lazyConnection.Value;
    private DbConnection CreateConnection()
    {
        var result = _factory.CreateConnection() ?? throw new InvalidOperationException("Failed to create a connection.");

        result.ConnectionString = _connectionStringBuilder.ConnectionString;

        return result;
    }
}