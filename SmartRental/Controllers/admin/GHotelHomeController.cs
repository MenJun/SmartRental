using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartRental.Models;
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






        /// <summary>
        /// 酒店房间添加界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            ViewBag.RoomType = new SelectList(BLL.ServiceAdmin.GHotelManageService.GetAll2(), "RoomTypeID", "RoomType1");
            ViewBag.Mattres = new SelectList(BLL.ServiceAdmin.GHotelManageService.GetAll1(), "MattresID", "MattresType");
            ViewBag.HotelName = new SelectList(BLL.ServiceAdmin.GHotelManageService.GetAll3(), "HotelID", "HotelName");
            return View();
        }

        [HttpPost]
        public ActionResult Index(RoomMessage roomMessage, string[] RoomFacility)
        {
            string fi = "";

            foreach (var item in RoomFacility)
            {
                //可以自己写Insert语句(添加数据)

                fi = fi + "," + item.ToString();//这样写方便调时看
            }
            roomMessage.RoomFacility = fi;
            string[] bs = common.RoomUpload.ImgName();
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities()) { 
                RoomPhoto roomph = new RoomPhoto();
            for (int i = 0; i <bs.Length; i++)
            {

                if (i == 0)
                        roomph.PhotoName1 = bs[i].ToString();
                if (i == 1)
                        roomph.PhotoName2 = bs[i].ToString();
                if (i == 2)
                        roomph.PhotoName3 = bs[i].ToString();
                if (i == 3)
                        roomph.PhotoName4 = bs[i].ToString();
                if (i == 4)
                        roomph.PhotoName5 = bs[i].ToString();
                if (i == 5)
                        roomph.PhotoName6 = bs[i].ToString();
                if (i == 6)
                        roomph.PhotoName7 = bs[i].ToString();

            }
            var bb = db.RoomPhoto.Add(roomph);
            db.SaveChanges();
            int photoID = bb.RoomPhotoID;
                roomMessage.RoomPhotoID = photoID;
            db.RoomMessage.Add(roomMessage);
            db.SaveChanges();
                //if (db.SaveChanges()>0)
                //return Content("<script>alert('添加成功');location.href='/GHotelHome/Index'</script>");
            }
           
            return Content("<script>alert('添加成功');</script>");
        }
    }
}