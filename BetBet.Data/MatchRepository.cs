using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BetBet.Model;

namespace BetBet.Data
{
    public class MatchRepository : IRepository<Match>
    {
        BetBetDB database = new BetBetDB();
        
        public bool Create(Match match)
        {
            bool result;

            int matchExists = GetID(match);

            if (matchExists == 0)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                string createMatchCMD = $"INSERT INTO matches (Date, MultiplierHome, MultiplierAway, MultiplierDraw, IsFinished) VALUES " +
                    $"('{match.Date.ToString("yyyy-MM-dd")}','{match.MultiplierTeamHome.ToString(CultureInfo.InvariantCulture)}','{match.MultiplierTeamAway.ToString(CultureInfo.InvariantCulture)}','{match.MultiplierDraw.ToString(CultureInfo.InvariantCulture)}','{0}')";

                int matchID = database.ExecuteAndGetID(createMatchCMD);

                if (matchID != 0)
                {
                    string addParticipantsCMD = $"INSERT INTO matchparticipants (MatchID, TeamHome, TeamAway) VALUES ('{matchID}','{match.HomeTeamID}','{match.AwayTeamID}')";
                    database.ExecuteCMD(addParticipantsCMD);
                    result = true;
                }

                else
                {
                    result = false;
                }
               
            }
            else
            {
                result = false;
            }
            
            return result;
        }

        public int GetID(Match match)
        {
            string command = $"SELECT MatchID FROM matchparticipants WHERE HomeTeam = '{match.HomeTeamID}' AND AwayTeam = {match.AwayTeamID}'";
            int id = database.getID(command);

            return id;
        }

        public bool Delete(Match match)
        {
            if (match != null)
            {
                int id = GetID(match);
                string command = $"DELETE * FROM match Where MatchID =  '{id}'";
                database.ExecuteCMD(command);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Match match)
        {
            //---------------------------------- moet nog code bij------------------------------------------------
            return true;
        }
    }
}
