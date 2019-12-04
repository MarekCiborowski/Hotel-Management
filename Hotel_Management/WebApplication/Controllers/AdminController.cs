using BusinessLogic.Services;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Filters;
using WebApplication.ViewModels.Admin;

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
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Rooms()
        {
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
            return View();
        }

        public ActionResult EditRoom(RoomVM roomVM)
        {
            return View();
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
                foreach (var amenityId in addRoomVM.SelectedAmenityIds)
                {
                    amenityIds.Add(int.Parse(amenityId));
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

    }
}