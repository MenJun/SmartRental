using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartRental.BLL.ServiceAdmin;
using SmartRental.Models;

namespace SmartRental.Controllers.admin
{
    public class HotelController : Controller
    {
        // GET: Hotel
        public ActionResult Index()
        {

            var hotel = HotelService.Hotel();
            return View(hotel);
        }
    }
}