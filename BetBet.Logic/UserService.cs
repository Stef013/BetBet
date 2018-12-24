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
        UserRepository userRep = new UserRepository();

        public bool CreateUser(User user)
        {
            bool usercheck = false;

            user.Password = Hash(user.Password);

            if (user != null)
            {
                usercheck = userRep.Create(user);
                
                return usercheck;
            }
            else
            {
                return usercheck;
            }
        }

        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }

        public User getLoggedinUserData(string username)
        {
            User user = new User
            {
                Username = username
            };

            user.UserID = userRep.GetID(user);
            user.Balance = userRep.GetBalance(user);
            user.IsAdmin = userRep.GetIsAdmin(user);

            return user;
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
                password = Hash(password);
                bool checkPassword = userRep.ComparePassword(username, password);

                return checkPassword;
            }
        }

        public void AddFunds(User user, decimal amount)
        {
            decimal newBalance = user.Balance + amount;
            user.Balance = newBalance;

            userRep.Update(user);
        }

        public void RemoveFunds(User user, decimal amount)
        {
            decimal newBalance = user.Balance - amount;
            user.Balance = newBalance;

            userRep.Update(user);
        }
    }
}
