using System;
using System.Data.Entity;

namespace BetBet.ViewModels
{
    public class BetViewModel
    {
        public int BetID { get; set; }
        public int UserID { get; set; }
        public int MatchID { get; set; }
        public decimal Amount { get; set; }
        public string Prediction { get; set; }
    }

    public class BetBetDB : DbContext
    {
        public DbSet<BetViewModel> Bets { get; set; }
        public DbSet<UserViewModel> Users { get; set; }
    }
}