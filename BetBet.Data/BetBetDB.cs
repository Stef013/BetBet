using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using System.Data.SqlClient;
using BetBet.Model;

namespace BetBet.Data
{
    public class BetBetDB
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Stefan\Documents\School\S2 Software\LP\BetBet\BetBet\App_Data\BetBetDB.mdf;Initial Catalog=aspnet-BetBet;Integrated Security=True";
        //SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Stefan\Documents\School\S2 Software\LP\BetBet\BetBet\App_Data\BetBetDB.mdf;Initial Catalog=aspnet-BetBet;Integrated Security=True");

        /*public void Connect()
        {
            conn.Open();
            
        }*/
        public void InsertOrRemoveCMD(string command)
        {
            SqlConnection sqlcon = new SqlConnection(connString);
            try
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand(command, sqlcon);
                cmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                sqlError(ex);
            }
        }



        public void testDbCon()
        {
            SqlConnection sqlcon = new SqlConnection(connString);
            try
            {
                sqlcon.Open();
                Console.WriteLine("SQL server connected!");
                sqlcon.Close();
            }
            catch
            {
                sqlError();
            }
        }

        private void sqlError()
        {
            Console.WriteLine("Can't connect to the SQL Server!");
        }

        private void sqlError(Exception ex)
        {
            Console.WriteLine("Something went wrong!" + "Environment.NewLine" + ex.ToString());
        }
    }
}
