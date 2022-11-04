using Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;

public class AdminDistributeMoneyRepo
{
    private ConnectionFactory _factory;

    public AdminDistributeMoneyRepo(){
        
        _factory = new ConnectionFactory();
    }


    public int DistributeMoney(double amount){

        SqlConnection connection = _factory.AddConnection();
        connection.Open();

        // add money to Users checking account amount who is not admin
        SqlCommand command = new SqlCommand($"UPDATE System_Accounts SET Amount = Amount + @amount WHERE AccountType = 'checking'AND UserID != (SELECT ID FROM User_Admin)", connection);
        command.Parameters.AddWithValue("@amount", amount);

        int affectRow = command.ExecuteNonQuery();

        connection.Close();
        return affectRow;
    }

}