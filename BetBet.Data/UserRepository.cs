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
                string command = $"INSERT into Users (UserName, Password, Balance) VALUES ('{user.Username}','{user.Password}','{0.00}')";
                database.InsertOrRemoveCMD(command);
                //db.Users.Add(user);
                //db.SaveChanges();
            }           
        } 
        public bool ComparePassword(string username, string password)
        {
            //var v = db.Users.Where(a => a.Username == username).FirstOrDefault();
            /*var v = 12;
            if (v != null)
            {
                if (string.Compare(password, v.Password) == 0)
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
            }*/
            return false;
        }
    }
}
