namespace DataAccess;

public interface ITransactionRepo
{
    /* Create */
    bool NewTransaction(int sender, int receiver, decimal amount);

    /* Read */
    decimal? GetAmount(int account);

    /* Update */
    bool UpdateAccountAmount(int account, decimal amountDiff);
}

public interface IAuthenticationRepo
{
    /* Create */
    void NewLogIn(string username, string password);
    void NewProfile(string username);

    /* Read */
    bool UsernameExists(string username);
    string? GetHash(string username);
    int UserId(string username);
}