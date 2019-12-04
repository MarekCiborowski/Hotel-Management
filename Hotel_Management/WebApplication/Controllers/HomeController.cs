using BusinessLogic.Services;
using DataAccessLayer;
using DomainObjects.Dto;
using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication.Filters;
using WebApplication.ViewModels.Home;
using static DomainObjects.Enums;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext databaseContext;
        private UserService userService;
        private RoomService roomService;
        private ConversationService conversationService;
        private ReservationService reservationService;

        public HomeController()
        {
            this.databaseContext = new DatabaseContext();
            this.userService = new UserService(this.databaseContext);
            this.roomService = new RoomService(this.databaseContext);
            this.conversationService = new ConversationService(this.databaseContext);
            this.reservationService = new ReservationService(this.databaseContext);
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
            var foundRooms = this.roomService.GetAvailableRooms(roomSearchVM.AccomodationDate, roomSearchVM.CheckOutDate, roomSearchVM.SelectedAmenityIds, roomSearchVM.NumberOfGuests, roomSearchVM.RoomSize);
            var foundRoomsVM = new List<RoomVM>();
            foreach(var foundRoom in foundRooms)
            {
                var amenities = this.roomService.GetAmenitiesOfRoom(foundRoom.RoomId);
                var amenitiesString = new StringBuilder();
                foreach(var amenity in amenities)
                {
                    amenitiesString.Append(amenity.AmenityName + ", ");
                }

                foundRoomsVM.Add(new RoomVM
                {
                    Amenities = amenitiesString.ToString(),
                    Cost = foundRoom.Cost,
                    MaxNumberOfGuests = foundRoom.MaxNumberOfGuests,
                    RoomNumber = foundRoom.RoomId,
                    RoomSize = foundRoom.RoomSize
                });
            }

            roomSearchVM.AmenitiesToSearch = roomService.GetAllAmenities().Select(a => new AmenitySearchVM
            {
                AmenityId = a.AmenityId,
                AmenityName = a.AmenityName
            }).ToList();
            roomSearchVM.FoundRooms = foundRoomsVM;

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

        public ActionResult MakeReservation(int roomId, DateTime accomodationDate, DateTime checkOutDate)
        {
            var room = this.roomService.GetRoom(roomId);
            var amenities = this.roomService.GetAmenitiesOfRoom(roomId);
            var amenitiesString = new StringBuilder();
            foreach (var amenity in amenities)
            {
                amenitiesString.Append(amenity.AmenityName + ", ");
            }

            var makeReservationVM = new MakeReservationVM
            {
                RoomId = room.RoomId,
                Amenities = amenitiesString.ToString(),
                RoomSize = room.RoomSize,
                Cost = room.Cost,
                MaxNumberOfGuests = room.MaxNumberOfGuests,
                AccomodationDate = accomodationDate,
                CheckOutDate = checkOutDate
            };

            return View(makeReservationVM);
        }

        public ActionResult SendReservation(MakeReservationVM makeReservationVM)
        {
            var user = (User)Session["CurrentUser"];
            this.reservationService.AddReservation(makeReservationVM.RoomId, user.Identity, makeReservationVM.AccomodationDate, makeReservationVM.CheckOutDate, ReservationStatusEnum.AwaitingConfirmation);
            TempData["message"] = "Successfully sent reservation";

            return Index();
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

        [GeneralAuthorizationFilter]
        public ActionResult MyProfile()
        {
            User user = (User)Session["CurrentUser"];
            ProfileVM myProfileVM = new ProfileVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                City = user.City,
                Email = user.Email,
                Zipcode = user.Zipcode
            };
            return View(myProfileVM);
        }

        [GeneralAuthorizationFilter]
        public ActionResult EditProfile()
        {
            User user = (User)Session["CurrentUser"];
            ProfileVM myProfileVM = new ProfileVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                City = user.City,
                Email = user.Email,
                Zipcode = user.Zipcode
            };
            return View(myProfileVM);
        }

        [HttpPost]
        [GeneralAuthorizationFilter]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ProfileVM myProfileVM)
        {
            User editedUser = (User)Session["CurrentUser"];
            bool isValid = true;
            if (editedUser.Email != myProfileVM.Email)
                if (!userService.IsEmailCorrect(myProfileVM.Email))
                {
                    ModelState.AddModelError("email", "Email is taken or not correct.");
                    isValid = false;
                }

            if (ModelState.IsValid && isValid)
            {

                editedUser.Address = myProfileVM.Address;
                editedUser.City = myProfileVM.City;
                editedUser.Zipcode = myProfileVM.Zipcode;
                editedUser.FirstName = myProfileVM.FirstName;
                editedUser.LastName = myProfileVM.LastName;
                editedUser.Email = myProfileVM.Email;

                userService.EditUser(editedUser);

                Session["CurrentUser"] = editedUser;
                TempData["message"] = "Successfully edited profile: " + editedUser.Login;
                return RedirectToAction("MyProfile");
            }

            return View(myProfileVM);
        }

        [GeneralAuthorizationFilter]
        public ActionResult ChangePassword()
        {
            ChangePasswordVM changePasswordVM = new ChangePasswordVM();
            return View(changePasswordVM);
        }

        [HttpPost]
        [GeneralAuthorizationFilter]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            User user = (User)Session["CurrentUser"];

            var oldPassword = userService.HashPassword(changePasswordVM.oldPassword);

            var newPassword = userService.HashPassword(changePasswordVM.newPassword);

            var repeatPassword = userService.HashPassword(changePasswordVM.repeatPassword);

            bool isValid = true;
            if (oldPassword != user.Password)
            {
                ModelState.AddModelError("oldPassword", "The enter password is different from the old password.");
                isValid = false;
            }
            if (ModelState.IsValid && isValid)
            {
                
                user.Password = newPassword;

                userService.EditUser(user);
                Session["CurrentUser"] = user;
                TempData["message"] = "Successfully password was changed!";
                return RedirectToAction("MyProfile");
            }
            return View(changePasswordVM);
        }
    }
}