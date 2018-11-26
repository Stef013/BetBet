using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Model
{
    public class FinishedMatch : Match
    {
        public int ScoreTeamHome { get; set; }
        public int ScoreTeamAway { get; set; }
        public int CardsHome { get; set; }
        public int CardsAway { get; set; }

        public FinishedMatch (int hometeamID, int awayteamID, decimal multiplierteamhome, decimal multiplierteamaway, DateTime date, int scoreteamhome, int scoreteamaway,
            int cardshome, int cardsaway) : base(hometeamID, awayteamID, multiplierteamhome, multiplierteamaway, date)
        {
            ScoreTeamHome = scoreteamhome;
            ScoreTeamAway = scoreteamaway;
            CardsHome = cardshome;
            CardsAway = cardsaway;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
