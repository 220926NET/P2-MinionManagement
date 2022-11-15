using Microsoft.Data.SqlClient;

namespace DataAccess;

public class BuyTroopRepo
{
    private readonly ConnectionFactory _factory;

    public BuyTroopRepo(ConnectionFactory factory){
        _factory = factory;
    }

    public int BuyTroop(int userID, int amount){

        SqlConnection connection = _factory.GetConnection();
        connection.Open();

        // Deduct money from checking account
        SqlCommand command = new SqlCommand($"UPDATE System_Accounts SET Amount = Amount - @amount WHERE UserID = @userID AND AccountType= 'checking' AND Amount >= @amount", connection);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@userID", userID);

        int affectRow = command.ExecuteNonQuery();
        if(affectRow == 1){
            // Add troops
            command.CommandText = $"UPDATE User_Profiles SET TroopCount = TroopCount + @amount WHERE id = @userID";
            //command.Parameters.AddWithValue("@amount", amount);
            //command.Parameters.AddWithValue("@userID", userID);

            affectRow = command.ExecuteNonQuery();

            if(affectRow == 1){

                //Update transaction record
                command.CommandText = $"INSERT INTO System_Transactions VALUES((SELECT Number FROM System_Accounts WHERE UserID = @userID AND AccountType = 'checking'), 1, @amount)";
                //command.Parameters.AddWithValue("@amount", amount);
                //command.Parameters.AddWithValue("@userID", userID);

                affectRow = command.ExecuteNonQuery();
                if(affectRow == 1){
                    return 1;
                }
                else{
                    return -1;
                }
            }
            else{
                return -1;
            }
        }
        else{
            return -1;
        }
    }

}