using Models;
using DataAccess;

namespace Services;
public class AccountTransactionService{

    private AccountAmountDatabase _accountRepo;
    public AccountTransactionService(){

        _accountRepo = new AccountAmountDatabase();
    }

    // function to update account amount by transaction
    public int ToUpdateAccountAmount(Transaction transaction){

        // function to check accont type
        // return true if both checking account OR same UserID with different account type
        if(ToCheckAccountType(transaction)){

            // call function from DB layer AND return affected rows
            int returnAffectedRow = _accountRepo.UpdateAccountAmount(transaction);
            return returnAffectedRow;
        }
        else{

            // Invalid transaction between account type
            return -1;
        }

        
    }

    public bool ToCheckAccountType(Transaction transaction){

        // contain list look like [type1, UserID1, type2, UserID2]
        List<string> accountTypeAndIDList = _accountRepo.CheckAccountType(transaction);

        // check if only contain checking account
        if(accountTypeAndIDList.Contains("checking") & !accountTypeAndIDList.Contains("saving")){
            return true;
        }
        // check if contain both checking and saving account
        else if(accountTypeAndIDList.Contains("checking") & accountTypeAndIDList.Contains("saving")){
            
            // check if two UserId are same
            if(accountTypeAndIDList[1] == accountTypeAndIDList[3]){
                return true;
            }
            else{
                return false;
            }
        }
        else{
            return false;
        }
    }
}
