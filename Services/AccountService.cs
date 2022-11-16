using DataAccess;

namespace Services;
public class AccountService
{
    private IAccountRepo _repo;
    public AccountService(IAccountRepo repo) {
        _repo = repo;
    }

    public bool? TransferMoney(int sendingAccount, int receivingAccount, decimal amount) {
        // Validating that accounts exist
        if (!_repo.UpdateAmount(sendingAccount, Decimal.Negate(amount))) 
            return null;
        if (!_repo.UpdateAmount(receivingAccount, amount)) {
            _repo.UpdateAmount(sendingAccount, amount);
            return false;
        }

        return _repo.NewTransaction(sendingAccount, receivingAccount, amount);
    }

    public List<Dictionary<int, decimal>> Transactions(int accountNumber, int userId) {
        List<Dictionary<int, decimal>> output = new();
        // Verify that user owns account
        if (_repo.OwnerID(accountNumber) == userId) {
            Dictionary<int, Dictionary<int, decimal>> deposits = _repo.GetTransactions(accountNumber, false);
            List<int> transactionIds = deposits.Keys.ToList();

            Dictionary<int, Dictionary<int, decimal>> withdraws = _repo.GetTransactions(accountNumber, true);
            transactionIds.AddRange(withdraws.Keys.ToList());

            transactionIds.Sort();
            foreach (int i in transactionIds) {
                if (deposits.ContainsKey(i))
                    output.Add(deposits[i]);
                else
                    output.Add(withdraws[i]);
            }
        }
        return output;
    }
}
