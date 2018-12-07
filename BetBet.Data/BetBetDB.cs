using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using BetBet.Model;
using System.Data;
using System.Diagnostics;

namespace BetBet.Data
{
    public class BetBetDB
    {
        public string Hostname = "studmysql01.fhict.local";
        public string DBName = "dbi382222";
        public string ID = "dbi382222";
        public string Password = "vQzTCdskA2UBvfzp";

        private MySqlConnection connection;

        public BetBetDB()
        {
            connection = new MySqlConnection($"SERVER={Hostname};DATABASE={DBName};UID={ID};PASSWORD={Password};SslMode=none");
        }

        private void OpenConnectionIfClosed()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public void ExecuteCMD(MySqlCommand command)
        {
            command.Connection = connection;
            //using (MySqlCommand cmd = new MySqlCommand(command, connection))
            using (command)
            {
                try
                {
                    OpenConnectionIfClosed();
                    command.ExecuteNonQuery();         
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
        }
        public void ExecuteCMD(string command)
        {
            using (MySqlCommand cmd = new MySqlCommand(command, connection))
            {
                try
                {
                    OpenConnectionIfClosed();
                    cmd.ExecuteNonQuery();
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
        }
        public int ExecuteAndGetID(string command)
        {
            int id = 0;
            using (MySqlCommand cmd = new MySqlCommand(command, connection))
            {
                try
                {
                    OpenConnectionIfClosed();
                    cmd.ExecuteNonQuery();
                    id = (int)cmd.LastInsertedId;
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
            return id;
        }

        public string getString(string command)
        {
            string result = null;
            using (MySqlCommand cmd = new MySqlCommand(command, connection))
            {
                try
                {
                    OpenConnectionIfClosed();
                    result = (string)cmd.ExecuteScalar();
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

            return result;
        }
        public int getID(string command)
        {
            int result = 0;

            using (MySqlCommand cmd = new MySqlCommand(command, connection))
            {
                try
                {
                    OpenConnectionIfClosed();
                    result = (int)cmd.ExecuteScalar();
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
            return result;
        }

        public MySqlDataReader ReadMysql(string command)
        {
            
            //using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM teams", connection))
            // {
            MySqlCommand cmd = new MySqlCommand(command, connection);
                try
                {
                    OpenConnectionIfClosed();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    return reader;
                    /*while (reader.Read())
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
                        teamList.Add(team);
                    }
                    return teamList;*/
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
                                 
            //}
        }
    }
}
