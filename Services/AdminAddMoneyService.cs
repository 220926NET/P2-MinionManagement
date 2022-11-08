using DataAccess;

namespace Services;
public class AdminAddMoneyService
{
    private readonly AdminAddMoneyRepo _adminRepo;
    public AdminAddMoneyService(){
        _adminRepo = new AdminAddMoneyRepo();
    }

    public int AddMoneyToAllUsers(double amount){
        // call Add Money from Repo
        return _adminRepo.AddMoney(amount);
    }
}