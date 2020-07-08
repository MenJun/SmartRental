using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartRental.Models;
namespace SmartRental.Controllers.admin
{
    public class YAdminwoController : Controller
    {

        //管理员订单界面
        public ActionResult AdminOrder(int pageindex = 1, int pagesize = 8)
        {
            int pagecount = 1;
            var student  = BLL.ServiceAdmin.YAdminManageServise.GetStudentByPaging(pageindex, pagesize, out  pagecount);
            if ((Session["order"] != null && Session["order"].ToString() != "" && Session["se"]!= "所有订单"&& Session["se"]!=null))
            {
                Session["order"] = 45;
                student = BLL.ServiceAdmin.YAdminManageServise.GetStudentByPaging1(pageindex, pagesize, out  pagecount, Session["se"].ToString(), Session["ts"].ToString()); 
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
        public ActionResult AdminOrder(int? OrderID, string OrderState, string ab, string text1, int pageindex = 1, int pagesize = 8)
        {

            Session["order"] = 45;
         
            SmartRentalSystemEntities db = new SmartRentalSystemEntities();
            var sss = ab; var bbb = text1;
            if (ab != null && ab != "")
            {
                Session["se"] = ab; Session["ts"] = text1;
            }
            if (sss == null && bbb == null)
            {
                var order = db.Order.Include("RoomMessage").Where(t => t.OrderID == OrderID).FirstOrDefault();
                order.OrderState = OrderState;
                var or = db.Order.Find(OrderID); var roms = db.RoomMessage.Find(order.RoomID);
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
            }
            else if (sss == "所有订单")
            {
                return RedirectToAction("AdminOrder");
            }
            else
            {
                //int countss = 5;
                var student = BLL.ServiceAdmin.YAdminManageServise.GetStudentByPaging1(pageindex, pagesize, out int pagecount, sss, bbb);
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
            return RedirectToAction("AdminOrder");
        }






        /// <summary>
        /// 酒店报表
        /// </summary>
        public ActionResult AdminwoEchart()
        {


            return View();
        }

        public ActionResult AdminwoEchart2()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetEchartsPie(string dateday)
        {

            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var month = dateday.Split('-')[1];
                var year = dateday.Split('-')[0];
                var day = dateday.Split('-')[2];

                var all = db.view_HotelDateHourmoney.Where(v => v.datemoneytime.Contains(year + "-" + month + "-" + day)).Select(s => new { Name = s.datemoneytime, ID = s.summoney }).OrderBy(s => s.Name);

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

                        xAxisData.Add(i.ToString() + ":00");
                        yAxisData.Add(sum[i]);

                    }
                    else
                    {
                        xAxisData.Add(i.ToString() + ":00");
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

            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var month = dates.Split('-')[1];
                var year = dates.Split('-')[0];

                int days = DateTime.DaysInMonth(int.Parse(year), int.Parse(month));


                var all = db.view_HotelDatedaymoney.Where(v => v.datemoneytime.Contains(year + "-" + month)).Select(s => new { Name = s.datemoneytime, ID = s.summoney }).OrderBy(s => s.Name);

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

                    st2 = item.Name.Split('-')[1];


                    string bs = item.Name.Split('-')[2];
                    if (st1 == year && st2 == month)
                    {
                        st3[int.Parse(bs)] = item.Name.Split('-')[2];
                        sum[int.Parse(bs)] = (decimal)item.ID;
                    }
                    else
                    {
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

            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var all = db.view_HotelDatemonthmoney.Where(v => v.datemoneytime.Contains(datesyear)).Select(s => new { Name = s.datemoneytime, ID = s.summoney }).OrderBy(s => s.Name);

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

        [HttpPost]
        //酒店月销量和房间销量图
        public JsonResult GetEchartsHotelType()
        {

            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var all = db.HotelManag.Where(v => v.HotelRatify == true).GroupBy(r => r.HotelType).Select(s => new { Name = s.Key, ID = s.Count() });

                //声明两个动态数据，存储循环出来的数据
                ArrayList xAxisData = new ArrayList();
                ArrayList yAxisData = new ArrayList();


                //循环list将数据循环存储在动态数组中
                foreach (var item in all)
                {

                    xAxisData.Add(item.Name);
                    yAxisData.Add(item.ID);
                }

                var resultPic = new { Sex = xAxisData, Num = yAxisData };
                return Json(resultPic, JsonRequestBehavior.AllowGet);

                //2、不循环添加数据直接数据Json化数据
                //var all = db.Student.GroupBy(s => new { s.StuSex }).Select(s => new { Name = s.Key.StuSex, ID = s.Count() }).ToList();
                //return Json(new { data = all }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetEchartsHotelTypeOrder()
        {

            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var all = db.Order.Include("HotelManag").GroupBy(r => r.HotelManag.HotelType).Select(s => new { Name = s.Key, ID = s.Count() });

                //声明两个动态数据，存储循环出来的数据
                ArrayList xAxisData = new ArrayList();
                ArrayList yAxisData = new ArrayList();


                //循环list将数据循环存储在动态数组中
                foreach (var item in all)
                {

                    xAxisData.Add(item.Name);
                    yAxisData.Add(item.ID);
                }

                var resultPicorder = new { Sex = xAxisData, Num = yAxisData };
                return Json(resultPicorder, JsonRequestBehavior.AllowGet);

                //2、不循环添加数据直接数据Json化数据
                //var all = db.Student.GroupBy(s => new { s.StuSex }).Select(s => new { Name = s.Key.StuSex, ID = s.Count() }).ToList();
                //return Json(new { data = all }, JsonRequestBehavior.AllowGet);
            }
        }

        private SmartRentalSystemEntities db = new SmartRentalSystemEntities();
        public ActionResult Recommend()
        {
            var hotel = db.HotelManag.OrderBy(t => t.Hotelrecommen).ToList();
            return View(hotel);
        }
        public ActionResult RecommendAlter(int Id)
        {
            ViewBag.hh = db.HotelManag.Find(Id).Hotelrecommen;
            ViewBag.kk = db.HotelManag.Find(Id).HotelName;
            //var hotel = db.HotelManag.ToList(Id);
            var hotel = db.HotelManag.ToList();
            return View(hotel);
        }
        [HttpPost]
        public ActionResult RecommendAlter(int? Hotelrecommen, int? OldHotelrecommen, int? HotelID, string HotelName, string HotelNames)
        {
            //Hotelrecommen，OldHotelrecommen需要更新的图像字段，HotelID酒店选择的id，HotelName，HotelNames所选择的酒店名
            if (Hotelrecommen == null && HotelID == null)
            {
                ViewBag.hh = OldHotelrecommen;
                ViewBag.kk = HotelNames;
                var hotels = db.HotelManag.Where(t => t.HotelName.Contains(HotelName)).ToList();
                return View(hotels);
            }
            else
            {
                HotelManag hotelManag = db.HotelManag.Where(t => t.Hotelrecommen == OldHotelrecommen).FirstOrDefault();
                hotelManag.Hotelrecommen = Hotelrecommen;
                db.Entry(hotelManag).State = EntityState.Modified;//标注
                HotelManag hotelManag1 = db.HotelManag.Where(t => t.HotelID == HotelID).FirstOrDefault();
                hotelManag1.Hotelrecommen = OldHotelrecommen;
                db.Entry(hotelManag1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Recommend");
            }
        }

        public ActionResult Permission()
        {
            var usermessage = db.UserMessage.ToList();
            return View(usermessage);
        }
        [HttpPost]
        public ActionResult Permission(int UserID, string UserGrade)
        {
            if (UserGrade == null)
            {
                UserMessage userMessage = db.UserMessage.Where(t => t.UserID == UserID).FirstOrDefault();
                if (userMessage.User_status == true)
                {
                    userMessage.User_status = false;
                }
                else
                {
                    userMessage.User_status = true;
                }
                var user=db.UserMessage.Find(UserID);
                user.User_status = userMessage.User_status;
               
                db.SaveChanges();
                return RedirectToAction("Permission");
            }
            else
            {
                UserMessage userMessage = db.UserMessage.Where(t => t.UserID == UserID).FirstOrDefault();
                userMessage.UserGrade = UserGrade;
                var user = db.UserMessage.Find(UserID);
                user.UserGrade = userMessage.UserGrade;
                db.SaveChanges();
                return RedirectToAction("Permission");
            }
        }
    }
}