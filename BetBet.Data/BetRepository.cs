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

            //----------------------------Prediction value insert fixen-------------------------------------------\\
            string command = $"INSERT INTO bets (MatchID, UserID, Amount, Prediction) VALUES ('{bet.MatchID}','{bet.UserID}','{bet.Amount.ToString(CultureInfo.InvariantCulture)}','{bet.Prediction.ToString()}')";
            bool result = database.ExecuteCMD(command);

            return result;
        }

        public bool Delete(Bet bet)
        {
            throw new NotImplementedException();
        }

        public int GetID(Bet bet)
        {
            throw new NotImplementedException();
        }

        public bool Update(Bet bet)
        {
            throw new NotImplementedException();
        }

        public List<Bet> GetBets(User user)
        {
            List<Bet> TeamList = new List<Bet>();

            string command = $"SELECT * FROM Bets WHERE UserID = '{user.UserID}'";
            MySqlDataReader reader = database.ReadMysql(command);

            while (reader.Read())
            {
                Bet bet = new Bet
                {
                    MatchID = (int)reader["MatchID"],
                    Amount = (decimal)reader["Amount"],
                    Prediction = (PredictionEnum)reader["Prediction"]

                };
                TeamList.Add(bet);
            }

            database.CloseConnection();
            return TeamList;
        }

        public int CheckBet(int matchID, int userID)
        {
            string command = $"SELECT BetID FROM bets WHERE MatchID = '{matchID}' AND UserID = '{userID}'";

            int result = database.GetInt(command);

            return result;
        }
    }
}
