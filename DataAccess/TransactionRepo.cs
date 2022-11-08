namespace DataAccess;

public class TransactionRepo : ITransactionRepo
{
    public TransactionRepo() {}

    public bool NewTransaction(int sender, int receiver, decimal amount) {
        SqlQuery.SetData("INSERT INTO System_Transactions (SenderNumber, ReceiverNumber, Amount) VALUES (@Sender, @Receiver, @Amount);", 
                new List<string> { "@Sender", "@Receiver", "@Amount" }, new List<string> { sender.ToString(), receiver.ToString(), amount.ToString() });
        return true;
    }

    public decimal? GetAmount(int account) {
        string? currentAmount = SqlQuery.GetValue($"SELECT Amount FROM System_Accouts WHERE Number = {account};", "Amount");
        if (currentAmount == null)  return null;
        else    return decimal.Parse(currentAmount);
    }

    public bool UpdateAccountAmount(int account, decimal amountDiff) {
        decimal? currentAmount = GetAmount(account);
        if (currentAmount == null)  return false;

        decimal? newAmount = currentAmount + amountDiff;
        SqlQuery.SetData($"UPDATE System_Accounts SET Amount = @UpdatedAmount WHERE Number = {account};", 
                new List<string> { "@UpdatedAmount" }, new List<string> { newAmount.ToString()! });
        return true;
    }
}