using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using BetBet.Model;

namespace BetBet.Data
{
    public class TeamRepository : ITeamRepository
    {
        BetBetDB database = new BetBetDB();
        
        public string GetName(int id)
        {
            MySqlCommand command = new MySqlCommand(@"SELECT TeamName FROM teams WHERE TeamID = @teamid;");
            command.Parameters.AddWithValue("@teamid", id);
            
            string result = database.getString(command);

            return result;
        }

        public Team GetTeam(int id)
        {
            Team team = new Team();

            MySqlCommand command = new MySqlCommand(@"SELECT * FROM teams WHERE TeamID = @teamid;");
            command.Parameters.AddWithValue("@teamid", id);

            MySqlDataReader reader = database.Read(command);

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

            MySqlCommand command = new MySqlCommand(@"SELECT * FROM teams");
            MySqlDataReader reader = database.Read(command);
            
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

        public void Update (Team team)
        {
            MySqlCommand getTeam = new MySqlCommand(@"SELECT * FROM teams WHERE TeamID = @teamid;");
            getTeam.Parameters.AddWithValue("@teamid", team.TeamID);
            
            MySqlDataReader reader = database.Read(getTeam);

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


            // Update de team, Stored procedure hiervan doet vreemd
            MySqlCommand UpdateTeam = new MySqlCommand(@"UPDATE teams SET GamesPlayed = @gamesplayed, GamesWon = @gameswon, Draws = @draws, GamesLost = @gameslost, GoalsFor = @goalsfor, GoalsAgainst = @goalsagainst, GoalDifferential = @goaldifferential, Points = @points WHERE TeamID = @teamid;");
            UpdateTeam.Parameters.AddWithValue("@gamesplayed", team.GamesPlayed);
            UpdateTeam.Parameters.AddWithValue("@gameswon", team.GamesWon);
            UpdateTeam.Parameters.AddWithValue("@draws", team.Draws);
            UpdateTeam.Parameters.AddWithValue("@gameslost", team.GamesLost);
            UpdateTeam.Parameters.AddWithValue("@goalsfor", team.GoalsFor);
            UpdateTeam.Parameters.AddWithValue("@goalsagainst", team.GoalsAgainst);
            UpdateTeam.Parameters.AddWithValue("@goaldifferential", team.GoalDifferential);
            UpdateTeam.Parameters.AddWithValue("@points", team.Points);
            UpdateTeam.Parameters.AddWithValue("@teamid", team.TeamID);
            
            database.ExecuteCMD(UpdateTeam);
        }
    }
}
