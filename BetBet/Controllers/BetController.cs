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
        public ActionResult Create(int count)
        {
            User loggedInUser = (User)Session["LoggedInUser"];

            if (loggedInUser != null)
            {
                UpcomingMatch match = (UpcomingMatch)TempData["Match" + count];

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

            if (ModelState.IsValid)
            {
               
                return RedirectToAction("Index");
            }

            return View(bet);
        }

      

        
        
    }
}
