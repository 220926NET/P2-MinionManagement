using Microsoft.Data.SqlClient;

namespace DataAccess;

public class ConnectionFactory{

    // DEPRECATED: private const string _connectionString = Secret.ConnectionString;
    private readonly string _connectionString;
    public ConnectionFactory(string connectionString) {
        _connectionString = connectionString;
    }

    public SqlConnection GetConnection() {
        return new SqlConnection(_connectionString);
    }
}


