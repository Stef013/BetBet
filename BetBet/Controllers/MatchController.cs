using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetBet.ViewModels;
using BetBet.Model;
using BetBet.Logic;

namespace BetBet.Controllers
{
    public class MatchController : Controller
    {
        TeamService teamservice = new TeamService();
        MatchService matchService = new MatchService();
        // GET: Match
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            MatchViewModel mv = new MatchViewModel();

            List<Team> TeamList = new List<Team>();
            TeamList = teamservice.GetTeams();

            mv.TeamList = TeamList.ToList();


            return View(mv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MatchViewModel match)
        {
            bool status = false;
            string message;

            int hometeamID = match.SelectedHomeTeam;
            int awayteamID = match.SelectedAwayTeam;

            if (hometeamID != 0 && awayteamID != 0 && hometeamID != awayteamID)
            {
                UpcomingMatch newMatch = new UpcomingMatch(hometeamID, awayteamID, match.MultiplierTeamHome, match.MultiplierTeamAway, match.Date);
                matchService.CreateMatch(newMatch);

                status = true;
                message = "Match created successfully.";
            }
            else
            {
                status = true;
                message = "Select 2 different teams.";
            }

            ViewBag.Status = status;
            ViewBag.Message = message;
            return View();
        }
    }
}