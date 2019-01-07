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

        public bool Delete(int id)
        {
            string command = $"DELETE * FROM match Where MatchID =  '{id}'";
            database.ExecuteCMD(command);
            return true;

        }

        public void Update(Match match)
        {
            
            //-----------------------------
        }

        public bool AddFinishedMatch(FinishedMatch match)
        {
            string command = $"UPDATE matches SET `IsFinished`= {1},`ScoreHome`= {match.ScoreHome},`ScoreAway`= {match.ScoreAway}, `Result`= '{match.Result.ToString()}' WHERE MatchID = '{match.MatchID}'";

            database.ExecuteCMD(command);
            return true;
        }

        public UpcomingMatch GetUpcomingMatch(int matchID)
        {
            DateTime date = Convert.ToDateTime("01-01-1900");
            Team homeTeam = new Team();
            Team awayTeam = new Team();
            decimal multiplierHome = 0;
            decimal multiplierAway = 0;
            decimal multiplierDraw = 0;

            MySqlCommand command = new MySqlCommand("GetMatch");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@matchID", MySqlDbType.Int32).Value = matchID;

            MySqlDataReader reader = database.Read(command);

            while (reader.Read())
            {
                homeTeam.TeamID = (int)reader["HomeTeamID"];
                awayTeam.TeamID = (int)reader["AwayTeamID"];
                multiplierHome = (decimal)reader["MultiplierHome"];
                multiplierAway = (decimal)reader["MultiplierAway"];
                multiplierDraw = (decimal)reader["MultiplierDraw"];
                date = (DateTime)reader["Date"];
            }

            database.CloseConnection();

            homeTeam.TeamName = teamrep.GetName(homeTeam.TeamID);
            awayTeam.TeamName= teamrep.GetName(awayTeam.TeamID);

            UpcomingMatch match = new UpcomingMatch(matchID, homeTeam , awayTeam, multiplierHome, multiplierAway, multiplierDraw, date);

            return match;
        }

        public List<UpcomingMatch> GetUpcomingMatches()
        {
            List<UpcomingMatch> matchList = new List<UpcomingMatch>();

            string command = $"SELECT MatchID,Date,MultiplierHome,MultiplierAway,MultiplierDraw FROM matches WHERE IsFinished = '0'";

            MySqlDataReader reader = database.ReadMysql(command);

            while (reader.Read())
            {
                int matchID = (int)reader["MatchID"];

                Team homeTeam = new Team();
                Team awayTeam = new Team();
               
                decimal multiplierHome = (decimal)reader["MultiplierHome"];
                decimal multiplierAway = (decimal)reader["MultiplierAway"];
                decimal multiplierDraw = (decimal)reader["MultiplierDraw"];
                DateTime date = (DateTime)reader["Date"];
                
                UpcomingMatch match = new UpcomingMatch(matchID, homeTeam, awayTeam, multiplierHome, multiplierAway, multiplierDraw, date);
               
                matchList.Add(match);
            }

            database.CloseConnection();

            //Hier worden de ID's en namen van de teams uit de database gehaald en in de lijst geplaatst.
            foreach (UpcomingMatch m in matchList)
            {
                m.HomeTeam.TeamID = GetHomeTeamID(m.MatchID);
                m.AwayTeam.TeamID = GetAwayTeamID(m.MatchID);
                m.HomeTeam.TeamName = teamrep.GetName(m.HomeTeam.TeamID);
                m.AwayTeam.TeamName = teamrep.GetName(m.AwayTeam.TeamID);
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

                Team homeTeam = new Team();
                Team awayTeam = new Team();

                decimal multiplierHome = (decimal)reader["MultiplierHome"];
                decimal multiplierAway = (decimal)reader["MultiplierAway"];
                decimal multiplierDraw = (decimal)reader["MultiplierDraw"];
                DateTime date = (DateTime)reader["Date"];
                int scoreHome = (int)reader["ScoreHome"];
                int scoreAway = (int)reader["ScoreAway"];
                Enum.TryParse((string)reader["Result"], out MatchResult result);

                FinishedMatch match = new FinishedMatch(matchID, homeTeam, awayTeam, multiplierHome, multiplierAway, multiplierDraw, date, scoreHome, scoreAway, result);
                
                matchList.Add(match);
            }

            database.CloseConnection();

            //Hier worden de ID's en namen van de teams uit de database gehaald en in de lijst geplaatst.
            foreach (FinishedMatch m in matchList)
            {
                m.HomeTeam.TeamID = GetHomeTeamID(m.MatchID);
                m.AwayTeam.TeamID = GetAwayTeamID(m.MatchID);
                m.HomeTeam.TeamName = teamrep.GetName(m.HomeTeam.TeamID);
                m.AwayTeam.TeamName = teamrep.GetName(m.AwayTeam.TeamID);
            }
            return matchList;
        }

        public FinishedMatch GetFinishedMatch(int matchID)
        {
            DateTime date = Convert.ToDateTime("01-01-1900");
            Team homeTeam = new Team();
            Team awayTeam = new Team();
            decimal multiplierHome = 0;
            decimal multiplierAway = 0;
            decimal multiplierDraw = 0;
            int scoreHome = 0;
            int scoreAway = 0;
            MatchResult result = 0;


            string command = $"SELECT * FROM matches WHERE IsFinished = '1' AND MatchID = '{matchID}'";

            MySqlDataReader reader = database.ReadMysql(command);

            while (reader.Read())
            {
                multiplierHome = (decimal)reader["MultiplierHome"];
                multiplierAway = (decimal)reader["MultiplierAway"];
                multiplierDraw = (decimal)reader["MultiplierDraw"];
                date = (DateTime)reader["Date"];
                scoreHome = (int)reader["ScoreHome"];
                scoreAway = (int)reader["ScoreAway"];
                Enum.TryParse((string)reader["Result"], out result);
            }

            database.CloseConnection();

            homeTeam.TeamID = GetHomeTeamID(matchID);
            awayTeam.TeamID = GetAwayTeamID(matchID);
            homeTeam.TeamName = teamrep.GetName(homeTeam.TeamID);
            awayTeam.TeamName = teamrep.GetName(awayTeam.TeamID);

            FinishedMatch match = new FinishedMatch(matchID, homeTeam, awayTeam, multiplierHome, multiplierAway, multiplierDraw, date, scoreHome, scoreAway, result);
                            
            return match;
        }
    }
}
