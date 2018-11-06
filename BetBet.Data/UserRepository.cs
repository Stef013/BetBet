using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBet.Model;

namespace BetBet.Data
{
    public class UserRepository
    {
        BetBetDB database = new BetBetDB();

        public void AddUser(User user)
        {
            if (user != null)
            {
                string command = $"INSERT into users (Username, Password, Balance, IsAdmin) VALUES ('{user.Username}','{user.Password}','{0.00}','{0}')";
                database.executeCMD(command);
            }           
        } 
        public bool ComparePassword(string username, string password)
        {
            string getPW = $"SELECT Password FROM Users WHERE Username = '{username}'";
            string pw = database.getString(getPW);
            
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
