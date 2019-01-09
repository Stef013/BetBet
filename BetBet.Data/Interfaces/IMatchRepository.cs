using System.Collections.Generic;
using BetBet.Model;

namespace BetBet.Data
{
    public interface IMatchRepository : IRepository<Match, FinishedMatch>
    {
        FinishedMatch GetFinishedMatch(int matchID);
        List<FinishedMatch> GetFinishedMatches(int isFinished);
        UpcomingMatch GetUpcomingMatch(int matchID);
        List<UpcomingMatch> GetUpcomingMatches(int isFinished);
    }
}