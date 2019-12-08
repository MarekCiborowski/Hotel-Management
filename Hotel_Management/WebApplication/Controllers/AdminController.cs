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
using WebApplication.ViewModels.Admin;
using static DomainObjects.Enums;

namespace WebApplication.Controllers
{
    [AdminAuthorizationFilter]
    public class AdminController : Controller
    {
        private readonly DatabaseContext databaseContext;
        private UserService userService;
        private RoomService roomService;
        private ConversationService conversationService;
        private ReservationService reservationService;

        public AdminController()
        {
            this.databaseContext = new DatabaseContext();
            this.userService = new UserService(this.databaseContext);
            this.roomService = new RoomService(this.databaseContext);
            this.conversationService = new ConversationService(this.databaseContext);
            this.reservationService = new ReservationService(this.databaseContext);
        }

        // GET: Admin
        public ActionResult Reservations()
        {
            this.reservationService.UpdateReservations();
            var reservations = this.reservationService.GetReservationsDto();

            return View(reservations);
        }

        public ActionResult ConfirmReservation(int reservationId)
        {
            this.reservationService.ChangeReservationStatus(reservationId, ReservationStatusEnum.Confirmed);
            TempData["message"] = "Reservation confirmed";
            return RedirectToAction("Reservations");
        }

        public ActionResult CancelReservation(int reservationId)
        {
            this.reservationService.ChangeReservationStatus(reservationId, ReservationStatusEnum.Canceled);
            TempData["message"] = "Reservation canceled";
            return RedirectToAction("Reservations");
        }

        public ActionResult CloseReservation(int reservationId)
        {
            this.reservationService.ChangeReservationStatus(reservationId, ReservationStatusEnum.Closed);
            TempData["message"] = "Reservation closed";
            return RedirectToAction("Reservations");
        }

        public ActionResult Rooms()
        {
            this.reservationService.UpdateReservations();
            var rooms = this.roomService.GetAllRooms();
            var allRoomsVM = new List<RoomListItem>();
            foreach(var room in rooms)
            {
                allRoomsVM.Add(new RoomListItem
                {
                    RoomId = room.RoomId,
                    RoomSize = room.RoomSize,
                    Cost = room.Cost,
                    MaxNumberOfGuests = room.MaxNumberOfGuests,
                    Amenities = this.roomService.GetRoomAmenitiesString(room.RoomId)
                });
            }

            return View(allRoomsVM);
        }

        public ActionResult EditRoom(int roomId)
        {
            var roomToEdit = this.roomService.GetRoom(roomId);
            var amenities = this.roomService.GetAmenitiesOfRoom(roomId);
            var editRoomVM = new RoomVM
            {
                Cost = roomToEdit.Cost,
                MaxNumberOfGuests = roomToEdit.MaxNumberOfGuests,
                RoomNumber = roomToEdit.RoomId,
                RoomSize = roomToEdit.RoomSize,
                SelectedAmenityIds = amenities.Select(a => a.AmenityId.ToString()).ToArray(),
                AllAmenities = roomService.GetAllAmenities().Select(a => new AmenityVM
                {
                    AmenityId = a.AmenityId,
                    AmenityName = a.AmenityName
                }).ToList(),
            };

            return View(editRoomVM);
        }

        [HttpPost]
        public ActionResult EditRoom(RoomVM editRoomVM)
        {
            if (ModelState.IsValid)
            {
                var amenityIds = new List<int>();
                if(editRoomVM.SelectedAmenityIds != null)
                {
                    foreach (var amenityId in editRoomVM.SelectedAmenityIds)
                    {
                        amenityIds.Add(int.Parse(amenityId));
                    }
                }

                this.roomService.EditRoom(editRoomVM.RoomNumber, editRoomVM.Cost, editRoomVM.MaxNumberOfGuests, editRoomVM.RoomSize, amenityIds);
                TempData["message"] = "Room edited";
                return RedirectToAction("Rooms");
            }

            editRoomVM.AllAmenities = roomService.GetAllAmenities().Select(a => new AmenityVM
            {
                AmenityId = a.AmenityId,
                AmenityName = a.AmenityName
            }).ToList();

            return View(editRoomVM);
        }

        public ActionResult DeleteRoom(int roomId)
        {
            TempData["message"] = "Removed room : " + roomId;
            this.roomService.RemoveRoom(roomId);

            return RedirectToAction("Rooms");
        }

