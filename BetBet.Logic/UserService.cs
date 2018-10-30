using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBet.Data;
using BetBet.Model;

namespace BetBet.Logic
{
    public class UserService
    {
        UserRepository UserRep = new UserRepository();

        public bool CreateUser(User user)
        {
            bool usercheck = false;

            if (user != null)
            {
                UserRep.AddUser(user);
                usercheck = true;
                return usercheck;
            }
            else
            {
                return usercheck;
            }
        }

        public bool ComparePassword(string username, string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password) 
                || string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            else
            {
                bool checkPassword = UserRep.ComparePassword(username, password);

                if (checkPassword == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
