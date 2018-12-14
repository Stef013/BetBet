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

namespace BetBet.Controllers
{
    public class BetController : Controller
    {
       

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
                BetViewModel newbet = new BetViewModel();
                newbet.Match = match;
                return View(newbet);
            }

            return RedirectToAction("Login", "User");
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
                            //--------------------------Bet aanmaken afmaken, service/repository-------------------------
                            status = true;
                            message = "Bet succesfully placed!";
                        }
                        else
                        {
                            message = "Not enough funds to make this bet.";
                        }
                        
                    }
                    else
                    {
                        message = "Bet amount must be higher than 5,-";
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

            return View();
        }
    }
}
