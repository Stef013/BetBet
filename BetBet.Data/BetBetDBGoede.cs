using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBet.Model;
using MySql.Data.MySqlClient;

namespace BetBet.Data
{
    public class BetBetDBGoede
    {
        public string Hostname = "studmysql01.fhict.local";
        public string DBName = "dbi382222";
        public string ID = "dbi382222";
        public string Password = "vQzTCdskA2UBvfzp";

        private MySqlConnection connection;

        public BetBetDBGoede()
        {
            connection = new MySqlConnection($"SERVER={Hostname};DATABASE={DBName};UID={ID};PASSWORD={Password};SslMode=none");
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM users;", connection))
            {
                try
                {
                    OpenConnectionIfClosed();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User(username: (string)reader["Username"], password: (string)reader["Password"])
                            {
                                UserID = (int)reader["UserID"],
                                Balance = (decimal)reader["Balance"]
                            };
                            users.Add(user);
                        }
                    } 
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return users;
        }

        public bool CreateMatch(Match match)
        {
            int matchID = 0;
            using (MySqlCommand cmd = new MySqlCommand($"INSERT INTO matches (Date, IsFinished) VALUES ('{match.Date}', {0});", connection))
            {
                try
                {
                    OpenConnectionIfClosed();
                    cmd.ExecuteNonQuery();
                    matchID = (int)cmd.LastInsertedId;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }

            using (MySqlCommand cmd = new MySqlCommand($"INSERT INTO matchparticipants (MatchID, TeamHome, TeamAway) VALUES ('{matchID}', '{match.HomeTeamID}', '{match.AwayTeamID}');", connection))
            {
                try
                {
                    OpenConnectionIfClosed();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return true;
        }

        private void OpenConnectionIfClosed()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }
    }
}
