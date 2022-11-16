namespace Models;

public static class Game
{
    public static decimal StartingMoney = 200.00m;
    public static int StartingTroops = 20;
    public static int DarkLordCheckingAccount = 1;
    public static decimal TroopBuyAmount = 10.00m;
    public static decimal TroopMonthlyCost = 5.00m;
    public static decimal MonthlyBaseIncome = 100.00m;
    public static decimal EvilDeedIncrease = 50.00m;
    public static int OpponentTroopRange = 10;
    public static List<int> WinTroopLossRange = new List<int>{0, 25};
    public static List<int> LoseTroopLossRange = new List<int>{50, 75};

    public static int RandomOpponentRange() {
        Random rand = new Random();
        return rand.Next(OpponentTroopRange + 1);
    }

    public static string CompareTroops(int ownedTroops, int opposingTroops) {
        if (opposingTroops > (ownedTroops + 5))   return "larger";
        else if (opposingTroops >= (ownedTroops - 5) && opposingTroops <= (ownedTroops + 5))   return "similar";
        else if (opposingTroops < (ownedTroops - 5))    return "smaller";
        else    return "undeterminable";
    }

    public static int LostTroops(bool won, int currentTroops) {
        List<int> lossRange;
        if (won)    lossRange = WinTroopLossRange;
        else    lossRange = LoseTroopLossRange;

        Random rand = new Random();
        double lossPercentage = rand.Next(lossRange[0], (lossRange[1] + 1)) / 100.00;
        int troopsLost = (int) Math.Round(currentTroops * lossPercentage);

        return troopsLost;
    }
}