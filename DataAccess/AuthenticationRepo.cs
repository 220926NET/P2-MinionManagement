namespace DataAccess;

public class AuthenticationRepo : IAuthenticationRepo
{
    public AuthenticationRepo() {}

    public void NewLogIn(string username, string password) {
        SqlQuery.SetData("INSERT INTO User_Login (UserName, PassWord) VALUES (@UName, @PWord);", 
                        new List<string> {"@UName", "@PWord"}, new List<string> {username, password});
        NewProfile(username);
        return;
    }
    public void NewProfile(string username) {
        SqlQuery.SetData("INSERT INTO User_Profiles (UserName, FirstName, LastName, TroopCount) VALUES (@UName, @FName, @LName, @TroopCount);", 
                        new List<string> {"@UName", "@FName", "@LName", "@TroopCount"}, new List<string> {username, "N/A", "N/A", "0"});
        return;
    }

    public bool UsernameExists(string username) {
        string? user = SqlQuery.GetValue($"SELECT * FROM User_Login WHERE UserName = '{username}';", "Username");
        if (user == null)   return false;
        else    return true;
    }
    public string? GetHash(string username) {
        string? pass = SqlQuery.GetValue($"SELECT * FROM User_Login WHERE UserName = '{username}';", "Password");
        if (pass == null)   return null;
        else    return pass;
    }
    public int UserId(string username) {
        string? id = SqlQuery.GetValue($"SELECT * FROM User_Profiles WHERE UserName = '{username}';", "ID");
        if (id == null) return 0;
        else    return int.Parse(id);
    }
}