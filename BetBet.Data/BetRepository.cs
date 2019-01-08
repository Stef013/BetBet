using BetBet.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetBet.Data
{
    public class BetRepository : IRepository<Bet, Bet>
    {
        BetBetDB database = new BetBetDB();

        public bool Create(Bet bet)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            MySqlCommand command = new MySqlCommand(@"INSERT INTO bets(MatchID, UserID, Amount, Prediction) VALUES(@matchid, @userid, @amount, @prediction)");
            command.Parameters.AddWithValue("@matchid", bet.MatchID);
            command.Parameters.AddWithValue("@userid", bet.UserID);
            command.Parameters.AddWithValue("@amount", bet.Amount.ToString(CultureInfo.InvariantCulture));
            command.Parameters.AddWithValue("@prediction", bet.Prediction.ToString());

            //string command = $"INSERT INTO bets (MatchID, UserID, Amount, Prediction) VALUES ('{bet.MatchID}','{bet.UserID}','{bet.Amount.ToString(CultureInfo.InvariantCulture)}','{bet.Prediction.ToString()}')";
            bool result = database.ExecuteCMD(command);

            return result;
        }

        public int DeleteBetValidation(int userID, int betID)
        {
            MySqlCommand command = new MySqlCommand(@"SELECT BetID FROM bets WHERE UserID = @userid AND BetID = @betid;");
            command.Parameters.AddWithValue("@userid", userID);
            command.Parameters.AddWithValue("@betid", betID);

            //string command = $"SELECT BetID FROM bets WHERE UserID = '{userID}' AND BetID = '{betID}'";
            int result = database.GetInt(command);

            return result;
        }

        public bool Delete(int id)
        {
            MySqlCommand command = new MySqlCommand(@"DELETE FROM bets WHERE BetID = @id");
            command.Parameters.AddWithValue("@id", id);

            //string command = $"DELETE FROM bets WHERE BetID = '{id}'";
            bool result = database.ExecuteCMD(command);

            return result;        
        }

        public int GetID(Bet bet)
        {
            MySqlCommand command = new MySqlCommand(@"DELETE FROM bets WHERE BetID = @id");
            command.Parameters.AddWithValue("@id", bet.BetID);

            //string command = $"SELECT BetID FROM bets WHERE UserID = '{bet.UserID}' AND MatchID = '{bet.MatchID}'";
            int id = database.GetInt(command);

            return id;
        }

        public void Update(Bet bet)
        {
            MySqlCommand command = new MySqlCommand(@"UPDATE bets SET `HasEnded`= @hasended,`Result`= @result,`Earned`= @earned WHERE BetID = @betid;");
            command.Parameters.AddWithValue("@hasended", 1);
            command.Parameters.AddWithValue("@result", bet.Result.ToString());
            command.Parameters.AddWithValue("@earned", bet.Earned);
            command.Parameters.AddWithValue("@betid", bet.BetID);

            //string command = $"UPDATE `bets` SET `HasEnded`= '{1}',`Result`= '{bet.Result.ToString()}',`Earned`= '{bet.Earned}' WHERE BetID = '{bet.BetID}'";
            database.ExecuteCMD(command);
        }

        public List<Bet> GetBetsByUser(User user)
        {
            List<Bet> BetList = new List<Bet>();

            MySqlCommand command = new MySqlCommand(@"SELECT * FROM bets WHERE UserID = @userid;");
            command.Parameters.AddWithValue("@userid", user.UserID);

            //string command = $"SELECT * FROM bets WHERE UserID = '{user.UserID}'";
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

            //string command = $"SELECT * FROM bets WHERE MatchID = '{match.MatchID}'";
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

           // string command = $"SELECT BetID FROM bets WHERE MatchID = '{matchID}' AND UserID = '{userID}'";

            int result = database.GetInt(command);

            return result;
        }
    }
}
