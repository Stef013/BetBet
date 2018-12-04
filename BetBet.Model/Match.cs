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
        public int HomeTeamID { get; set; }
        public int AwayTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public decimal MultiplierHome { get; set; }
        public decimal MultiplierAway { get; set; }
        public decimal MultiplierDraw { get; set; }

        public DateTime Date { get; set; }

        public Match (int hometeamID, int awayteamID, decimal multiplierhome, decimal multiplierteamaway, decimal multiplierdraw, DateTime date)
        {
            HomeTeamID = hometeamID;
            AwayTeamID = awayteamID;
            MultiplierHome = multiplierhome;
            MultiplierAway = multiplierteamaway;
            MultiplierDraw = multiplierdraw;
            Date = date;
        }

        public Match(int matchID, int hometeamID, int awayteamID, string hometeamname, string awayteamname, decimal multiplierteamhome, decimal multiplierteamaway, decimal multiplierdraw, DateTime date)
        {
            MatchID = matchID;
            HomeTeamID = hometeamID;
            AwayTeamID = awayteamID;
            HomeTeamName = hometeamname;
            AwayTeamName = awayteamname;
            MultiplierHome = multiplierteamhome;
            MultiplierAway = multiplierteamaway;
            MultiplierDraw = multiplierdraw;
            Date = date;
        }

        public override string ToString()
        {
            return string.Format("{0} VS {1} on {2}", HomeTeamID, AwayTeamID, Date);
        }
    }
}
