using BetBet.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetBet.Data
{
    public class BetRepository : IBetRepository
    {
        BetBetDB database = new BetBetDB();

        public bool Create(Bet bet)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            
            MySqlCommand command = new MySqlCommand("CreateBet");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@matchid", bet.MatchID);
            command.Parameters.AddWithValue("@userid", bet.UserID);
            command.Parameters.AddWithValue("@amount", bet.Amount.ToString(CultureInfo.InvariantCulture));
            command.Parameters.AddWithValue("@prediction", bet.Prediction.ToString());

            bool result = database.ExecuteCMD(command);

            return result;
        }

        public int DeleteBetValidation(int userID, int betID)
        {
            MySqlCommand command = new MySqlCommand(@"SELECT BetID FROM bets WHERE UserID = @userid AND BetID = @betid;");
            command.Parameters.AddWithValue("@userid", userID);
            command.Parameters.AddWithValue("@betid", betID);
            
            int result = database.GetInt(command);

            return result;
        }

        public bool Delete(int id)
        {
            MySqlCommand command = new MySqlCommand(@"DELETE FROM bets WHERE BetID = @id");
            command.Parameters.AddWithValue("@id", id);
            
            bool result = database.ExecuteCMD(command);

            return result;        
        }

        public int GetID(Bet bet)
        {
            MySqlCommand command = new MySqlCommand(@"SELECT BetID FROM bets WHERE MatchID = @matchid AND UserID = @userid");
            command.Parameters.AddWithValue("@matchid", bet.MatchID);
            command.Parameters.AddWithValue("@userid", bet.UserID);

            int id = database.GetInt(command);

            return id;
        }

        public void Update(Bet bet)
        {
            MySqlCommand command = new MySqlCommand("UpdateBet");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hasended", 1);
            command.Parameters.AddWithValue("@result", bet.Result.ToString());
            command.Parameters.AddWithValue("@earned", bet.Earned);
            command.Parameters.AddWithValue("@bid", bet.BetID);
            
            database.ExecuteCMD(command);
        }

        public List<Bet> GetBetsByUser(User user)
        {
            List<Bet> BetList = new List<Bet>();

            MySqlCommand command = new MySqlCommand(@"SELECT * FROM bets WHERE UserID = @userid;");
            command.Parameters.AddWithValue("@userid", user.UserID);
            
            MySqlDataReader reader = database.Read(command);

            while (reader.Read())
            {
                Enum.TryParse((string)reader["Prediction"], out MatchResult prediction);
                Enum.TryParse((string)reader["Result"], out BetResult result);
                Bet bet = new Bet
                {
                    BetID = (int)reader["BetID"],
                    MatchID = (int)reader["MatchID"],
                    Amount = (decimal)reader["Amount"],
                    Prediction = prediction,
                    HasEnded = (bool)reader["HasEnded"],
                    Result = result,
                    Earned = (decimal)reader["Earned"]

                };
                BetList.Add(bet);
            }

            database.CloseConnection();
            return BetList;
        }

        public List<Bet> GetBetsByMatch(FinishedMatch match)
        {
            List<Bet> BetList = new List<Bet>();

            MySqlCommand command = new MySqlCommand(@"SELECT * FROM bets WHERE MatchID = @matchid;");
            command.Parameters.AddWithValue("@matchid", match.MatchID);
            
            MySqlDataReader reader = database.Read(command);

            while (reader.Read())
            {
                Enum.TryParse((string)reader["Prediction"], out MatchResult prediction);
                Enum.TryParse((string)reader["Result"], out BetResult result);
                Bet bet = new Bet
                {
                    BetID = (int)reader["BetID"],
                    UserID = (int)reader["UserID"],
                    MatchID = (int)reader["MatchID"],
                    Amount = (decimal)reader["Amount"],
                    Prediction = prediction,
                    HasEnded = (bool)reader["HasEnded"],
                    Result = result,
                    Earned = (decimal)reader["Earned"]
                };
                BetList.Add(bet);
            }

            database.CloseConnection();
            return BetList;
        }

        public int CheckBet(int matchID, int userID)
        {
            MySqlCommand command = new MySqlCommand(@"SELECT BetID FROM bets WHERE MatchID = @matchid AND UserID = @userid;");
            command.Parameters.AddWithValue("@matchid", matchID);
            command.Parameters.AddWithValue("@userid", userID);

            int result = database.GetInt(command);

            return result;
        }
    }
}
