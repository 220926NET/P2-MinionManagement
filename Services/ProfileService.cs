using Models;
using DataAccess;

namespace Services;
public class ProfileService
{
    private readonly IProfileRepo _repo;
    public ProfileService(IProfileRepo repo) {
        _repo = repo;
    }

    public User? ProfileInfo(int userId) {
        User? user = _repo.GetProfile(userId);
        return user;
    }

    public User AccountsInfo(User user, int userId) {
        user.CheckingAccounts = _repo.GetAccounts(userId, "checking");
        user.SavingAccounts = _repo.GetAccounts(userId, "saving");
        return user;
    }
}