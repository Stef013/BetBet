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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ranking()
        {
            List<Team> TeamList = new List<Team>();

            TeamList = teamservice.GetTeams();

            return View(TeamList);
        }
    }
}