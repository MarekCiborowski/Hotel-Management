using BusinessLogic.Services;
using DataAccessLayer;
using DomainObjects.Entities;
using Repositories;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Filters;
using WebApplication.ViewModels.Authentication;

namespace WebApplication.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly DatabaseContext databaseContext;
        private UserService userService;
        
        public AuthenticationController()
        {
            this.databaseContext = new DatabaseContext();
            this.userService = new UserService(this.databaseContext);
        }
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM vm)
        {
            if (ModelState.IsValid)
            {
                User user;
                if ((user = this.userService.GetUser(vm.Username, vm.Password)) != null)
                {
                    if (!user.IsConfirmed)
                    {
                        TempData["message"] = "User has not been confirmed yet";
                        return View(vm);
                    }
                    Session["CurrentUser"] = user;
                    
                    TempData["message"] = "Successfully logged as " + user.Login;
                    if(user.RoleId == DomainObjects.Enums.RolesEnum.RegularUser)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Reservations", "Admin");
                }

            }
            ModelState.AddModelError("CredentialError", "Invalid username or password");
            return View(vm);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult NewUser()
        {
            
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult NewUser(NewUserVM newUser)
        {
            bool isValid = true;
            if (!userService.IsLoginFree(newUser.Login))
            {
                ModelState.AddModelError("login", "Login is already in use.");
                isValid = false;
            }
            if (!userService.IsEmailCorrect(newUser.Email))
            {
                ModelState.AddModelError("email", "Email is taken or not correct.");
                isValid = false;
            }
            
            if (ModelState.IsValid && isValid)
            {
                var user = this.userService.AddUser(newUser.ToEntityModel());
                TempData["message"] = "Successfully added new account: " + user.Login;
                return RedirectToAction("Login");

            }
           
            return View(newUser);
        }

        [AdminAuthorizationFilter]
        public ActionResult NewAdmin()
        {
            return View();
        }

        [AdminAuthorizationFilter]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult NewAdmin(NewUserVM newUser)
        {
            bool isValid = true;
            if (!userService.IsLoginFree(newUser.Login))
            {
                ModelState.AddModelError("login", "Login is already in use.");
                isValid = false;
            }
            if (!userService.IsEmailCorrect(newUser.Email))
            {
                ModelState.AddModelError("email", "Email is taken or not correct.");
                isValid = false;
            }

            if (ModelState.IsValid && isValid)
            {
                var user = this.userService.AddAdmin(newUser.ToEntityModel());
                TempData["message"] = "Successfully added new account: " + user.Login;
                return RedirectToAction("Login");

            }

            return View(newUser);
        }
    }
}