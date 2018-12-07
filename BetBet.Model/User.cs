using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetBet.Model
{
    public class User
    {
        
        public int UserID { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public decimal Balance { get; set; }

        public User (string username, string password)
        {
            Username = username;
            Password = password;
        }
        public bool IsAdmin { get; set; }
        
    }
}
