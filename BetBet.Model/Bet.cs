using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Model
{
    public class Bet
    {
        public int BetID { get; set; }
        public int UserID { get; set; }
        public int MatchID { get; set; }
        public Match Match { get; set; }
        public decimal Amount { get; set; }
        public MatchResult Prediction { get; set; }
        public bool HasEnded { get; set; }
        public BetResult Result { get; set; }
        public decimal Earned { get; set; }
    }
}
