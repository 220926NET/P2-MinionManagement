using Microsoft.Data.SqlClient;
using Models;

namespace DataAccess;

public class BuyTroopRepo
{
    private readonly ConnectionFactory _factory;

    public BuyTroopRepo(ConnectionFactory factory){
        _factory = factory;
    }

    public int BuyTroop(int userID, int numOfTroop){

        SqlConnection connection = _factory.GetConnection();
        connection.Open();

        // Deduct money from checking account
        // setting each troop cost $10
        int troopCost = (int) Game.TroopBuyAmount;
        SqlCommand command = new SqlCommand($"UPDATE System_Accounts SET Amount = Amount - {troopCost}*@numOfTroop WHERE UserID = @userID AND AccountType= 'checking' AND Amount >= {troopCost}*@numOfTroop", connection);
        command.Parameters.AddWithValue("@numOfTroop", numOfTroop);
        command.Parameters.AddWithValue("@userID", userID);

        int affectRow = command.ExecuteNonQuery();
        if(affectRow == 1){
            // Add troops
            command.CommandText = $"UPDATE User_Profiles SET TroopCount = TroopCount + @numOfTroop WHERE id = @userID";
            

            affectRow = command.ExecuteNonQuery();

            if(affectRow == 1){

                //Update transaction record
                command.CommandText = $"INSERT INTO System_Transactions VALUES((SELECT Number FROM System_Accounts WHERE UserID = @userID AND AccountType = 'checking'), 1, {troopCost}*@numOfTroop)";
                

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