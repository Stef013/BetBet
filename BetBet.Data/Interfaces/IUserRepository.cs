using BetBet.Model;

namespace BetBet.Data
{
    public interface IUserRepository : IRepository<User, User>
    {
        void ChangePassword(int userID, string password);
        bool ComparePassword(string username, string password);
        decimal GetBalance(int userID);
        bool GetIsAdmin(User user);
    }
}