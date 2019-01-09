using BetBet.Data;
using BetBet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Tests
{
    public class FakeUserRepository : IUserRepository
    {
        public void ChangePassword(int userID, string password)
        {
            throw new NotImplementedException();
        }

        public bool ComparePassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool Create(User obj)
        {
            throw new NotImplementedException();
        }

        public decimal GetBalance(int userID)
        {
            throw new NotImplementedException();
        }

        public int GetID(User obj)
        {

            throw new NotImplementedException();
        }

        public bool GetIsAdmin(User user)
        {
            throw new NotImplementedException();
        }

        public void Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
