using Microsoft.Data.SqlClient;

namespace DataAccess;

public class DBRepo : IDBRepo
{
    private ConnectionFactory _factory;
    public DBRepo() {
        _factory = new ConnectionFactory();
    }

    private string? GetValue(string query, string column) {
        string? value = null;
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) {
                if (reader.Read()) {
                    // if (reader[column].GetType() == typeof(Int32))
                    //     value = reader[column].ToString();
                    // else    
                        value = (string) reader[column];
                }
            }

            connection.Close();
        }
        catch (SqlException ex)
        {
            // SeriLog
        }
        return value;
    }
    private List<string>? GetData(string query, List<string> columns) {
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) {
                List<string> values = new List<string>();

                while (reader.Read()) {
                    foreach (string column in columns) {
                        // if (reader[column].GetType() == typeof(DBNull)) continue;
                        // else if (reader[column].GetType() == typeof(Int32)) {
                        //     values.Add(reader[column].ToString());
                        // }
                        // else if (reader[column].GetType() == typeof(Decimal)) {
                        //     values.Add(reader[column].ToString());
                        // }
                        // else
                        values.Add((string) reader[column]);
                    }
                }
                connection.Close();
                return values;
            }
            connection.Close();
        }
        catch (SqlException ex)
        {
            // SeriLog
        }
        return null;
    }
    private void SetData(string command, List<string> parameters, List<string> values) {
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            using SqlCommand cmd = new SqlCommand(command, connection);

            for (int i = 0; i < parameters.Count; i++) {
                cmd.Parameters.AddWithValue(parameters[i], values[i]);
            }

            cmd.ExecuteNonQuery();
            connection.Close();
        }
        catch (SqlException ex)
        {
            // SeriLog
        }
        return;
    }

    public bool? NewTransaction(int sender, int receiver, decimal amount) {
        // Validating that accounts exist (unnecessary?)
        // string? userSending = GetValue($"SELECT UserID FROM System_Accounts WHERE Number = {sender};", "UserID");
        // string? userReceiving = GetValue($"SELECT UserID FROM System_Accounts WHERE Number = {sender};", "UserID");
        // if (userSending == null || userReceiving == null)   return false;

        if (!UpdateAccountAmount(sender, Decimal.Negate(amount))) return null;
        if (!UpdateAccountAmount(receiver, amount)) return false;

        SetData("INSERT INTO System_Transactions (SenderNumber, ReceiverNumber, Amount) VALUES (@Sender, @Receiver, @Amount);", 
                new List<string> { "@Sender", "@Receiver", "@Amount" }, new List<string> { sender.ToString(), receiver.ToString(), amount.ToString() });
        return true;
    }

    public decimal? GetAmount(int account) {
        string? currentAmount = GetValue($"SELECT Amount FROM System_Accouts WHERE Number = {account};", "Amount");
        if (currentAmount == null)  return null;
        else    return decimal.Parse(currentAmount);
    }

    public bool UpdateAccountAmount(int account, decimal amountDiff) {
        decimal? currentAmount = GetAmount(account);
        if (currentAmount == null)  return false;

        decimal? newAmount = currentAmount + amountDiff;
        SetData($"UPDATE System_Accounts SET Amount = @UpdatedAmount WHERE Number = {account};", 
                new List<string> { "@UpdatedAmount" }, new List<string> { newAmount.ToString()! });
        return true;
    }
}