using Models;

namespace DataAccess;

public class AuthenticationRepo : IAuthenticationRepo
{
    private readonly ConnectionFactory _factory;
    private readonly SqlQuery _query;
    public AuthenticationRepo(ConnectionFactory factory) {
        _factory = factory;
        _query = new SqlQuery(factory);
    }

    public void NewLogIn(string username, string password) {
        _query.SetData("INSERT INTO User_Login (UserName, PassWord) VALUES (@UName, @PWord);", 
                        new List<string> {"@UName", "@PWord"}, new List<string> {username, password});
        NewProfile(username);
        return;
    }
    public void NewProfile(string username) {
        _query.SetData("INSERT INTO User_Profiles (UserName, FirstName, LastName, TroopCount) VALUES (@UName, @FName, @LName, @TroopCount);", 
                        new List<string> {"@UName", "@FName", "@LName", "@TroopCount"}, new List<string> {username, "N/A", "N/A", "0"});
        return;
    }

    public bool UsernameExists(string username) {
        string? user = _query.GetValue($"SELECT * FROM User_Login WHERE UserName = '{username}';", "Username");
        if (user == null)   return false;
        else    return true;
    }
    public string? GetHash(string username) {
        string? pass = _query.GetValue($"SELECT * FROM User_Login WHERE UserName = '{username}';", "Password");
        if (pass == null)   return null;
        else    return pass;
    }
    public int UserId(string username) {
        string? id = _query.GetValue($"SELECT * FROM User_Profiles WHERE UserName = '{username}';", "ID");
        if (id == null) return 0;
        else    return int.Parse(id);
    }

    public void InitialTroops(int id) {
        _query.SetData($"UPDATE User_Profiles SET TroopCount = @NewCount WHERE ID = @ID;",
                new List<string> {"@NewCount", "@ID"}, new List<string> {Game.StartingTroops.ToString(), id.ToString()});
        return;
    }
    public void UpdateUsername(string oldUsername, string newUsername) {
        _query.SetData("UPDATE User_Login SET UserName = @NewUname WHERE UserName = @OldUname;",
                        new List<string> {"@OldUname", "@NewUname"}, new List<string> {oldUsername, newUsername});
        _query.SetData("UPDATE User_Profiles SET UserName = @NewUname WHERE UserName = @OldUname;",
                        new List<string> {"@OldUname", "@NewUname"}, new List<string> {oldUsername, newUsername});
        // This function only works when the foreign key constraint is not enforced between Login & Profiles table!!
        // NEED TO: Ignore Foreign Key constraint between User_Login & User_Profiles tables,
                    //then make update to username in BOTH tables,
                    //then re-establish the key constraint
        return;
    }
    public void UpdatePassword(string username, string newPassword) {
        _query.SetData("UPDATE User_Login SET PassWord = @NewPass WHERE UserName = @UName",
                        new List<string> {"@NewPass", "@UName"}, new List<string> {newPassword, username});
        return;
    }
}