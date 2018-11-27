using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBet.Data;
using BetBet.Model;

namespace BetBet.Logic
{
    public class MatchService
    {
        MatchRepository matchrep = new MatchRepository();

        public void CreateMatch(Match match)
        {
            matchrep.Create(match);
        }

    }
}
