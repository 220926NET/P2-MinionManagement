namespace Models;

public class User
{
    public string Username { get; private set; }
    public string ProfilePic { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int TroopCount { get; set; }
    
    public Tuple<int, decimal> CheckingAccounts { get; set; }
    public Tuple<int, decimal> SavingAccounts { get; set; }

    public User(string uname, string fname, string lname, int troops) {
        Username = uname;
        FirstName = fname;
        LastName = lname;
        TroopCount = troops;
        CheckingAccounts = new Tuple<int, decimal>(0, 0.00m);
        SavingAccounts = new Tuple<int, decimal>(0, 0.00m);
        ProfilePic = $"https://robohash.org/set_set2/{Username}.png";
    }
}