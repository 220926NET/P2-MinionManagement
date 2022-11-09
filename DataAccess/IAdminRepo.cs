namespace DataAccess;

public interface IAdminRepo
{
    /* Update */
    int AddMoney(decimal amount);
    int RemoveMoney(decimal amount);
}