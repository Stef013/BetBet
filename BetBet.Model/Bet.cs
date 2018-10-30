﻿using System;
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
        public decimal Amount { get; set; }
        public string Prediction { get; set; }
    }
}