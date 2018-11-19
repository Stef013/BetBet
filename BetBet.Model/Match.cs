using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Model
{
    public abstract class Match
    {
        public int MatchID { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public decimal MultiplierTeamHome { get; set; }
        public decimal MultiplierTeamAway { get; set; }
        public DateTime Date { get; set; }

        public Match (Team hometeam, Team awayteam, decimal multiplierteamhome, decimal multiplierteamaway, DateTime date)
        {
            HomeTeam = hometeam;
            AwayTeam = awayteam;
            MultiplierTeamHome = multiplierteamhome;
            MultiplierTeamAway = multiplierteamaway;
            Date = date;

        }

        public override string ToString()
        {
            return string.Format("{0} VS {1} on {2}", HomeTeam, AwayTeam, Date);
        }
    }
}
