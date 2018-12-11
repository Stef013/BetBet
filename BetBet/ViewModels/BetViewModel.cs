using System;
using System.Data.Entity;
using BetBet.Model;

namespace BetBet.ViewModels
{
    public class BetViewModel
    {
        public int BetID { get; set; }
        public int UserID { get; set; }
        public UpcomingMatch Match { get; set; }
        public int MatchID { get; set; }
        public decimal Amount { get; set; }
        public PredictionEnum Prediction { get; set; }
    }
}