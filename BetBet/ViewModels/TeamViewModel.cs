using BetBet.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BetBet.ViewModels
{
    public class TeamViewModel
    {
        public int TeamID { get; set; }
        [Display(Name = "Team: ")]
        public string TeamName { get; set; }
        [Display(Name = "City: ")]
        public string City { get; set; }
        [Display(Name = "Matches played: ")]
        public int GamesPlayed { get; set; }
        [Display(Name = "Won: ")]
        public int GamesWon { get; set; }
        [Display(Name = "Lost: ")]
        public int GamesLost { get; set; }
        [Display(Name = "Draws: ")]
        public int Draws { get; set; }
        [Display(Name = "Goals made: ")]
        public int GoalsFor { get; set; }
        [Display(Name = "Goals recieved: ")]
        public int GoalsAgainst { get; set; }
        [Display(Name = "Goal Differential: ")]
        public int GoalDifferential { get; set; }
        [Display(Name = "Points: ")]
        public int Points { get; set; }
        public List<FinishedMatch> MatchList { get; set; }
    }
}