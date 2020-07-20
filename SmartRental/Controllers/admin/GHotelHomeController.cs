//using Microsoft.AspNetCore.Mvc;
using Aop.Api.Domain;
using SmartRental.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
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
        //测试视图
        public ActionResult Main2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Main(HotelManag manag, string city, string province, string[] HotelFacility,string userphone)
        {
            
            SmartRentalSystemEntities db = new SmartRentalSystemEntities();
            var usrmph=db.UserMessage.Where(u => u.UserPhone == userphone).FirstOrDefault();
            if (usrmph != null) {
                UserMessage usms = new UserMessage();
                usms.UserID = usrmph.UserID;
            if (city.Substring(0, 1)=="市"|| city.Substring(0, 1) == "县")
            {
                manag.HotelCity = province;
            }
            else
            {
                manag.HotelCity = city;
            }
            var b = province.Substring(0, 1);
            string fi = "";

            foreach (var item in HotelFacility)
            {

                //可以自己写Insert语句(添加数据)

                fi = fi + "+" + item.ToString();//这样写方便调试看
            }
            manag.HotelFacility = fi;

            manag.Hoteltration_time = DateTime.Now.ToLocalTime();

            manag.HotelGrade = 10;
            manag.HotelDiscount = 99999;
            System.Web.HttpFileCollection file = System.Web.HttpContext.Current.Request.Files;
            var photocount = file.Count;
            string[] imgname = new string[photocount];
            if (photocount > 0)
            {



                for (int i = 0; i < photocount; i++)
                {
                    if (file[i].FileName == "" || file[i].FileName == null)
                    {
                        imgname[i] = "";
                    }
                    else
                    {
                        string guid = Guid.NewGuid().ToString();
                        string path = "~/images/HotelPhoto/" + guid + file[i].FileName;
                        file[i].SaveAs(Server.MapPath(path));//必须得是相对路径
                        imgname[i] = "/images/HotelPhoto/" + guid + file[i].FileName;
                    }
                }
            }

            if (BLL.ServiceAdmin.GHotelManageService.AddHotelManager(manag, imgname,usms))
            {
                return Content("<script>alert('提交成功');location.href = '/Login/Index';</script>");
            }
            return Content("<script>alert('提交失败');location.href = '/Login/Index';</script>");
            }
            else
            {
                return Content("<script>alert('提交账号不存在');location.href = '/GHotelHome/Main';</script>");
            }


        }
        /// <summary>
        /// 酒店信息显示
        /// </summary>
        /// <returns></returns>
        public ActionResult MainIndex()
        {
            var hotelID = int.Parse( Session["HotelID"].ToString());//Session["HotelID"] 酒店ID
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
               
                var hotelrmes = db.HotelManag.Where(s => s.HotelID == hotelID).FirstOrDefault();
                var hotelph= db.HotelPhoto.Where(s => s.HotelPhotoID == hotelrmes.HotelPhotoID).FirstOrDefault();
                var sf = hotelrmes.HotelFacility;
               var fct=sf.Split('+').Count();
                string bb = "";
                for (int i = 1; i < fct; i++)
                {
                    if(i<fct-1)
                    bb =bb+sf.Split('+')[i]+"+";
                    else
                    {
                        bb =bb+sf.Split('+')[i];
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
        /// <summary>
        /// 酒店信息修改
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult MainEdit(int id)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {

                var hotelrmes = db.HotelManag.Where(s => s.HotelID == id).FirstOrDefault();
                var hotelph = db.HotelPhoto.Where(s => s.HotelPhotoID == hotelrmes.HotelPhotoID).FirstOrDefault();
                var sf = hotelrmes.HotelFacility;
                var fct = sf.Split('+').Count();
                string bb = "";
                for (int i = 1; i < fct; i++)
                {
                    if (i < fct - 1)
                        bb = bb + sf.Split('+')[i] + "+";
                    else
                    {
                        bb = bb + sf.Split('+')[i];
                    }
                }
                hotelrmes.HotelFacility = bb;

                ViewBag.HotelPhotonameEdit = new HotelPhoto
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
        [HttpPost]
        public ActionResult MainEdit(HotelManag manag, string city,string province, string[] HotelFacility)
        {


            if (city.Substring(0, 1) == "市" || city.Substring(0, 1) == "县")
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

                fi = fi + "+" + item.ToString();//这样写方便调时看
            }
            manag.HotelFacility = fi;
            System.Web.HttpFileCollection file = System.Web.HttpContext.Current.Request.Files;
            var photocount = file.Count;
            string[] imgname = new string[photocount];
            if (photocount > 0)
            {
                


                for (int i = 0; i < photocount; i++)
                {
                    if (file[i].FileName == "" || file[i].FileName == null)
                    {
                        imgname[i] = "";
                    }
                    else
                    {
                        string guid = Guid.NewGuid().ToString();
                        string path = "~/images/HotelPhoto/" + guid + file[i].FileName;
                        file[i].SaveAs(Server.MapPath(path));//必须得是相对路径
                        imgname[i] = "/images/HotelPhoto/" + guid + file[i].FileName;
                    }
                }
            }
           
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                if (imgname[0] != "" && imgname[0] != null)
                {
                    HotelPhoto roomph = new HotelPhoto();
                    for (int i = 0; i < photocount; i++)
                    {

                        if (i == 0)
                            roomph.Hotelphoto1 = imgname[i].ToString();
                        if (i == 1)
                            roomph.Hotelphoto2 = imgname[i].ToString();
                        if (i == 2)
                            roomph.Hotelphoto3= imgname[i].ToString();
                        if (i == 3)
                            roomph.Hotelphoto4 = imgname[i].ToString();
                        if (i == 4)
                            roomph.Hotelphoto5 = imgname[i].ToString();
                        if (i == 5)
                            roomph.Hotelphoto6 = imgname[i].ToString();
                        if (i == 6)
                            roomph.Hotelphoto7 = imgname[i].ToString();

                    }
                    for (int s = photocount; s < 7; s++)
                    {
                        if (s == 0)
                            roomph.Hotelphoto1 = null;
                        if (s == 1)
                            roomph.Hotelphoto2 = null;
                        if (s == 2)
                            roomph.Hotelphoto3 = null;
                        if (s == 3)
                            roomph.Hotelphoto4 = null;
                        if (s == 4)
                            roomph.Hotelphoto5 = null;
                        if (s == 5)
                            roomph.Hotelphoto6 = null;
                        if (s == 6)
                            roomph.Hotelphoto7 = null;
                    }
                    roomph.HotelPhotoID = (int)manag.HotelPhotoID;
                    db.Entry(roomph).State = EntityState.Modified;
                    db.SaveChanges();
                }
               
                var hotmana = db.HotelManag.Find(manag.HotelID);
                hotmana.HotelName = manag.HotelName;
                hotmana.HotelCity = manag.HotelCity;
                hotmana.AddressDetails = manag.AddressDetails;
                hotmana.HotelType = manag.HotelType;
                hotmana.HotelIntro = manag.HotelIntro;
                hotmana.HotelBoss = manag.HotelBoss;
                hotmana.HotelRule = manag.HotelRule;
                hotmana.HotelFacility = manag.HotelFacility;
                hotmana.HotelPhone = manag.HotelPhone;
                hotmana.HotelOpentime = manag.HotelOpentime;
                //db.Entry(manag).State = EntityState.Modified;
                db.SaveChanges();
             }


                return Content("<script>alert('修改成功');location.href = '/GHotelHome/MainIndex';</script>");
            
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
        public System.Web.Mvc.ActionResult Index(RoomMessage roomMessage, string[] RoomFacility)
        {
            string fi = "";

            foreach (var item in RoomFacility)
            {
                //可以自己写Insert语句(添加数据)

                fi = fi + "+" + item.ToString();//这样写方便调时看
            }
            roomMessage.RoomFacility = fi;

            int hotelID = int.Parse(Session["HotelID"].ToString());

            System.Web.HttpFileCollection file = System.Web.HttpContext.Current.Request.Files;
            var photocount = file.Count;
            string[] imgname = new string[photocount];
            if (photocount > 0&&file[0].FileName!="")
            {
                for (int i = 0; i < photocount; i++)
                {
                    if (file[i].FileName == "" || file[i].FileName == null)
                    {
                        imgname[i] = "";
                    }
                    else
                    {
                        string guid = Guid.NewGuid().ToString();
                        string path = "~/images/Roomphoto/" + guid + file[i].FileName;
                        file[i].SaveAs(Server.MapPath(path));//必须得是相对路径
                        imgname[i] = "/images/Roomphoto/" + guid + file[i].FileName;
                    }
                }
           
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                RoomPhoto roomph = new RoomPhoto();
                for (int i = 0; i < photocount; i++)
                {

                    if (i == 0)
                        roomph.PhotoName1 = imgname[i].ToString();
                    if (i == 1)
                        roomph.PhotoName2 = imgname[i].ToString();
                    if (i == 2)
                        roomph.PhotoName3 = imgname[i].ToString();
                    if (i == 3)
                        roomph.PhotoName4 = imgname[i].ToString();
                    if (i == 4)
                        roomph.PhotoName5 = imgname[i].ToString();
                    if (i == 5)
                        roomph.PhotoName6 = imgname[i].ToString();
                    if (i == 6)
                        roomph.PhotoName7 = imgname[i].ToString();

                }
                var bb = db.RoomPhoto.Add(roomph);
                db.SaveChanges();
                int photoID = bb.RoomPhotoID;
                roomMessage.RoomPhotoID = photoID;
                roomMessage.RoomCount = roomMessage.RoomRemain;
                roomMessage.HotelID = hotelID;
                db.RoomMessage.Add(roomMessage);
                var htole = db.HotelManag.Find(hotelID);
                if (htole.HotelDiscount > roomMessage.RoomPrice)
                {
                    htole.HotelDiscount = roomMessage.RoomPrice;
                }
                db.SaveChanges();
                //if (db.SaveChanges()>0)
                //return Content("<script>alert('添加成功');location.href='/GHotelHome/Index'</script>");
            }

            return Content("<script>alert('添加成功');location.href = '/GHotelHome/Index';</script>"); 
            }
            return Content("<script>alert('添加失败');location.href = '/GHotelHome/Index';</script>");
        }

        /// <summary>
        /// 酒店房间信息显示
        /// </summary>
        /// <returns></returns>
        public ActionResult RoomIndex()
        {


            return View();
        }
        /// <summary>
        /// 提供酒店房间信息
        /// </summary>
        /// <returns></returns>
        public JsonResult Roommessagelist()
        {
            var HotelNameID =  int.Parse(Session["HotelID"].ToString());//以酒店为10做测试   //Session["HotelID"] 酒店ID

            SmartRentalSystemEntities db = new SmartRentalSystemEntities();
            
            int pageSize = int.Parse(Request["limit"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            var roommessagelist = db.RoomMessage.Include(n=>n.RoomType).Include(s=>s.Mattres).Where(f => f.HotelID == HotelNameID&&f.Roomstate!=false).OrderBy(s => s.RoomID).Skip(pageSize * (pageIndex - 1)).Take(pageSize).Select(n => new { Mattres = n.Mattres.MattresType, RoomType = n.RoomType.RoomType1, RoomName=n.RoomName, RoomFacility = n.RoomFacility, RoomLayout = n.RoomLayout, RoomRemain = n.RoomRemain, Boolbreakfast = n.Boolbreakfast==true?"是":"否", PrimeCost = n.PrimeCost, RoomPrice = n.RoomPrice, RoomID = n.RoomID });
            //总共多少数据
            var allCount = db.RoomMessage.Where(f => f.HotelID == HotelNameID).Count();
            //把totle和rows:[{},{}]一起返回
            //先建立一个匿名类
           

            var dataJson = new { code = 0, msg = "", count = allCount, data = roommessagelist };
            var json = Json(dataJson, JsonRequestBehavior.AllowGet);
            return json;
        }
        /// <summary>
        /// 删除房间信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteRoom(int id)
        {
            SmartRentalSystemEntities dbContext = new SmartRentalSystemEntities();
            var s = dbContext.RoomMessage.Find(id);
            if (s.RoomCount == s.RoomRemain) {
                var htole = dbContext.HotelManag.Find(s.HotelID);
                if (htole.HotelDiscount>=s.RoomPrice)
                {
                    var hoteldipri = dbContext.RoomMessage.Where(d => d.HotelID == s.HotelID && d.RoomPrice > s.RoomPrice).OrderBy(r => r.RoomPrice).FirstOrDefault();
                    htole.HotelDiscount = hoteldipri.RoomPrice;
                }
                s.Roomstate = false;
            dbContext.SaveChanges(); 
                return Content("ok:删除成功");
            }else
            {
                return Content("no:删除失败");
            }
           
        }
        /// <summary>
        /// 修改酒店房间信息
        /// </summary>
        /// <returns></returns>
        /// 
       
        public ActionResult EditRoom(int id )
        {

            ViewBag.RoomType = new SelectList(BLL.ServiceAdmin.GHotelManageService.GetAll2(), "RoomTypeID", "RoomType1");
            ViewBag.Mattres = new SelectList(BLL.ServiceAdmin.GHotelManageService.GetAll1(), "MattresID", "MattresType");
            ViewBag.HotelName = new SelectList(BLL.ServiceAdmin.GHotelManageService.GetAll3(), "HotelID", "HotelName");
            SmartRentalSystemEntities dbContext = new SmartRentalSystemEntities();
            var s = dbContext.RoomMessage.Where(h => h.RoomID == id).FirstOrDefault();
            var photoid = s.RoomPhotoID;

            var sv = dbContext.RoomPhoto.Where(h => h.RoomPhotoID == photoid).FirstOrDefault();
            ViewBag.Photoname = new RoomPhoto
            {
                PhotoName1 = sv.PhotoName1,
                PhotoName2 = sv.PhotoName2,
                PhotoName3 = sv.PhotoName3,
                PhotoName4 = sv.PhotoName4,
                PhotoName5 = sv.PhotoName5,
                PhotoName6 = sv.PhotoName6,
                PhotoName7 = sv.PhotoName7,

            };
            return View(s);
    }
    [HttpPost]
    public ActionResult EditRoom(RoomMessage roomMessage, string[] RoomFacility)
    {
            
            string fi = "";

        foreach (var item in RoomFacility)
        {
            //可以自己写Insert语句(添加数据)

            fi = fi + "+" + item.ToString();//这样写方便调时看
        }
        roomMessage.RoomFacility = fi;

            System.Web.HttpFileCollection file = System.Web.HttpContext.Current.Request.Files;
            var photocount = file.Count;
            string[] imgname = new string[photocount];
            if (photocount > 0 && file[0].FileName != "")
            {
                for (int i = 0; i < photocount; i++)
                {
                    if (file[i].FileName == "" || file[i].FileName == null)
                    {
                        imgname[i] = "";
                    }
                    else
                    {
                        string guid = Guid.NewGuid().ToString();
                        string path = "~/images/Roomphoto/" + guid + file[i].FileName;
                        file[i].SaveAs(Server.MapPath(path));//必须得是相对路径
                        imgname[i] = "/images/Roomphoto/" + guid + file[i].FileName;
                    }
                }
            }
                RoomPhoto roomph = new RoomPhoto();
        using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
        {      var hotmana = db.RoomMessage.Find(roomMessage.RoomID);
                if (roomMessage.RoomRemain >= (hotmana.RoomRemain - hotmana.RoomCount))
                {
                  
               

                if (imgname[0] != "" && imgname[0]!= null)
            {
                for (int i = 0; i < photocount; i++)
                {

                    if (i == 0)
                        roomph.PhotoName1 = imgname[i].ToString();
                    if (i == 1)
                        roomph.PhotoName2 = imgname[i].ToString();
                    if (i == 2)
                        roomph.PhotoName3 = imgname[i].ToString();
                    if (i == 3)
                        roomph.PhotoName4 = imgname[i].ToString();
                    if (i == 4)
                        roomph.PhotoName5 = imgname[i].ToString();
                    if (i == 5)
                        roomph.PhotoName6 = imgname[i].ToString();
                    if (i == 6)
                        roomph.PhotoName7 = imgname[i].ToString();

                }

                for (int s = photocount; s < 7; s++)
                {
                    if (s == 0)
                        roomph.PhotoName1 = null;
                    if (s == 1)
                        roomph.PhotoName2 = null;
                    if (s == 2)
                        roomph.PhotoName3 = null;
                    if (s == 3)
                        roomph.PhotoName4 = null;
                    if (s == 4)
                        roomph.PhotoName5 = null;
                    if (s == 5)
                        roomph.PhotoName6 = null;
                    if (s == 6)
                        roomph.PhotoName7 = null;
                }
                roomph.RoomPhotoID = (int)roomMessage.RoomPhotoID;
                db.Entry(roomph).State = EntityState.Modified;
                db.SaveChanges();
            }
                
                hotmana.MattresID = roomMessage.MattresID;
                hotmana.RoomTypeID = roomMessage.RoomTypeID;
                hotmana.RoomName = roomMessage.RoomName;
                hotmana.RoomLayout = roomMessage.RoomLayout;
                hotmana.RoomFacility = roomMessage.RoomFacility;
                hotmana.RoomCount = roomMessage.RoomRemain- (hotmana.RoomRemain - hotmana.RoomCount);
                hotmana.RoomRemain = roomMessage.RoomRemain;
                hotmana.PrimeCost = roomMessage.PrimeCost;
                hotmana.RoomPrice = roomMessage.RoomPrice;
                    var htole = db.HotelManag.Find(hotmana.HotelID);
                    if (htole.HotelDiscount > hotmana.RoomPrice)
                    {
                        htole.HotelDiscount = hotmana.RoomPrice;
                    }
                hotmana.Boolbreakfast = roomMessage.Boolbreakfast;
            
                //db.Entry(manag).State = EntityState.Modified;
                db.SaveChanges(); 
                    return Content("<script>alert('修改成功');location.href = '/GHotelHome/RoomIndex';</script>");
                }
                else {
                  return  Content("<script>alert('修改失败，房间数量小于该房间已出售数量');location.href = '/GHotelHome/EditRoom?id="+ roomMessage.RoomID+"';</script>");
                }

              
        }
    }



        public ActionResult HotelOrder(int pageindex=1,int pagesize=8)
        {
            var HotelIDss = int.Parse(Session["HotelID"].ToString());/*Session["HotelID"];*///获取酒店HotelID
            int pagecount = 1;
            var student = BLL.ServiceAdmin.GHotelManageService.GetStudentByPaging(pageindex, pagesize, out pagecount,HotelIDss);
            if ((Session["order"] != null && Session["order"].ToString() != "" && Session["se"] != "所有订单" && Session["se"] != null))
            {
                Session["order"] = 45;
                student = BLL.ServiceAdmin.GHotelManageService.GetStudentByPaging1(pageindex, pagesize, out pagecount, Session["se"].ToString(), Session["ts"].ToString(),HotelIDss);
            }
            else
            {

                Session["order"] = null;
            }
            //int countss = 5;          
            ViewBag.pageindex = pageindex;
            ViewBag.pagecount = pagecount;
            ViewBag.pagesize = pagesize;
            //if (pagesize % 5 == 0)
            //{
            //    ViewBag.countss = pagesize;
            //}
            //else
            //{
            //    ViewBag.countss = countss;
            //}
            //db.Order.Include(t => t.HotelManag).Include(p => p.RoomMessage).ToList()
            return View(student);
          
        }
        [HttpPost]
        public ActionResult HotelOrder( string OrderState, string ab, string text1, int pageindex = 1, int pagesize = 8)
        {
            Session["order"] =45;
            var HotelIDss =  int.Parse(Session["HotelID"].ToString());/*Session["HotelID"];*///获取酒店HotelID
            SmartRentalSystemEntities db = new SmartRentalSystemEntities();
            var sss = ab; var bbb = text1;
            if (ab != null && ab != "" )
            {
                Session["se"] = ab; Session["ts"] = text1;
            }
            if (sss == null && bbb == null)
            {
                var student = BLL.ServiceAdmin.GHotelManageService.Selectorderdate(pageindex, pagesize, out int pagecount, OrderState, HotelIDss);
                //Session["order"] = 1;
                ViewBag.pageindex = pageindex;
                ViewBag.pagecount = pagecount;
                ViewBag.pagesize = pagesize;
                //if (pagesize % 5 == 0)
                //{
                //    ViewBag.countss = pagesize;
                //}
                //else
                //{
                //    ViewBag.countss = countss;
                //}
                return View(student);
            }
            else if (sss == "所有订单")
            {
                return RedirectToAction("HotelOrder");
            }
            else
            {
                //int countss = 5;
                var student = BLL.ServiceAdmin.GHotelManageService.GetStudentByPaging1(pageindex, pagesize, out int pagecount, sss, bbb,HotelIDss);
                //Session["order"] = 1;
                ViewBag.pageindex = pageindex;
                ViewBag.pagecount = pagecount;
                ViewBag.pagesize = pagesize;
                //if (pagesize % 5 == 0)
                //{
                //    ViewBag.countss = pagesize;
                //}
                //else
                //{
                //    ViewBag.countss = countss;
                //}
                return View(student);

            }
       
        }

        public ActionResult HotelOrderdtld(int id)
        {
            SmartRentalSystemEntities db = new SmartRentalSystemEntities();
            var orderdtld = db.view_OrderHotelPhotos.Find(id);
            return View(orderdtld);
        }
        [HttpPost]
        public ActionResult HotelOrderdtld(int id,string OrderState)
        {
            SmartRentalSystemEntities db = new SmartRentalSystemEntities();
            var order = db.Order.Include("RoomMessage").Where(t => t.OrderID == id).FirstOrDefault();
            order.OrderState = OrderState;
            var or = db.Order.Find(id); var roms = db.RoomMessage.Find(order.RoomID);
            if (OrderState == "已退款" && roms.RoomCount < roms.RoomRemain)
            {

                roms.RoomCount = roms.RoomCount + order.Ordercount;
                if (roms.RoomCount > roms.RoomRemain)
                {
                    roms.RoomCount = roms.RoomRemain;//保证能售房间数量和店长修改的房间数量一样；
                }
                db.SaveChanges();
            }
            or.OrderState = OrderState;
            db.SaveChanges();
            return Content("<script>alert('修改成功');location.href = '/GHotelHome/HotelOrder';</script>");
        }

        /// <summary>
        /// 酒店报表
        /// </summary>
        public ActionResult HotelEchart()
        {


            return View();
        }
        [HttpPost]
        public ActionResult HotelEchart(string select)
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetEchartsPie(string dateday)
        {
            var HotelIDss = int.Parse(Session["HotelID"].ToString());/*Session["HotelID"];*///获取酒店HotelID
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var month = dateday.Split('-')[1];
                var year = dateday.Split('-')[0];
                var day= dateday.Split('-')[2];
               
                var all = db.view_HotelDateHourmoney.Where(v => v.HotelID == HotelIDss && v.datemoneytime.Contains(year + "-" + month+"-"+ day)).Select(s => new { Name = s.datemoneytime, ID = s.summoney }).OrderBy(s => s.Name);

                //声明两个动态数据，存储循环出来的数据
                ArrayList xAxisData = new ArrayList();
                ArrayList yAxisData = new ArrayList();
              
              
                string st2 = null;
                string[] st3 = new string[32];
                decimal[] sum = new decimal[32];
                //循环list将数据循环存储在动态数组中
                foreach (var item in all)
                {
                    var df = item.Name;                    
                    st2 = item.Name.Split(' ')[1];                
                        st3[int.Parse(st2)] = st2;
                        sum[int.Parse(st2)] = (decimal)item.ID;
                 
                                    
                }
                for (int i = 0; i <= 23; i++)
                {
                    string ni = i.ToString();
                    if (i < 10)
                    {
                        ni = "0" + i.ToString();
                    }
                    if (st3[i] != null && st3[i] == ni)
                    {

                        xAxisData.Add(i.ToString()+":00");
                        yAxisData.Add(sum[i]);

                    }
                    else
                    {
                        xAxisData.Add(i.ToString()+":00");
                        yAxisData.Add(0);
                    }

                }
                var results = new { Sex = xAxisData, Num = yAxisData };
                return Json(results, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetEchartsIndex(string dates)
        {
            var HotelIDss = int.Parse(Session["HotelID"].ToString());/*Session["HotelID"];*///获取酒店HotelID
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var month = dates.Split('-')[1];
                var year = dates.Split('-')[0];
                
                int days = DateTime.DaysInMonth(int.Parse(year), int.Parse(month));

               
                var all = db.view_HotelDatedaymoney.Where(v=>v.HotelID==HotelIDss&&v.datemoneytime.Contains(year+"-"+month)).Select(s=>new {Name=s.datemoneytime,ID=s.summoney }).OrderBy(s=>s.Name);
                
                //声明两个动态数据，存储循环出来的数据
                ArrayList xAxisData = new ArrayList();
                ArrayList yAxisData = new ArrayList();
                var b = 1;
                string st1 = null;
                string st2 = null;
                string[] st3 = new string[32];
                decimal[] sum = new decimal[32];
                //循环list将数据循环存储在动态数组中
                foreach (var item in all)
                {  
                     st1 = item.Name.Split('-')[0];
                    
                     st2  = item.Name.Split('-')[1];


                     string bs = item.Name.Split('-')[2];
                    if ( st1 == year && st2 == month) { 
                        st3[int.Parse(bs)] = item.Name.Split('-')[2];
                         sum[int.Parse(bs)] = (decimal)item.ID;
                    }
                    else { 
                        st3[b] = null; 
                    }
                    b++;             
                }
                for (int i = 1; i <= days; i++)
                { 
                    string ni = i.ToString();
                    if (i < 10)
                    {
                        ni = "0" + i.ToString();
                    }
                    if (st3[i]!=null&&st3[i]== ni)
                    {
                       
                        xAxisData.Add(i.ToString());
                        yAxisData.Add(sum[i]);
                     
                    }
                    else
                    {
                        xAxisData.Add(i.ToString());
                        yAxisData.Add(0);
                    }
                  
                }
                var result = new { Sex = xAxisData, Num = yAxisData };
                return Json(result, JsonRequestBehavior.AllowGet);
                 
                //2、不循环添加数据直接数据Json化数据
                //var all = db.Student.GroupBy(s => new { s.StuSex }).Select(s => new { Name = s.Key.StuSex, ID = s.Count() }).ToList();
                //return Json(new { data = all }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        //酒店月销量和房间销量图
        public JsonResult GetEchartsyear(string datesyear)
        {
            var HotelIDss = int.Parse(Session["HotelID"].ToString());/*Session["HotelID"];*///获取酒店HotelID
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var all = db.view_HotelDatemonthmoney.Where(v => v.HotelID == HotelIDss && v.datemoneytime.Contains(datesyear)).Select(s => new { Name = s.datemoneytime, ID = s.summoney }).OrderBy(s => s.Name);

                //声明两个动态数据，存储循环出来的数据
                ArrayList xAxisData = new ArrayList();
                ArrayList yAxisData = new ArrayList();
              
                string st2 = null;
                string[] st3 = new string[32];
                decimal[] sum = new decimal[32];
                //循环list将数据循环存储在动态数组中
                foreach (var item in all)
                {
                   

                    st2 = item.Name.Split('-')[1];
                
                   
                        st3[int.Parse(st2)] = st2;
                        sum[int.Parse(st2)] = (decimal)item.ID;
                    
                                     
                }
                for (int i = 1; i <= 12; i++)
                {
                    string ni = i.ToString();
                    if (i < 10)
                    {
                        ni = "0" + i.ToString();
                    }
                    if (st3[i] != null && st3[i] == ni)
                    {

                        xAxisData.Add(i.ToString());
                        yAxisData.Add(sum[i]);

                    }
                    else
                    {
                        xAxisData.Add(i.ToString());
                        yAxisData.Add(0);
                    }

                }
                var resultsd = new { Sex = xAxisData, Num = yAxisData };
                return Json(resultsd, JsonRequestBehavior.AllowGet);

                //2、不循环添加数据直接数据Json化数据
                //var all = db.Student.GroupBy(s => new { s.StuSex }).Select(s => new { Name = s.Key.StuSex, ID = s.Count() }).ToList();
                //return Json(new { data = all }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}