using Microsoft.Data.SqlClient;

namespace DataAccess;

public class ConnectionFactory{

    private const string _connectionString = Secret.ConnectionString;

    public SqlConnection AddConnection(){
        return new SqlConnection(_connectionString);
    }

    public SqlConnection GetConnection() {
        return new SqlConnection(_connectionString);
    }
}


