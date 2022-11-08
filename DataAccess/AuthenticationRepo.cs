using Microsoft.Data.SqlClient;

namespace DataAccess;

public class AuthenticationRepo : IAuthenticationRepo
{
    private ConnectionFactory _factory; 
    public AuthenticationRepo() {
        _factory = new ConnectionFactory();
    }

    private string? GetValue(string query, string column) {
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
    private void SetData(string command, List<string> parameters, List<string> values) {
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
    
    public void NewLogIn(string username, string password) {
        SetData("INSERT INTO User_Login (UserName, PassWord) VALUES (@UName, @PWord)", new List<string> {"@UName", "@PWord"}, new List<string> {username, password});
        NewProfile(username);
        return;
    }
    public void NewProfile(string username) {
        SetData("INSERT INTO User_Profiles (UserName, FirstName, LastName, TroopCount) VALUES (@UName, @FName, @LName, @TroopCount)", 
                new List<string> {"@UName", "@FName", "@LName", "@TroopCount"}, new List<string> {username, "N/A", "N/A", "0"});
        return;
    }

    public bool UsernameExists(string username) {
        string? user = GetValue($"SELECT * FROM User_Login WHERE UserName = '{username}';", "Username");
        if (user == null)   return false;
        else    return true;
    }
    public string? GetHash(string username) {
        string? pass = GetValue($"SELECT * FROM User_Login WHERE UserName = '{username}';", "Password");
        if (pass == null)   return null;
        else    return pass;
    }
    public int UserId(string username) {
        string? id = GetValue($"SELECT * FROM User_Profiles WHERE UserName = '{username}';", "ID");
        if (id == null) return 0;
        else    return int.Parse(id);
    }
}