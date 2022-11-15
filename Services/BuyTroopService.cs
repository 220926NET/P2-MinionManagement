using DataAccess;

namespace Services;

public class BuyTroopService
{
    private BuyTroopRepo _buyTroopRepo;
    public BuyTroopService(BuyTroopRepo buyTroopRepo){
        _buyTroopRepo = buyTroopRepo;
    }

    public int BuyTroopFunc(int userID, int amount){
        return _buyTroopRepo.BuyTroop(userID, amount);
    }
}