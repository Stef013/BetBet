using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBet.Model;
using BetBet.Data;

namespace BetBet.Logic
{
    public class BetService
    {
        BetRepository betrep = new BetRepository();

        public bool CheckIfBetExists(int matchID, int userID)
        {
            bool result;

            int betExists = betrep.CheckBet(matchID, userID);

            if (betExists == 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

        public bool CreateBet(Bet bet)
        {
            bool result = betrep.Create(bet);

            return result;
        }
    }
}
