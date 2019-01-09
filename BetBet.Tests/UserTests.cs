using BetBet.Data;
using BetBet.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Tests
{
    public class UserTests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            //IUserRepository userRep = new UserRepository();
            IUserRepository userRep = new FakeUserRepository();
            UserService userService = new UserService(userRep);
        }
    }
}
