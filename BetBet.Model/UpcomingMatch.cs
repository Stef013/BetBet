using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Model
{
    public class UpcomingMatch : Match
    {
        public UpcomingMatch(int hometeamID, int awayteamID, decimal multiplierteamhome, decimal multiplierteamaway, decimal multiplierdraw, DateTime date) : base(hometeamID, awayteamID, multiplierteamhome, multiplierteamaway, multiplierdraw, date)
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
