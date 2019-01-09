using System.Collections.Generic;
using BetBet.Model;

namespace BetBet.Data
{
    public interface IBetRepository : IRepository<Bet, Bet>
    {
        int CheckBet(int matchID, int userID);
        bool Delete(int id);
        int DeleteBetValidation(int userID, int betID);
        List<Bet> GetBetsByMatch(FinishedMatch match);
        List<Bet> GetBetsByUser(User user);
    }
}