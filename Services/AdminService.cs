using DataAccess;

namespace Services;
public class AdminService
{
    private readonly IAdminRepo _adminRepo;
    public AdminService(){
        _adminRepo = new AdminRepo();
    }
    // Constructor used for mocking in unit testing
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