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
        TeamService teamservice;
        BetService betservice;

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

        public UpcomingMatch GetUpcomingMatch(int matchID)
        {
            UpcomingMatch match;
            match = matchrep.GetUpcomingMatch(matchID);

            return match;
        }

        public List<FinishedMatch> GetFinishedMatches()
        {
            List<FinishedMatch> matchList = new List<FinishedMatch>();
            matchList = matchrep.GetFinishedMatches();

            return matchList;
        }

        public List<FinishedMatch> GetFinishedMatchesByTeam(int id)
        {
            List<FinishedMatch> matchList = matchrep.GetFinishedMatches();
            List<FinishedMatch> TeamMatchList = new List<FinishedMatch>();

            foreach(FinishedMatch match in matchList)
            {
                if (match.HomeTeamID == id || match.AwayTeamID == id)
                {
                    TeamMatchList.Add(match);
                }                
            }

            return TeamMatchList;
        }

        public void GenerateResult(UpcomingMatch match)
        {
            teamservice = new TeamService();
            betservice = new BetService();

            Random random = new Random();

            int scoreHome = random.Next(0, 5);
            int scoreAway = random.Next(0, 5);

            MatchResult result;

            if (scoreHome > scoreAway)
            {
                result = MatchResult.HomeTeam;
            }
            else if(scoreHome < scoreAway)
            {
                result = MatchResult.AwayTeam;
            }
            else
            {
                result = MatchResult.Draw;
            }

            FinishedMatch finishedmatch = new FinishedMatch(match.MatchID, match.HomeTeamID, match.AwayTeamID, match.HomeTeamName, match.AwayTeamName, match.MultiplierHome,
                match.MultiplierAway, match.MultiplierDraw, match.Date, scoreHome, scoreAway, result);

            matchrep.AddFinishedMatch(finishedmatch);
            teamservice.CalculatePoints(finishedmatch);
            betservice.Payout(finishedmatch);
        }
    }
}
