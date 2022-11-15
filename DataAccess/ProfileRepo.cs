using Models;

namespace DataAccess;

public class ProfileRepo : IProfileRepo
{
    private readonly ConnectionFactory _factory;
    private readonly SqlQuery _query;
    public ProfileRepo(ConnectionFactory factory) {
        _factory = factory;
        _query = new SqlQuery(factory);
    }

    public User? GetProfile(int id) {
        List<string>? info = _query.GetData($"SELECT * FROM User_Profiles WHERE ID = {id};", new List<string> {"UserName", "FirstName", "LastName", "TroopCount"});
        if (info == null)   return null;

        return new User(info[0], info[1], info[2], int.Parse(info[3]));
    }
    public Dictionary<int, decimal> GetAccounts(int id, string type) {
        List<string>? info = _query.GetData($"SELECT * FROM System_Accounts WHERE UserID = {id} AND AccountType = '{type}';", new List<string> {"Number", "Amount"});
        Dictionary<int, decimal> accounts = new Dictionary<int, decimal>();
        
        if (info != null) {
            for (int i = 0; i < info.Count(); i += 2) {
                accounts.Add(int.Parse(info[i]), decimal.Parse(info[i+1]));
            }
        }
        return accounts;
    }
}