        public ActionResult AddRoom()
        {
            var addRoomVM = new RoomVM
            {
                AllAmenities = roomService.GetAllAmenities().Select(a => new AmenityVM
                {
                    AmenityId = a.AmenityId,
                    AmenityName = a.AmenityName
                }).ToList(),
            };

            return View(addRoomVM);
        }

        [HttpPost]
        public ActionResult AddRoom(RoomVM addRoomVM)
        {
            if (ModelState.IsValid)
            {
                var amenityIds = new List<int>();
                if(addRoomVM.SelectedAmenityIds != null)
                {
                    foreach (var amenityId in addRoomVM.SelectedAmenityIds)
                    {
                        amenityIds.Add(int.Parse(amenityId));
                    }
                }
               

                this.roomService.AddRoom(addRoomVM.Cost, addRoomVM.MaxNumberOfGuests, addRoomVM.RoomSize, amenityIds);
                TempData["message"] = "Room added";
                return RedirectToAction("Rooms");
            }

            addRoomVM.AllAmenities = roomService.GetAllAmenities().Select(a => new AmenityVM
            {
                AmenityId = a.AmenityId,
                AmenityName = a.AmenityName
            }).ToList();

            return View(addRoomVM);
        }

        public ActionResult Amenities()
        {
            var addAmenityVM = new AmenitiesVM
            {
                AvailableAmenities = this.roomService.GetAllAmenities().Select(a => new AmenityVM
                {
                    AmenityId = a.AmenityId,
                    AmenityName = a.AmenityName
                }).ToList(),
            };

            return View(addAmenityVM);
        }

        [HttpPost]
        public ActionResult Amenities(AmenitiesVM amenitiesVM)
        {
            if (ModelState.IsValid)
            {
                this.roomService.AddAmenity(amenitiesVM.NewAmenity);
                TempData["message"] = "Added amenity : " + amenitiesVM.NewAmenity;
            }

            amenitiesVM.AvailableAmenities = this.roomService.GetAllAmenities().Select(a => new AmenityVM
            {
                AmenityId = a.AmenityId,
                AmenityName = a.AmenityName
            }).ToList();

            return View(amenitiesVM);
        }

        public ActionResult DeleteAmenity(int amenityToDeleteId)
        {
            var amenity = this.roomService.GetAmenityById(amenityToDeleteId);
            TempData["message"] = "Removed amenity : " + amenity.AmenityName;
            this.roomService.RemoveAmenity(amenityToDeleteId);

            return RedirectToAction("Amenities");
        }

        public ActionResult Users()
        {

            var userSearchVM = new UserSearchVM
            {
                UserList = this.userService.GetAllConfirmedUsersOfType(RolesEnum.RegularUser).Select(u => new UserListItemVM(u)).ToList()
            };

            return View(userSearchVM);
        }



        [HttpPost]
        public ActionResult Users(UserSearchVM userSearchVM)
        {
            if (userSearchVM.ShowAdmin)
            {
                userSearchVM.UserList = this.userService.GetAllConfirmedUsersOfType(RolesEnum.Admin).Select(u => new UserListItemVM(u)).ToList();
            }
            else if (userSearchVM.ShowUnconfirmed)
            {
                userSearchVM.UserList = this.userService.GetUnconfirmedUsers().Select(u => new UserListItemVM(u)).ToList();
            }
            else
            {
                userSearchVM.UserList = this.userService.GetAllConfirmedUsersOfType(RolesEnum.RegularUser).Select(u => new UserListItemVM(u)).ToList();
            }

            return View(userSearchVM);
        }

        public ActionResult UserProfile(int userId)
        {
            var user = this.userService.GetUserById(userId);
            var profileVM = new ProfileVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                Address = user.Address,
                Email = user.Email,
                Zipcode = user.Zipcode
            };

            return View(profileVM);
        }

        public ActionResult ConfirmUser(int userId)
        {
            this.userService.ConfirmUser(userId);
            var user = this.userService.GetUserById(userId);
            TempData["message"] = "Confirmed user " + user.FirstName + " " + user.LastName;

            return RedirectToAction("Users");
        }

        public ActionResult ConversationList()
        {
            var conversationsDto = this.conversationService.GetConversationsIncludingSenderNameInTitle();

            return View(conversationsDto);
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
                TempData["message"] = "Message sent successfully";

                return RedirectToAction("Conversation", new { conversationId = conversationDto.ConversationId });
            }

            return Conversation(conversationDto);
        }

    }
}