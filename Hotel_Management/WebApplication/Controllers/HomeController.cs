using BusinessLogic.Services;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.ViewModels.Home;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext databaseContext;
        private UserService userService;
        private RoomService roomService;

        public HomeController()
        {
            this.databaseContext = new DatabaseContext();
            this.userService = new UserService(this.databaseContext);
            this.roomService = new RoomService(this.databaseContext);
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
            var x = 1;
            return View(roomSearchVM);
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