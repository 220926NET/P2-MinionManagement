using Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;

public class AccountCreateDatabase
{
    private ConnectionFactory _factory;

    public AccountCreateDatabase(){
        
        _factory = new ConnectionFactory();
    }


    public int CreateAccount(Account account){

        SqlConnection connection = _factory.AddConnection();
        connection.Open();

        // Insert New account to DB
        SqlCommand command = new SqlCommand($"INSERT INTO System_Accounts VALUES(@UserID, @Amount, @AccountType)", connection);

        // Adding fields of account
        command.Parameters.AddWithValue("@UserID", account.UserID);
        command.Parameters.AddWithValue("@Amount", account.Amonut);
        command.Parameters.AddWithValue("@AccountType", account.AccountType);

        int affectRow = command.ExecuteNonQuery();

        connection.Close();
        return affectRow;
    }

}