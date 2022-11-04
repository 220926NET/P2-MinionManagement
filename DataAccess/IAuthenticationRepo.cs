namespace DataAccess;

public interface IAuthenticationRepo
{
    /* Create */
    void NewLogIn(string username, string password);
    void NewProfile(string username);

    /* Read */
    bool UsernameExists(string username);
    bool VerifyCredentials(string username, string password);
    int UserId(string username);
}