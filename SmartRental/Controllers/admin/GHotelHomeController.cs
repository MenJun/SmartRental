using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartRental.Controllers.admin
{
    public class GHotelHomeController : Controller
    {
        // GET: GHotelHome
        public ActionResult Main()
        {
            return View();
        }
        public ActionResult Main2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Main(HotelManag manag, string city, string[] HotelFacility)
        {
            string fi = "";

            foreach (var item in HotelFacility)
            {

                //可以自己写Insert语句(添加数据)

                fi = fi + "," + item.ToString();//这样写方便调试看
            }
            manag.HotelFacility = fi;
            manag.HotelCity = city;
            manag.Hoteltration_time = DateTime.Now.ToLocalTime();
          
            manag.HotelGrade = 10;
            string[] bs = common.Upload.ImgName();

            if (BLL.ServiceAdmin.GHotelManageService.AddHotelManager(manag, bs))
            {
                return Content("<script>alert('提交成功')</script>");
            }
            return Content("<script>alert('提交失败')</script>");



        }
    }
}