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
        public ActionResult Index(string HotelID, string Ratify, string hotelname)
        {
            if (hotelname != null && hotelname != "")
            {
              var hotelmes=HotelService.SelectHotel(hotelname);
                return View(hotelmes);
            }
            else
            {
            HotelManag manag1 = new HotelManag();
            manag1.HotelID = int.Parse(HotelID);
            manag1.HotelRatify = bool.Parse(Ratify);
            if (HotelService.SetHotel(manag1))
            {
                    //return Json ( new { Code = "200",success = "成功" },JsonRequestBehavior.AllowGet );
                    var hotel = HotelService.Hotel();
                    return View(hotel);
                   
            }

            else
            {
                return Json(new { Code = "404", success = "失败" },JsonRequestBehavior.AllowGet);
            }
            }
          
           
        }
        public ActionResult MainIndex(int hotelID)
        {
          
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {

                var hotelrmes = db.HotelManag.Where(s => s.HotelID == hotelID).FirstOrDefault();
                var hotelph = db.HotelPhoto.Where(s => s.HotelPhotoID == hotelrmes.HotelPhotoID).FirstOrDefault();
                var sf = hotelrmes.HotelFacility;
                var fct = sf.Split(',').Count();
                string bb = "";
                for (int i = 1; i < fct; i++)
                {
                    if (i < fct - 1)
                        bb = bb + sf.Split(',')[i] + "+";
                    else
                    {
                        bb = bb + sf.Split(',')[i];
                    }
                }
                hotelrmes.HotelFacility = bb;

                ViewBag.HotelPhotoname = new HotelPhoto
                {
                    Hotelphoto1 = hotelph.Hotelphoto1,
                    Hotelphoto2 = hotelph.Hotelphoto2,
                    Hotelphoto3 = hotelph.Hotelphoto3,
                    Hotelphoto4 = hotelph.Hotelphoto4,
                    Hotelphoto5 = hotelph.Hotelphoto5,
                    Hotelphoto6 = hotelph.Hotelphoto6,
                    Hotelphoto7 = hotelph.Hotelphoto7,

                };


                return View(hotelrmes);
            }



        }
        //public ActionResult Cha(int hotelID)
        //{

        //}
        
    }
}