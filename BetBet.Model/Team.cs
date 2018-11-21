using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBet.Model
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string City { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int Draws { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalSaldo { get; set; }
        public int Points { get; set; }
       
    }
}
