using Models;
using DataAccess;

namespace Services;

public class AdminRemoveMoneyService{

    private readonly AdminRemoveMoneyRepo _adminRepo;

    public AdminRemoveMoneyService(){

        _adminRepo = new AdminRemoveMoneyRepo();
    }

    public int RemoveMoneyFromAllUsers(double amount){

        // call Remove Money from Repo
        return _adminRepo.RemoveMoney(amount);
    }
}