using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBet.Model;

namespace BetBet.Data
{
    public class MatchRepository : IRepository<Match>
    {
        BetBetDB database = new BetBetDB();
        BetBetDBGoede database2 = new BetBetDBGoede();
        public bool Create(Match match)
        {
            /*string createMatchCMD = $"INSERT INTO match (Date, IsFinished) VALUES ('{match.Date}', '{0}')";
            database.executeCMD(createMatchCMD);

            int matchID = GetID(match);

            string addParticipantsCMD = $"INSERT INTO matchparticipants (MatchID, TeamHome, TeamAway) VALUES ('{matchID}', '{match.HomeTeamID}', '{match.AwayTeamID}'";
            database.executeCMD(addParticipantsCMD);
            */
            database2.CreateMatch(match);

            return true;
        }

        public int GetID(Match match)
        {
            string command = $"SELECT MatchID FROM match WHERE Date  = '{match.Date}'";
            int id = database.getID(command);

            return id;
        }

        public bool Delete(Match match)
        {
            if (match != null)
            {
                int id = GetID(match);
                string command = $"DELETE * FROM match Where MatchID =  '{id}'";
                database.executeCMD(command);
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
