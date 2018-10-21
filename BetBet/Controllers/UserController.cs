using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetBet.Models;

namespace BetBet.Controllers
{
    public class UserController : Controller
    {
        private BetBetDB db = new BetBetDB();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "UserID, Balance")] User user)
        {
            bool status = false;
            string message;

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                message = "Account created successfully.";
                status = true;
            }

            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(user);
        }
    }
}