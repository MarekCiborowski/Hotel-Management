using DataAccessLayer;
using DataTransferObjects.Models;
using DomainObjects.Entities;
using Repositories;
using Repositories.Repositories;
using RepositoryLayer.Repositories;
using Survey_MVC.ViewModels.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.ViewModels.Authentication;

namespace WebApplication.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private DatabaseContext databaseContext;
        private UserRepository userRepository;
        
        public AuthenticationController()
        {
            this.databaseContext = new DatabaseContext();
            this.userRepository = new UserRepository(this.databaseContext);
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
                if ((user = this.userRepository.GetUser(vm.username, vm.password)) != null)
                {
                    Session["CurrentUser"] = user;
                    
                    TempData["message"] = "Successfully logged as " + user.Login;
                    return RedirectToAction("Index", "Home");
                }

            }
            ModelState.AddModelError("CredentialError", "Niepoprawna nazwa użytkownika lub hasło");
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
            if (!userSecurityRepository.IsLoginFree(newUser.login))
            {
                ModelState.AddModelError("login", "Login is already in use.");
                isValid = false;
            }
            if (!accountRepository.IsEmailCorrect(newUser.email))
            {
                ModelState.AddModelError("email", "Email is taken or not correct.");
                isValid = false;
            }
            if(!accountRepository.IsNicknameCorrect(newUser.nickname))
            {
                ModelState.AddModelError("nickname", "This nickname is taken or not correct. Length of nickname is 3-10 characters.");
                isValid = false;
            }
            
            if (ModelState.IsValid && isValid)
            {
                UserSecurity userSecurity = userSecurityRepository.CreateUserSecurity(newUser.login, newUser.password);
                PersonData personData = personDataRepository.CreatePersonData(newUser.address,
                    newUser.city, newUser.zipcode, newUser.state, newUser.country);
                Account account = accountRepository.CreateAccount(personData, newUser.email, newUser.nickname, userSecurity);
                accountRepository.AddAccount(account);
                TempData["message"] = "Successfully added new account: " + account.nickname;
                return RedirectToAction("Login");

            }
           
            return View(newUser);
        }
    }
}