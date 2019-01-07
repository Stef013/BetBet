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
    public class BetRepository : IRepository<Bet>
    {
        BetBetDB database = new BetBetDB();

        public bool Create(Bet bet)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            
            string command = $"INSERT INTO bets (MatchID, UserID, Amount, Prediction) VALUES ('{bet.MatchID}','{bet.UserID}','{bet.Amount.ToString(CultureInfo.InvariantCulture)}','{bet.Prediction.ToString()}')";
            bool result = database.ExecuteCMD(command);

            return result;
        }

        public int DeleteBetValidation(int userID, int betID)
        {
            string command = $"SELECT BetID FROM bets WHERE UserID = '{userID}' AND BetID = '{betID}'";
            int result = database.GetInt(command);

            return result;
        }

        public bool Delete(int id)
        {
            string command = $"DELETE FROM bets WHERE BetID = '{id}'";
            bool result = database.ExecuteCMD(command);

            return result;        
        }

        public int GetID(Bet bet)
        {
            string command = $"SELECT BetID FROM bets WHERE UserID = '{bet.UserID}' AND MatchID = '{bet.MatchID}'";
            int id = database.GetInt(command);

            return id;
        }

        public void Update(Bet bet)
        {
            string command = $"UPDATE `bets` SET `HasEnded`= '{1}',`Result`= '{bet.Result.ToString()}',`Earned`= '{bet.Earned}' WHERE BetID = '{bet.BetID}'";
            database.ExecuteCMD(command);
        }

        public List<Bet> GetBetsByUser(User user)
        {
            List<Bet> BetList = new List<Bet>();

            string command = $"SELECT * FROM bets WHERE UserID = '{user.UserID}'";
            MySqlDataReader reader = database.ReadMysql(command);

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

            string command = $"SELECT * FROM bets WHERE MatchID = '{match.MatchID}'";
            MySqlDataReader reader = database.ReadMysql(command);

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
            string command = $"SELECT BetID FROM bets WHERE MatchID = '{matchID}' AND UserID = '{userID}'";

            int result = database.GetInt(command);

            return result;
        }
    }
}
