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

        [HttpPost]
        public ActionResult Index(string HotelID, string Ratify)
        {
            HotelManag manag1 = new HotelManag();
            manag1.HotelID = int.Parse(HotelID);
            manag1.HotelRatify = bool.Parse(Ratify);
            if (HotelService.SetHotel(manag1))
            {
                //return Json ( new { Code = "200",success = "成功" },JsonRequestBehavior.AllowGet );
                return Content("<script>window.location.href = '/Hotel/Index '</script>");
            }
            else
            {
                return Json(new { Code = "404", success = "失败" },JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}