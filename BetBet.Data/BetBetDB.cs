using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BetBet.Model;

namespace BetBet.Data
{
    public class BetBetDB : DbContext
    {
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
