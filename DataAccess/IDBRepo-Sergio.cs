using Models;

namespace DataAccess;

public interface IAuthenticationRepo
{
    /* Create */
    void NewLogIn(string username, string password);
    void NewProfile(string username);

    /* Read */
    bool UsernameExists(string username);
    string? GetHash(string username);
    int UserId(string username);

    /* Update */
    void UpdateUsername(string oldUsername, string newUsername);
    void UpdatePassword(string username, string newPassword);
}

public interface IProfileRepo
{
    /* Read */
    User? GetProfile(int id);
    Dictionary<int, decimal> GetAccounts(int id, string type);
}

public interface IAccountRepo
{
    /* Create */
    bool NewTransaction(int sender, int receiver, decimal amount);

    /* Read */
    int OwnerID(int account);
    int GetChecking(int profileId);
    decimal? GetAmount(int account);
    int? GetTroops(int profileId);
    Dictionary<int, Dictionary<int, decimal>> GetTransactions(int account, bool sender);
    int? FindOpponent(int profileId, int currentTroops);

    /* Update */
    bool ChangeAmount(int account, decimal amountDiff);
    void UpdateTroops(int id, int newTroops);
}