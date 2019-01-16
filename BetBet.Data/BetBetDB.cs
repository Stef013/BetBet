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

        public bool ExecuteCMD(MySqlCommand command)
        {
            bool result;
            command.Connection = connection;
            
            using (command)
            {
                try
                {
                    OpenConnectionIfClosed();
                    command.ExecuteNonQuery();
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

        public int ExecuteAndGetID(MySqlCommand command)
        {
            int id = 0;
            command.Connection = connection;

            using (command)
            {
                try
                {
                    OpenConnectionIfClosed();
                    command.ExecuteNonQuery();
                    id = (int)command.LastInsertedId;
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

        public string getString(MySqlCommand command)
        {
            command.Connection = connection;

            string result = null;

            using (command)
            {
                try
                {
                    OpenConnectionIfClosed();
                    result = (string)command.ExecuteScalar();
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

        public int GetInt(MySqlCommand command)
        {
            int result = 0;

            command.Connection = connection;

            using (command)
            {
                try
                {
                    OpenConnectionIfClosed();
                    result = (int)command.ExecuteScalar();
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

        public decimal GetDecimal(MySqlCommand command)
        {
            command.Connection = connection;

            decimal result = 0;

            using (command)
            {
                try
                {
                    OpenConnectionIfClosed();
                    result = (decimal)command.ExecuteScalar();
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

        
        public MySqlDataReader Read(MySqlCommand command)
        {
            command.Connection = connection;

            try
            {
                OpenConnectionIfClosed();
                MySqlDataReader reader = command.ExecuteReader();
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
