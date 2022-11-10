using DataAccess;

namespace Services;
public class AdminService
{
    private readonly IAdminRepo _adminRepo;
    public AdminService(IAdminRepo repo) {
        _adminRepo = repo;
    }

    public int AddMoneyToAllUsers(decimal amount){
        // call Add Money from Repo
        return _adminRepo.AddMoney(amount);
    }

    public int RemoveMoneyFromAllUsers(decimal amount){
        // call Remove Money from Repo
        return _adminRepo.RemoveMoney(amount);
    }
}