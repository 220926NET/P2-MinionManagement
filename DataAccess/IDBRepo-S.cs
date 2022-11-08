
namespace DataAccess;

public interface IDBRepo
{
    /* Create */
    bool? NewTransaction(int sender, int receiver, decimal amount);

    /* Read */
    decimal? GetAmount(int account);

    /* Update */
    bool UpdateAccountAmount(int account, decimal amountDiff);
}