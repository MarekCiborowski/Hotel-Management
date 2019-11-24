using BusinessLogic.Services;
using DataAccessLayer;
using DomainObjects.Dto;
using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Filters;
using WebApplication.ViewModels.Home;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext databaseContext;
        private UserService userService;
        private RoomService roomService;
        private ConversationService conversationService;

        public HomeController()
        {
            this.databaseContext = new DatabaseContext();
            this.userService = new UserService(this.databaseContext);
            this.roomService = new RoomService(this.databaseContext);
            this.conversationService = new ConversationService(this.databaseContext);
        }

        public ActionResult Index()
        {
            var roomSearchVM = new RoomSearchVM
            {
                AmenitiesToSearch = roomService.GetAllAmenities().Select(a => new AmenitySearchVM
                {
                    AmenityId = a.AmenityId,
                    AmenityName = a.AmenityName
                }).ToList(),
            };
            
            return View(roomSearchVM);
        }

        [HttpPost]
        public ActionResult Index(RoomSearchVM roomSearchVM)
        {
            return View(roomSearchVM);
        }

        public ActionResult UserConversations()
        {
            var currentUser = (User)Session["CurrentUser"];
            var conversations = this.conversationService.GetUserConversations(currentUser.Identity);
            var conversationsVM = conversations.Select(c => new UserConversationsVM
            {
                ConversationId = c.ConversationID,
                ConversationTitle = c.Title
            });

            return View(conversationsVM);
        }

        public ActionResult Conversation(int conversationId)
        {
            var conversation = this.conversationService.GetConversationDto(conversationId);

            return View(conversation);
        }

        [HttpPost]
        public ActionResult Conversation(ConversationDto conversationDto)
        {
            if (ModelState.IsValid)
            {
                var currentUser = (User)Session["CurrentUser"];
                this.conversationService.AddMessageToConversation(conversationDto.NewMessage, currentUser.Identity, conversationDto.ConversationId);
                return Conversation(conversationDto.ConversationId);
            }

            return Conversation(conversationDto);
        }

        [UserAuthorizationFilter]
        public ActionResult NewConversation()
        {
            var newConversationDto = new NewConversationDto();
            return View(newConversationDto);
        }

        [HttpPost]
        [UserAuthorizationFilter]
        public ActionResult NewConversation(NewConversationDto newConversationDto)
        {
            if (ModelState.IsValid)
            {
                var receiver = this.userService.GetAdminUserWithLeastUserConversations();
                var currentUser = (User)Session["CurrentUser"];
                var newConversation = this.conversationService.AddConversationWithInitialMessage(currentUser.Identity, receiver.Identity,
                    newConversationDto.NewMessage, newConversationDto.NewConversationTitle);

                return Conversation(newConversation.ConversationID);
            }

            return View(newConversationDto);
        }

        //public ActionResult MyProfile()
        //{
        //    Account account = (Account)Session["CurrentUser"];
        //    MyProfileVM myProfileVM = new MyProfileVM
        //    {
        //        login = account.userSecurity.login,
        //        Email = account.email,
        //        nickname = account.nickname,
        //        address = account.personData.address,
        //        city = account.personData.city,
        //        country = account.personData.country,
        //        state = account.personData.state,
        //        zipcode = account.personData.zipcode,
        //        isProfilePublic = account.personData.isProfilePublic,
        //    };
        //    myProfileVM.followers = accountRepository.GetQuantityOfFollowersByID(account.accountID);
        //    myProfileVM.followed = accountRepository.GetFollowedAccounts(account.accountID).Count;
        //    return View(myProfileVM);
        //}

        //public ActionResult EditProfile()
        //{
        //    User user = (User)Session["CurrentUser"];
        //    MyProfileVM myProfileVM = new MyProfileVM
        //    {
        //        login = user.login,
        //        Email = user.email,
        //        nickname = user.nickname,
        //        address = user.personData.address,
        //        city = user.personData.city,
        //        country = user.personData.country,
        //        state = user.personData.state,
        //        zipcode = user.personData.zipcode,
        //        isProfilePublic = user.personData.isProfilePublic
        //    };
        //    return View(myProfileVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditProfile(MyProfileVM myProfileVM)
        //{
        //    Account account = (Account)Session["CurrentUser"];
        //    bool isValid = true;
        //    if (account.email != myProfileVM.Email)
        //        if (!accountRepository.IsEmailCorrect(myProfileVM.Email))
        //        {
        //            ModelState.AddModelError("email", "Email is taken or not correct.");
        //            isValid = false;
        //        }

        //    if (account.nickname != myProfileVM.nickname)
        //        if (!accountRepository.IsNicknameCorrect(myProfileVM.nickname))
        //        {
        //            ModelState.AddModelError("nickname", "This nickname is taken or not correct. Length of nickname is 3-10 characters.");
        //            isValid = false;
        //        }

        //    if (ModelState.IsValid && isValid)
        //    {
        //        Account editedAccount = accountRepository.GetAccount(account.accountID);
        //        editedAccount.personData.address = myProfileVM.address;
        //        editedAccount.personData.city = myProfileVM.city;
        //        editedAccount.personData.zipcode = myProfileVM.zipcode;
        //        editedAccount.personData.country = myProfileVM.country;

        //        editedAccount.email = myProfileVM.Email;
        //        editedAccount.nickname = myProfileVM.nickname;

        //        editedAccount.personData.isProfilePublic = myProfileVM.isProfilePublic;

        //        accountRepository.EditAccount(editedAccount);

        //        Session["CurrentUser"] = editedAccount;
        //        TempData["message"] = "Successfully edited profile: " + editedAccount.nickname;
        //        return RedirectToAction("MyProfile", "Account");
        //    }

        //    return View(myProfileVM);
        //}

        //public ActionResult ChangePassword()
        //{
        //    ChangePasswordVM changePasswordVM = new ChangePasswordVM();
        //    return View(changePasswordVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        //{
        //    Account account = (Account)Session["CurrentUser"];

        //    UserSecurityRepository userSecurityRepository = new UserSecurityRepository();

        //    UserSecurity oldPassword = userSecurityRepository.CreateUserSecurity("", changePasswordVM.oldPassword);

        //    UserSecurity newPassword = userSecurityRepository.CreateUserSecurity("", changePasswordVM.newPassword);

        //    UserSecurity repeatPassword = userSecurityRepository.CreateUserSecurity("", changePasswordVM.repeatPassword);

        //    bool isValid = true;
        //    if (oldPassword.password != account.userSecurity.password)
        //    {
        //        ModelState.AddModelError("oldPassword", "The enter password is different from the old password.");
        //        isValid = false;
        //    }
        //    if (ModelState.IsValid && isValid)
        //    {
        //        Account editedAccount = accountRepository.GetAccount(account.accountID);
        //        editedAccount.userSecurity.password = newPassword.password;

        //        accountRepository.EditAccount(editedAccount);
        //        Session["CurrentUser"] = editedAccount;
        //        TempData["message"] = "Successfully password was changed!";
        //        return RedirectToAction("MyProfile", "Account");
        //    }
        //    return View(changePasswordVM);
        //}
    }
}