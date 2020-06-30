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
        /// <summary>
        /// 酒店信息添加
        /// </summary>
        /// <returns></returns>
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
        public ActionResult Main(HotelManag manag, string city,string province, string[] HotelFacility)
        {
            if (province.Contains("市"))
            {
                manag.HotelCity = province;
            }
            else
            {
                manag.HotelCity = city;
            }
          
            string fi = "";

            foreach (var item in HotelFacility)
            {

                //可以自己写Insert语句(添加数据)

                fi = fi + "," + item.ToString();//这样写方便调试看
            }
            manag.HotelFacility = fi;
        
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
            string[] bs = common.Roomphoto.ImgName();
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


        public ActionResult RoomIndex()
        {

        
            return View();
        }

        public JsonResult Roommessagelist()
        {
           var  HotelNameID = 5;//以酒店为10做测试

            SmartRentalSystemEntities db = new SmartRentalSystemEntities();
            int pageSize = int.Parse(Request["limit"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            List<RoomMessage> roommessagelist = db.RoomMessage.Where(f=>f.HotelID == HotelNameID).OrderBy(s => s.RoomID).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            //总共多少数据
            var allCount = db.RoomMessage.Where(f => f.HotelID == HotelNameID).Count();
            //把totle和rows:[{},{}]一起返回
            //先建立一个匿名类
            var dataJson = new {code=0, msg="",count = allCount, data = roommessagelist };
            var json = Json(dataJson, JsonRequestBehavior.AllowGet);
            return json;
        }
        [HttpPost]
        public ActionResult DeleteRoom(int id)
        {
            SmartRentalSystemEntities dbContext = new SmartRentalSystemEntities();
            var s = dbContext.RoomMessage.Find(id);
            dbContext.RoomMessage.Remove(s);
            dbContext.SaveChanges();
            return Content("ok:删除成功");
        }

    }
}