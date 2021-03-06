﻿using System;
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
        MatchService matchService = new MatchService();
        TeamService teamservice;
        User loggedInUser;
        
        
        [Authorize]
        public ActionResult UpcomingMatches()
        {
            loggedInUser = (User)Session["LoggedInUser"];

            if (loggedInUser != null)
            {
                List<UpcomingMatch> matchList = new List<UpcomingMatch>();
                matchList = matchService.GetUpcomingMatches();

                TempData["IsAdmin"] = loggedInUser.IsAdmin;

                return View(matchList);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult FinishedMatches()
        {
            List<FinishedMatch> matchList = new List<FinishedMatch>();
            matchList = matchService.GetFinishedMatches();

            return View(matchList);
        }

        public ActionResult FinishMatch(int count)
        {
            UpcomingMatch match = (UpcomingMatch)TempData["Match" + count];
            matchService.GenerateResult(match);
            return RedirectToAction("FinishedMatches", "Match");
        }

        public ActionResult GetSelectedMatch(int count)
        {
            UpcomingMatch match = (UpcomingMatch)TempData["Match" + count];
            Session["SelectedMatch"] = match;
            return RedirectToAction("Create", "Bet");
        }

        [HttpGet]
        public ActionResult Create()
        {
            MatchViewModel mv = new MatchViewModel();
            teamservice = new TeamService();

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

            int hometeamID = match.HomeTeamID;
            int awayteamID = match.AwayTeamID;

            if (hometeamID != 0 && awayteamID != 0 && hometeamID != awayteamID)
            {
                if (match.MultiplierTeamHome > 1 && match.MultiplierTeamAway > 1 && match.MultiplierDraw > 1)
                {
                    UpcomingMatch newMatch = new UpcomingMatch(hometeamID, awayteamID, match.MultiplierTeamHome, match.MultiplierTeamAway, match.MultiplierDraw, match.Date);
                    status = matchService.CreateMatch(newMatch);

                    if (status == true)
                    {
                        message = "Match created successfully!";
                    }
                    else
                    {
                        message = "Match could not be created.";
                    }
                }
                else
                {
                    message = "Multipliers are invalid! The values must be greater than and decimals must be separated with a comma.";
                }
                    
            }
            else
            {
                message = "Select 2 different teams.";
            }

            teamservice = new TeamService();
            match.TeamList = teamservice.GetTeams();

            ViewBag.Status = status;
            ViewBag.Message = message;

            return View(match);
        }
    }
}