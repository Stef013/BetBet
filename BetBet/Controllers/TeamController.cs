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
    public class TeamController : Controller
    {
        TeamService teamservice = new TeamService();
        MatchService matchservice;

        public ActionResult Ranking()
        {
            RankingViewModel vmodel = new RankingViewModel();
            vmodel.TeamList = teamservice.GetTeams();
            return View(vmodel);
        }

        public ActionResult Details(int id)
        {
            matchservice = new MatchService();
            Team team = teamservice.GetTeam(id);

            TeamViewModel vmodel = new TeamViewModel
            {
                TeamID = team.TeamID,
                TeamName = team.TeamName,
                City = team.City,
                GamesPlayed = team.GamesPlayed,
                GamesWon = team.GamesWon,
                Draws = team.Draws,
                GamesLost = team.GamesLost,
                GoalsFor = team.GoalsFor,
                GoalsAgainst = team.GoalsAgainst,
                GoalDifferential = team.GoalDifferential,
                Points = team.Points,
            };

            vmodel.MatchList = matchservice.GetFinishedMatchesByTeam(id);

            return View(vmodel);
        }
    }
}