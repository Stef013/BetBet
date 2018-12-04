using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetBet.ViewModels;
using BetBet.Model;
using BetBet.Logic;
using System.Globalization;
using System.Threading;

namespace BetBet.Controllers
{
    public class MatchController : Controller
    {
        TeamService teamservice = new TeamService();
        MatchService matchService = new MatchService();
        // GET: Match
        public ActionResult UpcomingMatches()
        {
            List<UpcomingMatch> matchList = new List<UpcomingMatch>();
            matchList = matchService.getUpcomingMatches();

            return View(matchList);
        }

        public ActionResult FinishedMatches()
        {
            List<UpcomingMatch> matchList = new List<UpcomingMatch>();
            matchList = matchService.getUpcomingMatches();

            return View(matchList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            MatchViewModel mv = new MatchViewModel();

            List<Team> TeamList = new List<Team>();
            TeamList = teamservice.GetTeams();

            mv.TeamList = TeamList.ToList();

            //TempData["VM"] = mv;

            return View(mv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MatchViewModel match)
        {
            bool status = false;
            string message;

            int hometeamID = match.HomeTeamID;
            int awayteamID = match.AwayTeamID;

            if (hometeamID != 0 && awayteamID != 0 && hometeamID != awayteamID)
            {
                if (match.MultiplierTeamHome != 0 && match.MultiplierTeamAway != 0 && match.MultiplierDraw != 0)
                {
                    UpcomingMatch newMatch = new UpcomingMatch(hometeamID, awayteamID, match.MultiplierTeamHome, match.MultiplierTeamAway, match.MultiplierDraw, match.Date);
                    status = matchService.CreateMatch(newMatch);

                    if (status == true)
                    {
                        message = "Match created successfully.";
                    }
                    else
                    {
                        message = "Match could not be created.";
                    }
                }
                else
                {
                    status = false;
                    message = "Multipliers are invalid.";
                }
                    
            }
            else
            {
                status = false;
                message = "Select 2 different teams.";
            }

            ViewBag.Status = status;
            ViewBag.Message = message;

            return View();
        }
    }
}