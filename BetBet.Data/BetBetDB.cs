using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using BetBet.Model;
using System.Data;

namespace BetBet.Data
{
    public class BetBetDB
    {
        public string Hostname = "studmysql01.fhict.local";
        public string DBName = "dbi382222";
        public string ID = "dbi382222";
        public string Password = "vQzTCdskA2UBvfzp";

        private string connectionString()
        {
            return $"SERVER={Hostname};DATABASE={DBName};UID={ID};PASSWORD={Password};SslMode=none";

        }

        public void executeCMD(string command)
        {
            MySqlConnection mySqlcon = new MySqlConnection(connectionString());
            try
            {
                mySqlcon.Open();
                MySqlCommand cmd = new MySqlCommand(command, mySqlcon);
                cmd.ExecuteNonQuery();
                mySqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MysqlError(ex);
            }
        }

        public string getString(string command)
        {
            MySqlConnection mySqlcon = new MySqlConnection(connectionString());
            string result = null;
            try
            {
                mySqlcon.Open();
                MySqlCommand cmd = new MySqlCommand(command, mySqlcon);
                result = (string)cmd.ExecuteScalar();
                mySqlcon.Close();
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MysqlError(ex);
                return result;
            }
        }
        public int getID(string command)
        {
            MySqlConnection mySqlcon = new MySqlConnection(connectionString());
            int result = 0;
            try
            {
                mySqlcon.Open();
                MySqlCommand cmd = new MySqlCommand(command, mySqlcon);
                result = (int)cmd.ExecuteScalar();
                mySqlcon.Close();
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MysqlError(ex);
                return result;
            }
        }

        public MySqlDataReader ReadMysql(string command)
        {
            MySqlConnection mySqlcon = new MySqlConnection(connectionString());
            MySqlDataReader result = null;
            try
            {
                mySqlcon.Open();
                MySqlCommand cmd = new MySqlCommand(command, mySqlcon);
                //result = (string)cmd.ExecuteScalar();
                result = cmd.ExecuteReader();
                // Mysqlc.Close();
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MysqlError(ex);
                return result;
            }
        }

        public void testDbCon()
        {
            MySqlConnection mySqlcon = new MySqlConnection(connectionString());
            try
            {
                mySqlcon.Open();
                Console.WriteLine("Mysql server connected!");
                mySqlcon.Close();
            }
            catch
            {
                MySqlError();
            }
        }

        private void MySqlError()
        {
            Console.WriteLine("Can't connect to the Mysql Server!");
        }

        private void MysqlError(Exception ex)
        {
            Console.WriteLine("Something went wrong!" + "Environment.NewLine" + ex.ToString());
        }
    }
}
