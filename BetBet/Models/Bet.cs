using System;
using System.Data.Entity;

namespace BetBet.Models
{
    public class Bet
    {
        public int BetID { get; set; }
        public int UserID { get; set; }
        public int MatchID { get; set; }
        public decimal Amount { get; set; }
        public string Prediction { get; set; }
    }

    public class BetBetDB : DbContext
    {
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}