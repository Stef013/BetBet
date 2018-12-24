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
                    Goals = (int)reader["Goals"],
                    GoalsAgainst = (int)reader["GoalsAgainst"],
                    GoalSaldo = (int)reader["GoalSaldo"],
                    Points = (int)reader["Points"],
                };
                TeamList.Add(team);
            }
            
            database.CloseConnection();
            return TeamList;
        }

        

        public bool Delete(Team team)
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
                team.Goals = team.Goals + (int)reader["Goals"];
                team.GoalsAgainst = team.GoalsAgainst + (int)reader["GoalsAgainst"];
                team.GoalSaldo = team.Goals - team.GoalsAgainst;
                team.Points = team.Points + (int)reader["Points"];
            }

            database.CloseConnection();

            string UpdateTeam = $"UPDATE teams SET GamesPlayed = '{team.GamesPlayed}', GamesWon ='{team.GamesWon}', Draws = '{team.Draws}', GamesLost = '{team.GamesLost}', Goals = '{team.Goals}', GoalsAgainst = '{team.GoalsAgainst}', GoalSaldo = '{team.GoalSaldo}', Points = '{team.Points}' WHERE TeamID = '{team.TeamID}'";
            database.ExecuteCMD(UpdateTeam);

        }
    }
}
