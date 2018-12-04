using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBet.Data;
using BetBet.Model;

namespace BetBet.Logic
{
    public class MatchService
    {
        MatchRepository matchrep = new MatchRepository();

        public bool CreateMatch(Match match)
        {
            bool result = matchrep.Create(match);
            return result;
        }

        public List<UpcomingMatch> getUpcomingMatches()
        {
            List<UpcomingMatch> matchList = new List<UpcomingMatch>();
            matchList = matchrep.GetUpcomingMatches();

            return matchList;
        }
    }
}
