using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Amount:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Amount can't be empty")]
        public decimal Amount { get; set; }
        public PredictionEnum Prediction { get; set; }
        public List<Bet> BetList { get; set; }
    }
}