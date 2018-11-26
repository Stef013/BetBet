using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BetBet.Model;

namespace BetBet.ViewModels
{
    public class MatchViewModel
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int SelectedHomeTeam { get; set; }
        public int SelectedAwayTeam { get; set; }
        public List<Team> TeamList { get; set; }
        public decimal MultiplierTeamHome { get; set; }
        public decimal MultiplierTeamAway { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}