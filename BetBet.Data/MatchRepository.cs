using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BetBet.Model;
using MySql.Data.MySqlClient;

namespace BetBet.Data
{
    public class MatchRepository : IRepository<Match>
    {
        BetBetDB database = new BetBetDB();
        TeamRepository teamrep = new TeamRepository();
        
        public bool Create(Match match)
        {
            bool result;

            int matchExists = GetID(match);

            if (matchExists == 0)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                string createMatchCMD = $"INSERT INTO matches (Date, MultiplierHome, MultiplierAway, MultiplierDraw, IsFinished) VALUES " +
                    $"('{match.Date.ToString("yyyy-MM-dd")}','{match.MultiplierHome.ToString(CultureInfo.InvariantCulture)}','{match.MultiplierAway.ToString(CultureInfo.InvariantCulture)}','{match.MultiplierDraw.ToString(CultureInfo.InvariantCulture)}','{0}')";

                int matchID = database.ExecuteAndGetID(createMatchCMD);

                if (matchID != 0)
                {
                    string addParticipantsCMD = $"INSERT INTO matchparticipants (MatchID, HomeTeamID, AwayTeamID) VALUES ('{matchID}','{match.HomeTeamID}','{match.AwayTeamID}')";
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

        public UpcomingMatch GetUpcomingMatch(int matchID)
        {
            int hometeamID = 0;
            int awayteamID = 0;
            DateTime date = Convert.ToDateTime("01-01-1900");

            MySqlCommand command = new MySqlCommand("GetMatch");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@matchID", MySqlDbType.Int32).Value = matchID;
            
            MySqlDataReader reader = database.Read(command);

            while(reader.Read())
            {
                hometeamID = (int)reader["HomeTeamID"];
                awayteamID = (int)reader["AwayTeamID"];
                date = (DateTime)reader["Date"];
            }
            database.CloseConnection();

            string hometeamName = teamrep.GetName(hometeamID);
            string awayteamName = teamrep.GetName(awayteamID);

            UpcomingMatch match = new UpcomingMatch(matchID, hometeamID, awayteamID, hometeamName, awayteamName, 0, 0, 0, date);

            return match;
        }

        public int GetID(Match match)
        {
            string command = $"SELECT MatchID FROM matchparticipants WHERE HomeTeam = '{match.HomeTeamID}' AND AwayTeam = {match.AwayTeamID}'";
            int id = database.GetInt(command);

            return id;
        }

        public int GetHomeTeamID(int matchid)
        {
            string command = $"SELECT HomeTeamID FROM matchparticipants WHERE MatchID = '{matchid}'";
            int id = database.GetInt(command);

            return id;
        }

        public int GetAwayTeamID(int matchid)
        {
            string command = $"SELECT AwayTeamID FROM matchparticipants WHERE MatchID = '{matchid}'";
            int id = database.GetInt(command);

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

        public void Update(Match match)
        {
            
            //--------------------------------
        }

        public bool AddFinishedMatch(FinishedMatch match)
        {
            string command = $"UPDATE matches SET `IsFinished`= {1},`ScoreHome`= {match.ScoreHome},`ScoreAway`= {match.ScoreAway} WHERE MatchID = '{match.MatchID}'";

            database.ExecuteCMD(command);
            return true;
        }

        public List<UpcomingMatch> GetUpcomingMatches()
        {
            List<UpcomingMatch> matchList = new List<UpcomingMatch>();

            string command = $"SELECT MatchID,Date,MultiplierHome,MultiplierAway,MultiplierDraw FROM matches WHERE IsFinished = '0'";

            MySqlDataReader reader = database.ReadMysql(command);

            while (reader.Read())
            {
                int matchID = (int)reader["MatchID"];


                //De ID's en namen zet ik op null omdat ik ze nog niet kan ophalen uit de database omdat de datareader nog openstaat.
                int homeTeamID = 0;
                int awayTeamID = 0;
                string homeTeamName = null;
                string awayTeamName = null;
               
                decimal multiplierHome = (decimal)reader["MultiplierHome"];
                decimal multiplierAway = (decimal)reader["MultiplierAway"];
                decimal multiplierDraw = (decimal)reader["MultiplierDraw"];
                DateTime date = (DateTime)reader["Date"];
                
                UpcomingMatch match = new UpcomingMatch(matchID, homeTeamID, awayTeamID, homeTeamName, awayTeamName, multiplierHome, multiplierAway, multiplierDraw, date);
               
                matchList.Add(match);
            }

            database.CloseConnection();

            //Hier worden de ID's en namen van de teams uit de database gehaald en in de lijst geplaatst.
            foreach (UpcomingMatch m in matchList)
            {
                m.HomeTeamID = GetHomeTeamID(m.MatchID);
                m.AwayTeamID = GetAwayTeamID(m.MatchID);
                m.HomeTeamName = teamrep.GetName(m.HomeTeamID);
                m.AwayTeamName = teamrep.GetName(m.AwayTeamID);
            }
            return matchList;
        }

        public List<FinishedMatch> GetFinishedMatches()
        {
            List<FinishedMatch> matchList = new List<FinishedMatch>();

            string command = $"SELECT * FROM matches WHERE IsFinished = '1'";

            MySqlDataReader reader = database.ReadMysql(command);

            while (reader.Read())
            {
                int matchID = (int)reader["MatchID"];


                //De ID's en namen zet ik op null omdat ik ze nog niet kan ophalen uit de database omdat de datareader nog openstaat.
                int homeTeamID = 0;
                int awayTeamID = 0;
                string homeTeamName = null;
                string awayTeamName = null;

                decimal multiplierHome = (decimal)reader["MultiplierHome"];
                decimal multiplierAway = (decimal)reader["MultiplierAway"];
                decimal multiplierDraw = (decimal)reader["MultiplierDraw"];
                DateTime date = (DateTime)reader["Date"];
                int scoreHome = (int)reader["ScoreHome"];
                int scoreAway = (int)reader["ScoreAway"];
                

                FinishedMatch match = new FinishedMatch(matchID, homeTeamID, awayTeamID, homeTeamName, awayTeamName, multiplierHome, multiplierAway, multiplierDraw, date, 
                    scoreHome, scoreAway);

                matchList.Add(match);
            }

            database.CloseConnection();

            //Hier worden de ID's en namen van de teams uit de database gehaald en in de lijst geplaatst.
            foreach (FinishedMatch m in matchList)
            {
                m.HomeTeamID = GetHomeTeamID(m.MatchID);
                m.AwayTeamID = GetAwayTeamID(m.MatchID);
                m.HomeTeamName = teamrep.GetName(m.HomeTeamID);
                m.AwayTeamName = teamrep.GetName(m.AwayTeamID);
            }
            return matchList;
        }
    }
}
