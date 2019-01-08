using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using BetBet.Model;
using System.Diagnostics;
using System.Data;
using System.Threading;
using System.Globalization;

namespace BetBet.Data
{
    public class UserRepository : IRepository<User, User>
    {
        BetBetDB database = new BetBetDB();

        public bool Create(User user)
        {

            MySqlCommand checkUsername = new MySqlCommand(@"SELECT Username FROM users WHERE Username = @username;");
            checkUsername.Parameters.AddWithValue("@username", user.Username);

            //string checkUsername = $"SELECT Username FROM users WHERE Username = '{user.Username}'";
             var namecheck = database.getString(checkUsername);

            if(namecheck == null)
            {
                MySqlCommand command = new  MySqlCommand("CreateUser");
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Username", MySqlDbType.VarChar).Value = user.Username;
                command.Parameters.Add("@Password", MySqlDbType.VarChar).Value = user.Password;
                command.Parameters.Add("@Balance", MySqlDbType.Decimal).Value = 0.00;
                command.Parameters.Add("@IsAdmin", MySqlDbType.Binary).Value = 0;
                
                database.ExecuteCMD(command);
                return true;
            }
            else
            {
                return false;
            } 
        }

        public int GetID(User user)
        {
            MySqlCommand command = new MySqlCommand(@"SELECT UserID FROM users WHERE Username = @username;");
            command.Parameters.AddWithValue("@username", user.Username);
            
            int id = database.GetInt(command);

            return id;
        }

        public bool GetIsAdmin(User user)
        {
            bool result;

            //string command = $"SELECT IsAdmin FROM users WHERE UserID = '{user.UserID}'";

            MySqlCommand command = new MySqlCommand(@"SELECT IsAdmin FROM users WHERE UserID = @userid;");
            command.Parameters.AddWithValue("@userid", user.UserID);

            int admin = database.GetInt(command);

            if (admin == 1)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public decimal GetBalance(int userID)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

            //string command = $"SELECT Balance FROM users WHERE UserID = '{userID}'";
            MySqlCommand command = new MySqlCommand(@"SELECT Balance FROM users WHERE UserID = @userid;");
            command.Parameters.AddWithValue("@userid", userID);
            decimal balance = database.GetDecimal(command);

            return balance;
        }

        public void Update(User user)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            //string command = $"UPDATE users SET Balance = '{user.Balance.ToString(CultureInfo.InvariantCulture)}' WHERE UserID = '{user.UserID}'";
            MySqlCommand command = new MySqlCommand(@"UPDATE users SET Balance = @balance WHERE UserID = @userid;");
            command.Parameters.AddWithValue("@userid", user.UserID);
            command.Parameters.AddWithValue("@balance", user.Balance.ToString(CultureInfo.InvariantCulture));

            database.ExecuteCMD(command);
        }

        public void ChangePassword(int userID, string password)
        {
            //string command = $"UPDATE users SET Password = '{password}' WHERE UserID = '{userID}'";
            MySqlCommand command = new MySqlCommand(@"UPDATE users SET Password = @password WHERE UserID = @userid;");
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@userid", userID);
            database.ExecuteCMD(command);
        }

        public bool ComparePassword(string username, string password)
        {
            MySqlCommand command = new MySqlCommand(@"SELECT Password FROM users WHERE Username = @username;");
            command.Parameters.AddWithValue("@username", username);
            //string getPW = $"SELECT Password FROM Users WHERE Username = '{username}'";
            string pw = database.getString(command);

            if (pw != null)
            {
                if (string.Compare(password, pw) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
