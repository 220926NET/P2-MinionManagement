using Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;
public class AccountAmountDatabase
{
    

    private ConnectionFactory _factory;

    public AccountAmountDatabase(){

        _factory = new ConnectionFactory();
        
    }

    

    public int UpdateAccountAmount(Transaction transaction){

        SqlConnection connection = _factory.AddConnection();
        connection.Open();

        // Update Account Amount for sender by subtracting Amount of transaction
        SqlCommand command = new SqlCommand($"UPDATE System_Accounts SET Amount = Sa.Amount-St.Amount FROM System_Accounts Sa, System_Transactions St WHERE Sa.Number = {transaction.SenderAccountNumber};", connection);
        int affectRow = command.ExecuteNonQuery();

        // Update Account Amount for receiver by adding Amount of transaction 
        command.CommandText = $"UPDATE System_Accounts SET Amount = Sa.Amount+St.Amount FROM System_Accounts Sa, System_Transactions St WHERE Sa.Number = {transaction.ReceiverAccountNumber};";

        // Two command should return two affected rows
        affectRow  += command.ExecuteNonQuery();

        connection.Close();
        return affectRow;


    }

    public  List<string> CheckAccountType(Transaction transaction){

        SqlConnection connection = _factory.AddConnection();
        connection.Open();

        // Select Account Type and UserID affected by transaction
        SqlCommand command = new SqlCommand($"SELECT  Sa.AccountType, Sa.UserID From System_Accounts Sa, System_Transactions St WHERE Sa.Number = {transaction.ReceiverAccountNumber} OR Sa.Number = {transaction.SenderAccountNumber}", connection);
        SqlDataReader reader = command.ExecuteReader();
        
        // List to store information look like [accountType1, UserId1, accountType2, UserID2]
        List<string> accountTypeAndIDList = new List<string>();


        if(reader.HasRows){

            while(reader.Read()){

                // Read for DB store them all as string for convenience
                string returnAccountType = (string)reader["AccountType"];
                string returnUserID = ((int)reader["UserID"]).ToString();
                
                // Add them to lsit
                accountTypeAndIDList.Add(returnAccountType);
                accountTypeAndIDList.Add(returnUserID);
                 
            }
        }

        connection.Close();
        return accountTypeAndIDList;
        

    }
}
                         