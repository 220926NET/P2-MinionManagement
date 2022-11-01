using Models;
using DataAccess;

namespace Services;


public class AccountCreateService{


    private AccountCreateDatabase _accountRepo;

    public AccountCreateService(){
        
        _accountRepo = new AccountCreateDatabase();
    }

    public int ToCreateAccount(Account account){

        // Call CreateAccount function from DB
        // Return number of affected row
        int returnAffectedRow = _accountRepo.CreateAccount(account);
        return returnAffectedRow;
    }
    

}