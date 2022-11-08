using Microsoft.Data.SqlClient;

namespace DataAccess;

internal static class SqlQuery
{
    private static ConnectionFactory _factory = new ConnectionFactory();

    internal static string? GetValue(string query, string column) {
        string? value = null;
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) {
                if (reader.Read()) {
                    if (reader[column].GetType() == typeof(Int32))
                        value = reader[column].ToString();
                    else    
                        value = (string) reader[column];
                }
            }

            connection.Close();
        }
        catch (SqlException ex)
        {
            // SeriLog
            Console.WriteLine(ex);
        }
        return value;
    }

    internal static List<string>? GetData(string query, List<string> columns) {
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) {
                List<string> values = new List<string>();

                while (reader.Read()) {
                    foreach (string column in columns) {
                        if (reader[column].GetType() == typeof(DBNull)) 
                            continue;
                        else if (reader[column].GetType() == typeof(Int32))
                            values.Add(reader[column].ToString()!);
                        else if (reader[column].GetType() == typeof(Decimal))
                            values.Add(reader[column].ToString()!);
                        else {
                            values.Add((string) reader[column]);
                        }
                    }
                }
                connection.Close();
                return values;
            }

            connection.Close();
        }
        catch (SqlException ex)
        {
            // SeriLog
            Console.WriteLine(ex);  // Comment these out before END!!
        }
        return null;
    }
    
    internal static void SetData(string command, List<string> parameters, List<string> values) {
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            using SqlCommand cmd = new SqlCommand(command, connection);

            for (int i = 0; i < parameters.Count; i++) {
                cmd.Parameters.AddWithValue(parameters[i], values[i]);
            }

            cmd.ExecuteNonQuery();
            connection.Close();
        }
        catch (SqlException ex)
        {
            // SeriLog
            Console.WriteLine(ex);
        }
        return;
    }
}