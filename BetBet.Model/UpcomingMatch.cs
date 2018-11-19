using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Model
{
    public class UpcomingMatch : Match
    {
        public UpcomingMatch(Team hometeam, Team awayteam, decimal multiplierteamhome, decimal multiplierteamaway, DateTime date) : base(hometeam, awayteam, multiplierteamhome, multiplierteamaway, date)
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
