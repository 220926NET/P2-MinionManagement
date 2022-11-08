using Microsoft.Data.SqlClient;

namespace DataAccess;

public class AdminAddMoneyRepo
{
    private readonly ConnectionFactory _factory;

    public AdminAddMoneyRepo(){
        _factory = new ConnectionFactory();
    }

    public int AddMoney(double amount){
        SqlConnection connection = _factory.GetConnection();
        connection.Open();

        // add money to Users checking account amount who are not admin
        SqlCommand command = new SqlCommand($"UPDATE System_Accounts SET Amount = Amount + @amount WHERE AccountType = 'checking'AND UserID != (SELECT ID FROM User_Admin)", connection);
        command.Parameters.AddWithValue("@amount", amount);

        int affectRow = command.ExecuteNonQuery();

        // create records to transaction table for all users
        command.CommandText = $"INSERT INTO System_Transactions (SenderNumber, ReceiverNumber, Amount) SELECT ua.ID, sa.UserID, @amountTransaction FROM System_Accounts sa, User_Admin ua WHERE sa.AccountType = 'checking' AND sa.UserId != ua.ID";
        command.Parameters.AddWithValue("@amountTransaction", amount);
        affectRow += command.ExecuteNonQuery();

        connection.Close();
        return affectRow;
    }

}