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
        [Authorize]
        public ActionResult Index()
        {
            User loggedInUser = (User)Session["LoggedInUser"];

            if ( loggedInUser != null)
            {
                
                return View();
            }

            return RedirectToAction("Login", "User");
        }
        
    }
}