using Models;
using DataAccess;

namespace Services;
public class AccountService
{
    private IAccountRepo _repo;
    public AccountService(IAccountRepo repo) {
        _repo = repo;
    }

    public bool? TransferMoney(int sendingAccount, int receivingAccount, decimal amount) {
        // Validating that accounts exist
        if (!_repo.ChangeAmount(sendingAccount, Decimal.Negate(amount))) 
            return null;
        if (!_repo.ChangeAmount(receivingAccount, amount)) {
            _repo.ChangeAmount(sendingAccount, amount);
            return false;
        }

        return _repo.NewTransaction(sendingAccount, receivingAccount, amount);
    }

    public List<Tuple<int, decimal>> Transactions(int accountNumber, int userId) {
        List<Tuple<int, decimal>> output = new();
        // Verify that user owns account
        if (_repo.OwnerID(accountNumber) == userId) {
            Dictionary<int, Tuple<int, decimal>> deposits = _repo.GetTransactions(accountNumber, false);
            List<int> transactionIds = deposits.Keys.ToList();

            Dictionary<int, Tuple<int, decimal>> withdraws = _repo.GetTransactions(accountNumber, true);
            transactionIds.AddRange(withdraws.Keys.ToList());

            transactionIds.Sort();
            foreach (int i in transactionIds) {
                if (deposits.ContainsKey(i))
                    output.Add(deposits[i]);
                else
                    output.Add(withdraws[i]);
            }
        }
        return output;
    }

    public Tuple<int, string> Opponent(int userId) {
        Tuple<int, string> opponentInfo = new Tuple<int, string>(0, "");
        int? playerTroops = _repo.GetTroops(userId);

        if (playerTroops != null) {
            int? opponent = _repo.FindOpponent(userId, (int) playerTroops);
            if (opponent != null)   opponentInfo = new Tuple<int, string>((int) opponent, Game.CompareTroops((int) playerTroops, (int) _repo.GetTroops((int) opponent)!));
        }
        return opponentInfo;
    }

    public Tuple<bool, int, decimal> Raid(int user, int opponent) {
        Tuple<bool, int, decimal> result = new Tuple<bool, int, decimal>(false, 0, 0.0m);
        int? userTroops = _repo.GetTroops(user);
        int? opposingTroops = _repo.GetTroops(opponent);
        int userAccount = _repo.GetChecking(user);
        int opponentAccount = _repo.GetChecking(opponent);

        if (userTroops != null && opposingTroops != null && userAccount != 0 && opponentAccount != 0) {
            bool win = false;
            bool? transfer = false;
            decimal money = 0.00m;

            if (userTroops >= opposingTroops) {  // Won raid
                win = true;
                money = (decimal) _repo.GetAmount(opponentAccount)!;
                transfer = TransferMoney(opponentAccount, userAccount, money);

                _repo.AddDeed(user);
            }
            else {  // Lost raid
                win = false;
                money = (decimal) _repo.GetAmount(userAccount)!;
                transfer = TransferMoney(userAccount, opponentAccount, money);
            }

            if (transfer == true) { // Transaction succeeded
                int troopsLost = Game.LostTroops(win, (int) userTroops);
                _repo.UpdateTroops(user, (int)userTroops - troopsLost);

                result = new Tuple<bool, int, decimal>(win, troopsLost, money);
            }
        }
        return result;
    }

    public Tuple<decimal, int, int> MonthlyReport(int userId) {
        Tuple<decimal, int, int> report = new Tuple<decimal, int, int>(0.00m, 0, 0);
        Tuple<decimal, int> incomeInfo = MonthIncome(userId);
        Tuple<decimal, int> expenseInfo = MonthExpenses(userId);
        int checkingAccount = _repo.GetChecking(userId);

        if (incomeInfo != new Tuple<decimal, int>(0.00m, 0) && expenseInfo != new Tuple<decimal, int>(0.00m, 0) && checkingAccount != 0) {
            decimal monthChange = incomeInfo.Item1 - expenseInfo.Item1;
            decimal remainingMoney = (decimal)_repo.GetAmount(checkingAccount)! + monthChange;

            int unpaidTroops = 0;
            while (remainingMoney < 0.00m) {
                unpaidTroops++;
                remainingMoney += Game.TroopMonthlyCost;
                monthChange += Game.TroopMonthlyCost;
            }

            // Player loses any troops they could not afford to pay for the month
            if (unpaidTroops > 0) {
                _repo.UpdateTroops(userId, expenseInfo.Item2 - unpaidTroops);
            }

            if (_repo.NewTransaction(Game.DarkLordCheckingAccount, checkingAccount, incomeInfo.Item1)) {        // Record income for month
                if (_repo.NewTransaction(checkingAccount, Game.DarkLordCheckingAccount, (expenseInfo.Item1 - (unpaidTroops * Game.TroopMonthlyCost)))) {   // Record expenses for month
                    if (_repo.ChangeAmount(checkingAccount, monthChange)) {     // Update user's checking account
                        report = new Tuple<decimal, int, int>(monthChange, incomeInfo.Item2, unpaidTroops);
                    }
                }
            }
        }
        return report;
    }

    public Tuple<decimal, int> MonthIncome(int userId) {
        Tuple<decimal, int> incomeInfo = new Tuple<decimal, int>(0.00m, 0);
        int? deeds = _repo.GetDeeds(userId);

        if (deeds != null) {
            decimal income = Game.MonthlyBaseIncome + ((int)deeds * Game.EvilDeedIncrease);
            _repo.ResetDeeds(userId);

            incomeInfo = new Tuple<decimal, int>(income, (int) deeds);
        }
        return incomeInfo;
    }

    public Tuple<decimal, int> MonthExpenses(int userId) {
        Tuple<decimal, int> expenseInfo = new Tuple<decimal, int>(0.00m, 0);
        int? troops = _repo.GetTroops(userId);

        if (troops != null) {
            decimal expenses = Game.TroopMonthlyCost * (int)troops;
            expenseInfo = new Tuple<decimal, int>(expenses, (int) troops);
        }
        return expenseInfo;
    }
}
