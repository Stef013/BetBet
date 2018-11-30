﻿using System;
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
        BetBetDB database = new BetBetDB();

        public bool Create(User user)
        {

             string checkUsername = $"SELECT Username FROM users WHERE Username = '{user.Username}'";
             var namecheck = database.getString(checkUsername);

             if(namecheck == null)
             {
                 string command = $"INSERT into users (Username, Password, Balance, IsAdmin) VALUES ('{user.Username}','{user.Password}','{0.00}','{0}')";
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
            string command = $"SELECT UserID FROM users WHERE Username = '{user.Username}'";
            int id = database.getID(command);

            return id;
        }

        public bool Delete(User user)
        {
            if (user != null)
            {
                int id = GetID(user);
                string command = $"DELETE * Where UserID =  '{id}'";
                database.ExecuteCMD(command);
                return true;
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
