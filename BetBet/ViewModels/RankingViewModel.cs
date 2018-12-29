using BetBet.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BetBet.ViewModels
{
    public class RankingViewModel
    {
        [Display(Name = "Team")]
        public string TeamName { get; set; }
        public string City { get; set; }
        [Display(Name = "Pld")]
        public int GamesPlayed { get; set; }
        [Display(Name = "W")]
        public int GamesWon { get; set; }
        [Display(Name = "L")]
        public int GamesLost { get; set; }
        [Display(Name = "D")]
        public int Draws { get; set; }
        [Display(Name = "GF")]
        public int GoalsFor { get; set; }
        [Display(Name = "GA")]
        public int GoalsAgainst { get; set; }
        [Display(Name = "GD")]
        public int GoalDifferential { get; set; }
        [Display(Name = "Pts")]
        public int Points { get; set; }
        public List<Team> TeamList { get; set; }
    }
}