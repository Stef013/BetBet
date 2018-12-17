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

        public bool ExecuteCMD(string command)
        {
            bool result;

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
                    result = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
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

        public int GetInt(string command)
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

        public decimal GetDecimal(string command)
        {
            decimal result = 0;

            using (MySqlCommand cmd = new MySqlCommand(command, connection))
            {
                try
                {
                    OpenConnectionIfClosed();
                    result = (decimal)cmd.ExecuteScalar();
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
            MySqlCommand cmd = new MySqlCommand(command, connection);
            try
            {
                OpenConnectionIfClosed();
                MySqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
