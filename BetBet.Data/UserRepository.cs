using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using BetBet.Model;
using System.Diagnostics;
using System.Data;

namespace BetBet.Data
{
    public class UserRepository : IRepository<User>
    {

        public string Hostname = "studmysql01.fhict.local";
        public string DBName = "dbi382222";
        public string ID = "dbi382222";
        public string Password = "vQzTCdskA2UBvfzp";

        private MySqlConnection connection;

        public UserRepository()
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

        //BetBetDB database = new BetBetDB();
        //BetBetDBGoede dBGoede = new BetBetDBGoede();

        public bool Create(User user)
        {
            var namecheck = CheckUsername(user.Username);
            bool result = false;

            if (namecheck == null)
            {
                string command = $"INSERT into users (Username, Password, Balance, IsAdmin) VALUES ('{user.Username}','{user.Password}','{0.00}','{0}')";
                using (MySqlCommand cmd = new MySqlCommand(command, connection))
                {
                    try
                    {
                        OpenConnectionIfClosed();
                        cmd.ExecuteNonQuery();
                        result = true;
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
            else
            {
                return false;
            }            
        } 

        public int GetID(User user)
        {
            string command = $"SELECT UserID FROM users WHERE Username = '{user.Username}'";
            int id = 0;

            using (MySqlCommand cmd = new MySqlCommand(command, connection))
            {
                try
                {
                    OpenConnectionIfClosed();
                    id = (int)cmd.ExecuteScalar();
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

        public bool Delete(User user)
        {
            bool result = false;
            int id = GetID(user);

            if (id != 0)
            {
                string command = $"DELETE * Where UserID =  '{id}'";
                using (MySqlCommand cmd = new MySqlCommand(command, connection))
                {
                    try
                    {
                        OpenConnectionIfClosed();
                        cmd.ExecuteNonQuery();
                        result = true;
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
            else
            {
                return false;
            }
        }

        public bool Update(User user)
        {
            //---------------------------------- moet nog code bij------------------------------------------------
            return true;
        }

        public string CheckUsername(string username)
        {
            string command = $"SELECT Username FROM users WHERE Username = '{username}'";
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

        public bool ComparePassword(string username, string password)
        {
            bool result = false;
            string command = $"SELECT Password FROM Users WHERE Username = '{username}'";

            using (MySqlCommand cmd = new MySqlCommand(command, connection))
            {
                try
                {
                    OpenConnectionIfClosed();
                    string passwordresult = (string)cmd.ExecuteScalar();

                    if (string.Compare(password, passwordresult) == 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
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

            return result;
        }
    }
}
