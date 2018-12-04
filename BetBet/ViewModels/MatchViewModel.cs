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
        public int HomeTeamID { get; set; }
        public int AwayTeamID { get; set; }
        public List<Team> TeamList { get; set; }
        [Required]
        //[DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
        public decimal MultiplierTeamHome { get; set; }
        [Required]
        //[DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
        public decimal MultiplierTeamAway { get; set; }
        [Required]
        //[DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
        public decimal MultiplierDraw { get; set; }
        [Required]
        [Display(Name = "Select Date:")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}