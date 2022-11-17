using DataAccess;

namespace Services;
public class BuyTroopService
{
    private readonly BuyTroopRepo _buyTroopRepo;
    public BuyTroopService(BuyTroopRepo buyTroopRepo){
        _buyTroopRepo = buyTroopRepo;
    }

    public int BuyTroopFunc(int userID, int numOfTroop){
        return _buyTroopRepo.BuyTroop(userID, numOfTroop);
    }
}