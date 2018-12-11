using BetBet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetBet.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            User loggedInUser = (User)Session["LoggedInUser"];

            if ( loggedInUser != null)
            {
                TempData["LoggedInUser"] = loggedInUser.Username;
                return View();
            }

            return RedirectToAction("Login", "User");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}