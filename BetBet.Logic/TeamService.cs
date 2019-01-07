using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBet.Data;
using BetBet.Model;

namespace BetBet.Logic
{
    public class TeamService
    {
        TeamRepository TeamRep = new TeamRepository();

        public Team GetTeam(int id)
        {
            Team team = new Team();
            team = TeamRep.GetTeam(id);

            return team;
        }

        public List<Team> GetTeams ()
        {
            List<Team> TeamList = new List<Team>();

            TeamList = TeamRep.GetTeams();

            return TeamList;
        }

        public void CalculatePoints(FinishedMatch match)
        {
            int homePoints = 0;
            int awayPoints = 0;
            int homeWin = 0;
            int awayWin = 0;
            int homeLoss = 0;
            int awayLoss = 0;
            int draw = 0;

            if (match.ScoreHome > match.ScoreAway)
            {
                homePoints = 3;
                homeWin = 1;
                awayLoss = 1;
            }
            else if(match.ScoreHome < match.ScoreAway)
            {
                awayPoints = 3;
                awayWin = 1;
                homeLoss = 1;
            }
            else if (match.ScoreHome == match.ScoreAway)
            {
                homePoints = 1;
                awayPoints = 1;
                draw = 1;
            }

            Team hometeam = new Team
            {
                TeamID = match.HomeTeam.TeamID,
                GoalsFor = match.ScoreHome,
                GoalsAgainst = match.ScoreAway,
                GamesWon = homeWin,
                GamesLost = homeLoss,
                Draws = draw,
                Points = homePoints
            };

            Team awayteam = new Team
            {
                TeamID = match.AwayTeam.TeamID,
                GoalsFor = match.ScoreAway,
                GoalsAgainst = match.ScoreHome,
                GamesWon = awayWin,
                GamesLost = awayLoss,
                Draws = draw,
                Points = awayPoints
            };

            TeamRep.Update(hometeam);
            TeamRep.Update(awayteam);
        }

    }
}
