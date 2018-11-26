using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using BetBet.Model;

namespace BetBet.Data
{
    public class TeamRepository : IRepository<Team>
    {
        BetBetDB database = new BetBetDB();

        public bool Create(Team team)
        {
            return true;
        }

        public int GetID(Team team)
        {
            return 1;
        }

        public List<Team> GetTeams()
        {
            List<Team> TeamList = new List<Team>();

            string command = $"SELECT * FROM teams";
            MySqlDataReader reader = database.ReadMysql(command);

            while (reader.Read())
            {
                Team team = new Team
                {
                    TeamID = (int)reader["TeamID"],
                    TeamName = (string)reader["TeamName"],
                    City = (string)reader["City"],
                    GamesPlayed = (int)reader["GamesPlayed"],
                    GamesWon = (int)reader["GamesWon"],
                    Draws = (int)reader["Draws"],
                    GamesLost = (int)reader["GamesLost"],
                    GoalsFor = (int)reader["GoalsFor"],
                    GoalsAgainst = (int)reader["GoalsAgainst"],
                    GoalSaldo = (int)reader["GoalSaldo"],
                    Points = (int)reader["Points"],
                };

                TeamList.Add(team);
            }

            return TeamList;
            
        }

        public bool Delete(Team team)
        {
            return true;
        }

        public bool Update (Team team)
        {
            return true;
        }
    }
}
