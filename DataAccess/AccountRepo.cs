namespace DataAccess;

public class AccountRepo : IAccountRepo
{
    private readonly ConnectionFactory _factory;
    private readonly SqlQuery _query;
    public AccountRepo(ConnectionFactory factory) {
        _factory = factory;
        _query = new SqlQuery(factory);
    }

    public bool NewTransaction(int sender, int receiver, decimal amount) {
        _query.SetData("INSERT INTO System_Transactions (SenderNumber, ReceiverNumber, Amount) VALUES (@Sender, @Receiver, @Amount);", 
                new List<string> { "@Sender", "@Receiver", "@Amount" }, new List<string> { sender.ToString(), receiver.ToString(), amount.ToString() });
        return true;
    }

    public int OwnerID(int account) {
        string? id = _query.GetValue($"SELECT UserID FROM System_Accounts WHERE Number = {account};", "UserID");
        if (id == null) return 0;
        else    return int.Parse(id);
    }
    public decimal? GetAmount(int account) {
        string? currentAmount = _query.GetValue($"SELECT Amount FROM System_Accounts WHERE Number = {account};", "Amount");
        if (currentAmount == null)  return null;
        else    return decimal.Parse(currentAmount);
    }
    public Dictionary<int, Dictionary<int, decimal>> GetTransactions(int account, bool sender) {    // COULD also use Tuple???
        List<string>? data = _query.GetData($"SELECT * FROM System_Transactions WHERE {(sender ? $"SenderNumber = {account}" : $"ReceiverNumber = {account}")}", 
                                                new List<string> {"ID", (sender ? "ReceiverNumber" : "SenderNumber"), "Amount"});
        Dictionary<int, Dictionary<int, decimal>> transfers = new();
        if (data != null) {
            for (int i = 0; i < data.Count(); i += 3) {
                decimal amount = decimal.Parse(data[i+2]);
                transfers.Add(int.Parse(data[i]), new Dictionary<int, decimal>(){{int.Parse(data[i+1]), (sender ? Decimal.Negate(amount) : amount)}});
            }
        }
        return transfers;
    }

    public bool UpdateAmount(int account, decimal amountDiff) {
        decimal? currentAmount = GetAmount(account);
        if (currentAmount == null)  return false;

        decimal? newAmount = currentAmount + amountDiff;
        _query.SetData($"UPDATE System_Accounts SET Amount = @UpdatedAmount WHERE Number = {account};", 
                new List<string> { "@UpdatedAmount" }, new List<string> { newAmount.ToString()! });
        return true;
    }
}