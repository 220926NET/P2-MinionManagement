using Microsoft.Data.SqlClient;

namespace DataAccess;

public class ConnectionFactory{

    private const string _connectionString = SecretString.ConnectionString;

    public SqlConnection AddConnection(){

        return new SqlConnection(_connectionString);
    }
}