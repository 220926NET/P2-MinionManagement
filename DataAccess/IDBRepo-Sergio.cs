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
    void InitialTroops(int id);
    void UpdateUsername(string oldUsername, string newUsername);
    void UpdatePassword(string username, string newPassword);
}

public interface IProfileRepo
{
    /* Read */
    User? GetProfile(int id);
    Tuple<int, decimal> GetAccounts(int id, string type);

    /* Update */
    void UpdateProfile(int id, User changed);
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
    int? GetDeeds(int id);
    Dictionary<int, Tuple<int, decimal>> GetTransactions(int account, bool sender);
    int? FindOpponent(int profileId, int currentTroops);

    /* Update */
    bool ChangeAmount(int account, decimal amountDiff);
    void UpdateTroops(int id, int newTroops);
    void AddDeed(int id);
    void ResetDeeds(int id);
}