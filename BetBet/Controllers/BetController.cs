using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BetBet.Model;
using BetBet.ViewModels;
using BetBet.Logic;

namespace BetBet.Controllers
{
    public class BetController : Controller
    {
        BetService betservice = new BetService();

        // GET: Bets
        public ActionResult Index()
        {
            return View();
        }

        // GET: Bets/Details/5
        

        // GET: Bets/Create
        [HttpGet]
        public ActionResult Create()
        {
            User loggedInUser = (User)Session["LoggedInUser"];

            if (loggedInUser != null)
            {
                UpcomingMatch match = (UpcomingMatch)Session["SelectedMatch"];

                bool betExists = betservice.CheckIfBetExists(match.MatchID, loggedInUser.UserID);

                if (betExists == false)
                {
                    BetViewModel newbet = new BetViewModel
                    {
                        Match = match
                    };
                   
                    return View(newbet);
                }
                else
                {
                    return RedirectToAction("UpcomingMatches", "Match");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: Bets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BetViewModel bet)
        {
            bool status = false;
            string message;

            User loggedinUser = (User)Session["LoggedInUser"];
            UpcomingMatch match = (UpcomingMatch)Session["SelectedMatch"];

            if (ModelState.IsValid)
            {
                if (bet.Prediction != 0)
                {
                    if(bet.Amount > 5)
                    {
                        if(bet.Amount < loggedinUser.Balance)
                        {
                            Bet newbet = new Bet()
                            {
                                UserID = loggedinUser.UserID,
                                MatchID = match.MatchID,
                                Prediction = bet.Prediction,
                                Amount = bet.Amount
                            };
                           
                            status = betservice.CreateBet(newbet);
                            if (status == true)
                            {
                                message = "Bet succesfully placed!";
                            }
                            else
                            {
                                message = "Database Error!";
                            }
                        }
                        else
                        {
                            message = "Not enough funds to make this bet.";
                        }
                        
                    }
                    else
                    {
                        message = "Bet amount must be higher than 5,- or try to use a comma.";
                    }

                }
                else
                {
                    message = "No prediction selected.";
                }  
            }
            else
            {
                message = "Invalid request.";
            }

            ViewBag.Status = status;
            ViewBag.Message = message;

            return View(bet);
        }
    }
}
