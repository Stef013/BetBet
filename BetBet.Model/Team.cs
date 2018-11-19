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
        public int Points { get; set; }
        public int Goals { get; set; }
        public int Cards { get; set; }


    }
}
