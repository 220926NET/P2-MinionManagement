using DataAccess;

namespace Services;
public class TransactionService
{
    private IDBRepo _repo;
    public TransactionService() {
        _repo = new DBRepo();           // FOR dependency injection, COULD switch out with initialized factory??
    }

    public bool? TransferMoney(int sendingAccount, int receivingAccount, decimal amount) {
        return _repo.NewTransaction(sendingAccount, receivingAccount, amount);
    }
}
