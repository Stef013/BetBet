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

        [Authorize]
        [HttpGet]
        public ActionResult BetList()
        {
            User loggedInUser = (User)Session["LoggedInUser"];

            if (loggedInUser != null)
            {
                List<Bet> betList = new List<Bet>();
                betList = betservice.GetBetsFromUser(loggedInUser);

                BetViewModel betmodel = new BetViewModel();

                betmodel.BetList = betList;
                
                return View(betList);
            }
            else
            {
               return RedirectToAction("Login", "User");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            User loggedInUser = (User)Session["LoggedInUser"];

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

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BetViewModel bet)
        {
            bool status = false;
            string message;

            User loggedinUser = (User)Session["LoggedInUser"];
            UpcomingMatch match = (UpcomingMatch)Session["SelectedMatch"];
            UserService userservice = new UserService();

            if (ModelState.IsValid)
            {
                if (bet.Amount > 5)
                {
                    if (bet.Amount < loggedinUser.Balance)
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
                            userservice.RemoveFunds(loggedinUser, newbet.Amount);
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
                message = "Invalid request.";
            }

            ViewBag.Status = status;
            ViewBag.Message = message;

            return View(bet);
        }

        public ActionResult Delete(int betID, decimal amount)
        {
            User loggedInUser = (User)Session["LoggedInUser"];

            bool isDeleted = betservice.DeleteBet(betID, amount, loggedInUser);            
            
            return RedirectToAction("BetList");
        }
    }
}
