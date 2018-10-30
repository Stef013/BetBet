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
        BetBetDB db = new BetBetDB();

        public void AddUser(User user)
        {
            if (user != null)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }           
        } 
        public bool ComparePassword(User user)
        {
            var v = db.Users.Where(a => a.Username == user.Username).FirstOrDefault();
            if (v != null)
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
