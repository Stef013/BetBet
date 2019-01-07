using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Model
{
    public class UpcomingMatch : Match
    {
        public UpcomingMatch(int hometeamID, int awayteamID, decimal multiplierhome, decimal multiplieraway, decimal multiplierdraw, DateTime date) : base(hometeamID, awayteamID, multiplierhome, multiplieraway, multiplierdraw, date)
        {

        }
        public UpcomingMatch(int matchID, Team homeTeam, Team awayTeam, decimal multiplierhome, decimal multiplieraway, decimal multiplierdraw, DateTime date) : base(matchID, homeTeam, awayTeam, multiplierhome, multiplieraway, multiplierdraw, date)
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
