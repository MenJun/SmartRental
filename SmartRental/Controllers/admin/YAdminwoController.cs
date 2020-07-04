using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartRental.Models;
namespace SmartRental.Controllers.admin
{
    public class YAdminwoController : Controller
    {
       
        public ActionResult AdminOrder(int pageindex = 1, int pagesize = 5)
        {
          
            Session["order"] = null;
            int countss = 5;
            var student = BLL.ServiceAdmin.YAdminManageServise.GetStudentByPaging(pageindex, pagesize, out int pagecount);
            ViewBag.pageindex = pageindex;
            ViewBag.pagecount = pagecount;
            ViewBag.pagesize = pagesize;
            if (pagesize % 5 == 0)
            {
                ViewBag.countss = pagesize;
            }
            else
            {
                ViewBag.countss = countss;
            }
            //db.Order.Include(t => t.HotelManag).Include(p => p.RoomMessage).ToList()
            return View(student);

        }
        [HttpPost]
        public ActionResult AdminOrder(int? OrderID, string OrderState, string ab, string text1, int pageindex = 1, int pagesize = 8)
        {
          
            SmartRentalSystemEntities db = new SmartRentalSystemEntities();
            var sss = ab; var bbb = text1;
            if (sss == null && bbb == null)
            {
                Order order = db.Order.Where(t => t.OrderID == OrderID).FirstOrDefault();
                order.OrderState = OrderState;
                var or = db.Order.Find(OrderID);
                or.OrderState = OrderState;
                db.SaveChanges();
            }
            else if (sss == "所有订单")
            {
                return RedirectToAction("HotelOrder");
            }
            else
            {
                int countss = 5;
                var student = BLL.ServiceAdmin.YAdminManageServise.GetStudentByPaging1(pageindex, pagesize, out int pagecount, sss, bbb);
                //Session["order"] = 1;
                ViewBag.pageindex = pageindex;
                ViewBag.pagecount = pagecount;
                ViewBag.pagesize = pagesize;
                if (pagesize % 5 == 0)
                {
                    ViewBag.countss = pagesize;
                }
                else
                {
                    ViewBag.countss = countss;
                }
                return View(student);

            }
            return RedirectToAction("HotelOrder");
        }

    }
}