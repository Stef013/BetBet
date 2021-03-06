﻿using System;
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
        private UserService userservice = new UserService();

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
            bool status = false;
            string message;

            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    Username = user.Username,
                    Password = user.Password
                };

                status = userservice.CreateUser(newUser);

                if ( status == true)
                {
                    message = "Account created successfully.";
                }
                else
                {
                    message = "Username already exists.";
                }
            }

            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = status;
            return View();
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
            
            bool usercheck = userservice.ComparePassword(login.Username, login.Password);

            if(usercheck == true)
            {
                int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                var ticket = new FormsAuthenticationTicket(login.Username, login.RememberMe, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);

                User loggedinUser = userservice.getLoggedinUserData(login.Username);

                Session["LoggedinUser"] = loggedinUser;

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
                message = "Invalid credentials provided";
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

        [Authorize]
        [HttpGet]
        public ActionResult AccountSettings()
        {
            User loggedInUser = (User)Session["loggedInUser"];

            if (loggedInUser != null)
            {
                TempData["IsAdmin"] = loggedInUser.IsAdmin;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        [HttpPost]
        public ActionResult ChangePassword(SettingsViewModel settings)
        {
            bool status = false;
            string message;

            if (ModelState.IsValid)
            {
                User loggedInUser = (User)Session["LoggedInUser"];

                bool oldPasswordCheck = userservice.ComparePassword(loggedInUser.Username, settings.OldPassword);

                if (oldPasswordCheck == true)
                {
                    userservice.ChangePassword(loggedInUser.UserID, settings.NewPassword);
                    status = true;
                    message = "Password successfully changed!";
                }
                else
                {
                    message = "Current password is invalid";
                }
            }
            else
            {
                message = "Invalid request";
            }

            ViewBag.Status = status;
            ViewBag.Message = message;

            return View("AccountSettings");
        }

        [HttpPost]
        public ActionResult UpdateBalance(string DepositButton, string WithdrawButton, SettingsViewModel settings)
        {
            bool status = false;
            string message;
            User loggedInUser = (User)Session["LoggedInUser"];
            if (settings.Funds > 0)
            {
                if (DepositButton != null)
                {
                    userservice.AddFunds(loggedInUser, settings.Funds);
                    status = true;
                    message = "Funds added to you account balance.";
                }
                else if (WithdrawButton != null)
                {
                    if (settings.Funds <= loggedInUser.Balance)
                    {
                        userservice.RemoveFunds(loggedInUser, settings.Funds);
                        status = true;
                        message = "Funds removed from your account balance.";
                    }
                    else
                    {
                        message = "Can't withdraw more than your balance.";
                    }
                }
                else
                {
                    message = "Invalid request";
                }
            }
            else
            {
                message = "Please insert an amount";
            }
            ViewBag.Status = status;
            ViewBag.Message = message;
            return View("AccountSettings");
        }
        
    }
}