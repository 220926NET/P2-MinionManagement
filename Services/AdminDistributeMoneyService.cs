using Models;
using DataAccess;

namespace Services;


public class AdminDistributeMoneyService{


    private AdminDistributeMoneyRepo _adminRepo;

    public AdminDistributeMoneyService(){
        
        _adminRepo = new AdminDistributeMoneyRepo();
    }

    public int DistributeMoneyToAllUsers(double amount){

        // call Distribute Money from Repo
        return _adminRepo.DistributeMoney(amount);
    }
    

}