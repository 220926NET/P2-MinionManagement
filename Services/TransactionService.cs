using DataAccess;

namespace Services;
public class TransactionService
{
    private ITransactionRepo _repo;
    public TransactionService(ITransactionRepo repo) {
        _repo = repo;
    }

    public bool? TransferMoney(int sendingAccount, int receivingAccount, decimal amount) {
        // Validating that accounts exist
        if (!_repo.UpdateAccountAmount(sendingAccount, Decimal.Negate(amount))) 
            return null;
        if (!_repo.UpdateAccountAmount(receivingAccount, amount)) {
            _repo.UpdateAccountAmount(sendingAccount, amount);
            return false;
        }

        return _repo.NewTransaction(sendingAccount, receivingAccount, amount);
    }
}
