using Models;
using DataAccess;

namespace Services;


public class AdminDistributeMoneyService{


    private AdminAddMoneyRepo _adminRepo;

    public AdminDistributeMoneyService(){
        
        _adminRepo = new AdminAddMoneyRepo();
    }

    public int DistributeMoneyToAllUsers(double amount){

        // call Distribute Money from Repo
        return _adminRepo.AddMoney(amount);
    }
    

}