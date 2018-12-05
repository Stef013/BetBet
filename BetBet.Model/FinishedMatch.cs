using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Model
{
    public class FinishedMatch : Match
    {
        public int ScoreHome { get; set; }
        public int ScoreAway { get; set; }
        public int CardsHome { get; set; }
        public int CardsAway { get; set; }

        public FinishedMatch (int matchID, int hometeamID, int awayteamID, string hometeamname, string awayteamname, decimal multiplierhome, decimal multiplieraway, decimal multiplierdraw, DateTime date, int scoreteamhome, int scoreteamaway,
            int cardshome, int cardsaway) : base(matchID, hometeamID, awayteamID, hometeamname, awayteamname, multiplierhome, multiplieraway, multiplierdraw, date)
        {
            ScoreHome = scoreteamhome;
            ScoreAway = scoreteamaway;
            CardsHome = cardshome;
            CardsAway = cardsaway;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
