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
            int isFinished = 0;
            matchList = matchrep.GetUpcomingMatches(isFinished);

            return matchList;
        }

        public UpcomingMatch GetUpcomingMatch(int matchID)
        {
            UpcomingMatch match;
            match = matchrep.GetUpcomingMatch(matchID);

            return match;
        }

        public FinishedMatch GetFinishedMatch(int matchID)
        {
            FinishedMatch match;
            match = matchrep.GetFinishedMatch(matchID);

            return match;
        }

        public List<FinishedMatch> GetFinishedMatches()
        {
            List<FinishedMatch> matchList = new List<FinishedMatch>();
            int isFinished = 1;
            matchList = matchrep.GetFinishedMatches(isFinished);

            return matchList;
        }

        public List<FinishedMatch> GetFinishedMatchesByTeam(int id)
        {
            int isFinished = 1;
            List<FinishedMatch> matchList = matchrep.GetFinishedMatches(isFinished);
            List<FinishedMatch> TeamMatchList = new List<FinishedMatch>();

            foreach(FinishedMatch match in matchList)
            {
                if (match.HomeTeam.TeamID == id || match.AwayTeam.TeamID == id)
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

            FinishedMatch finishedmatch = new FinishedMatch(match.MatchID, match.HomeTeam, match.AwayTeam, match.MultiplierHome,
                match.MultiplierAway, match.MultiplierDraw, match.Date, scoreHome, scoreAway, result);

            matchrep.Update(finishedmatch);
            teamservice.CalculatePoints(finishedmatch);
            betservice.Payout(finishedmatch);
        }
    }
}
