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
    public class MatchRepository : IMatchRepository
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

                MySqlCommand command = new MySqlCommand(@"INSERT INTO matches(Date, MultiplierHome, MultiplierAway, MultiplierDraw, IsFinished) VALUES(@date, @multiplierhome, @multiplieraway, @multiplierdraw, @isfinished)");
                command.Parameters.AddWithValue("@date", match.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@multiplierhome", match.MultiplierHome.ToString(CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@multiplieraway", match.MultiplierAway.ToString(CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@multiplierdraw", match.MultiplierDraw.ToString(CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@isfinished", 0);
                

                //string createMatchCMD = $"INSERT INTO matches (Date, MultiplierHome, MultiplierAway, MultiplierDraw, IsFinished) VALUES " +
                //   $"('{match.Date.ToString("yyyy-MM-dd")}','{match.MultiplierHome.ToString(CultureInfo.InvariantCulture)}','{match.MultiplierAway.ToString(CultureInfo.InvariantCulture)}','{match.MultiplierDraw.ToString(CultureInfo.InvariantCulture)}','{0}')";

                int matchID = database.ExecuteAndGetID(command);

                

                if (matchID != 0)
                {
                    MySqlCommand addParticipantsCMD = new MySqlCommand(@"INSERT INTO matchparticipants (MatchID, HomeTeamID, AwayTeamID) VALUES (@matchid, @hometeamid, @awayteamid)");
                    
                    addParticipantsCMD.Parameters.AddWithValue("@matchid", matchID);
                    addParticipantsCMD.Parameters.AddWithValue("@hometeamid", match.HomeTeamID);
                    addParticipantsCMD.Parameters.AddWithValue("@awayteamid", match.AwayTeamID);
                    //string addParticipantsCMD = $"INSERT INTO matchparticipants (MatchID, HomeTeamID, AwayTeamID) VALUES ('{matchID}','{match.HomeTeamID}','{match.AwayTeamID}')";
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
            MySqlCommand command = new MySqlCommand(@"SELECT MatchID FROM matchparticipants WHERE HomeTeam = @hometeamID AND AwayTeam = @awayteamID;");
            command.Parameters.AddWithValue("@hometeamID", match.HomeTeamID);
            command.Parameters.AddWithValue("@awayteamID", match.AwayTeamID);

           // string command = $"SELECT MatchID FROM matchparticipants WHERE HomeTeam = '{match.HomeTeamID}' AND AwayTeam = {match.AwayTeamID}'";
            int id = database.GetInt(command);

            return id;
        }

        public void Update(FinishedMatch match)
        {
            //string command = $"UPDATE matches SET `IsFinished`= {1},`ScoreHome`= {match.ScoreHome},`ScoreAway`= {match.ScoreAway}, `Result`= '{match.Result.ToString()}' WHERE MatchID = '{match.MatchID}'";

            MySqlCommand command = new MySqlCommand(@"UPDATE matches SET `IsFinished`= @isfinished,`ScoreHome`= @scorehome,`ScoreAway`= @scoreaway, `Result`= @result WHERE MatchID = @matchid;");
            command.Parameters.AddWithValue("@isfinished", 1);
            command.Parameters.AddWithValue("@scorehome", match.ScoreHome);
            command.Parameters.AddWithValue("@scoreaway", match.ScoreAway);
            command.Parameters.AddWithValue("@result", match.Result.ToString());
            command.Parameters.AddWithValue("@matchid", match.MatchID);

            database.ExecuteCMD(command);
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

        public List<UpcomingMatch> GetUpcomingMatches(int isFinished)
        {
            List<UpcomingMatch> matchList = new List<UpcomingMatch>();
            
            MySqlCommand command = new MySqlCommand("GetMatches");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@isFinished", MySqlDbType.Binary).Value = isFinished;

            MySqlDataReader reader = database.Read(command);

            while (reader.Read())
            {
                int matchID = (int)reader["MatchID"];

                Team homeTeam = new Team();
                Team awayTeam = new Team();

                homeTeam.TeamID = (int)reader["HomeTeamID"];
                awayTeam.TeamID = (int)reader["AwayTeamID"];
                decimal multiplierHome = (decimal)reader["MultiplierHome"];
                decimal multiplierAway = (decimal)reader["MultiplierAway"];
                decimal multiplierDraw = (decimal)reader["MultiplierDraw"];
                DateTime date = (DateTime)reader["Date"];
                
                UpcomingMatch match = new UpcomingMatch(matchID, homeTeam, awayTeam, multiplierHome, multiplierAway, multiplierDraw, date);
               
                matchList.Add(match);
            }

            database.CloseConnection();

            //Hier worden de namen van de teams uit de database gehaald en in de lijst geplaatst.
            foreach (UpcomingMatch m in matchList)
            {                m.HomeTeam.TeamName = teamrep.GetName(m.HomeTeam.TeamID);
                m.AwayTeam.TeamName = teamrep.GetName(m.AwayTeam.TeamID);
            }
            return matchList;
        }

        public List<FinishedMatch> GetFinishedMatches(int isFinished)
        {
            List<FinishedMatch> matchList = new List<FinishedMatch>();

            MySqlCommand command = new MySqlCommand("GetMatches");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@isFinished", MySqlDbType.Bit).Value = isFinished;

            MySqlDataReader reader = database.Read(command);

            while (reader.Read())
            {
                int matchID = (int)reader["MatchID"];

                Team homeTeam = new Team();
                Team awayTeam = new Team();

                homeTeam.TeamID = (int)reader["HomeTeamID"];
                awayTeam.TeamID = (int)reader["AwayTeamID"];
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
                scoreHome = (int)reader["ScoreHome"];
                scoreAway = (int)reader["ScoreAway"];
                Enum.TryParse((string)reader["Result"], out result);
            }

            database.CloseConnection();
            
            homeTeam.TeamName = teamrep.GetName(homeTeam.TeamID);
            awayTeam.TeamName = teamrep.GetName(awayTeam.TeamID);

            FinishedMatch match = new FinishedMatch(matchID, homeTeam, awayTeam, multiplierHome, multiplierAway, multiplierDraw, date, scoreHome, scoreAway, result);
                            
            return match;
        }
    }
}
