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

        public List<UpcomingMatch> GetUpcomingMatches()
        {
            List<UpcomingMatch> matchList = new List<UpcomingMatch>();
            matchList = matchrep.GetUpcomingMatches();

            return matchList;
        }

        public List<FinishedMatch> GetFinishedMatches()
        {
            List<FinishedMatch> matchList = new List<FinishedMatch>();
            matchList = matchrep.GetFinishedMatches();

            return matchList;
        }

        public FinishedMatch GenerateResult(UpcomingMatch match)
        {
            Random random = new Random();

            int scoreHome = random.Next(0, 5);
            int scoreAway = random.Next(0, 5);
            int cardsHome = random.Next(0, 4);
            int cardsAway = random.Next(0, 4);

            FinishedMatch finishedmatch = new FinishedMatch(match.MatchID, match.HomeTeamID, match.AwayTeamID, match.HomeTeamName, match.AwayTeamName, match.MultiplierHome,
                match.MultiplierAway, match.MultiplierDraw, match.Date, scoreHome, scoreAway, cardsHome, cardsAway);

            matchrep.AddFinishedMatch(finishedmatch);

            return finishedmatch;
        }
    }
}
