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

        public string GetName(int id)
        {
            string command = $"SELECT TeamName FROM teams WHERE TeamID = '{id}'";
            string result = database.getString(command);

            return result;
        }

        public Team GetTeam(int id)
        {
            Team team = new Team();

            string command = $"SELECT * FROM teams WHERE TeamID = '{id}'";
            MySqlDataReader reader = database.ReadMysql(command);

            while (reader.Read())
            {
                team = new Team
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
                    GoalDifferential = (int)reader["GoalDifferential"],
                    Points = (int)reader["Points"],
                };
            }

            database.CloseConnection();
            return team;
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
                    GoalDifferential = (int)reader["GoalDifferential"],
                    Points = (int)reader["Points"],
                };
                TeamList.Add(team);
            }
            
            database.CloseConnection();
            return TeamList;
        }

        

        public bool Delete(int id)
        {
            return true;
        }

        public void Update (Team team)
        {
            string getTeam = $"SELECT * FROM teams WHERE TeamID = '{team.TeamID}'";
            MySqlDataReader reader = database.ReadMysql(getTeam);

            while (reader.Read())
            {
                team.GamesPlayed = (int)reader["GamesPlayed"] + 1;
                team.GamesWon = team.GamesWon + (int)reader["GamesWon"];
                team.Draws = team.Draws + (int)reader["Draws"];
                team.GamesLost = team.GamesLost + (int)reader["GamesLost"];
                team.GoalsFor = team.GoalsFor + (int)reader["GoalsFor"];
                team.GoalsAgainst = team.GoalsAgainst + (int)reader["GoalsAgainst"];
                team.GoalDifferential = team.GoalsFor - team.GoalsAgainst;
                team.Points = team.Points + (int)reader["Points"];
            }

            database.CloseConnection();

            string UpdateTeam = $"UPDATE teams SET GamesPlayed = '{team.GamesPlayed}', GamesWon ='{team.GamesWon}', Draws = '{team.Draws}', GamesLost = '{team.GamesLost}', GoalsFor = '{team.GoalsFor}', GoalsAgainst = '{team.GoalsAgainst}', GoalDifferential = '{team.GoalDifferential}', Points = '{team.Points}' WHERE TeamID = '{team.TeamID}'";
            database.ExecuteCMD(UpdateTeam);

        }
    }
}
