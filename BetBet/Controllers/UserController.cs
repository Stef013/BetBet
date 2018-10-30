using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BetBet.ViewModels;
using BetBet.Model;
using BetBet.Logic;

namespace BetBet.Controllers
{
    public class UserController : Controller
    {
        private BetBetDB db = new BetBetDB();
        private UserService us = new UserService();

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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "UserID, Balance")] User user)
        {
            bool status = false;
            string message;

            if (ModelState.IsValid)
            {              
                status = us.CreateUser(user);
                message = "Account created successfully.";
                //status = true;
            }

            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel login, string ReturnUrl = "")
        {
            string message = "";

            var v = db.Users.Where(a => a.Username == login.Username).FirstOrDefault();
            if (v != null)
            {
                if (string.Compare(login.Password, v.Password) == 0)
                {
                    int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                    var ticket = new FormsAuthenticationTicket(login.Username, login.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            else
            {
                message = "Invalid credential provided";
            }

            ViewBag.Message = message;
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }
    }
}