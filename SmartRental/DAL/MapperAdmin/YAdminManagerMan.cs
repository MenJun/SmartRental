using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRental.DAL.MapperAdmin
{
    public class YAdminManagerMan
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">一页的数量</param>
        /// <returns></returns>
        public static List<Order> Select(int pageindex, int pagesize, out int pagecount)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var orders = db.Order.Include("HotelManag").Include("RoomMessage").Include("UserMessage");

                pagecount = (int)Math.Ceiling(orders.Count() * 1.0 / pagesize);
                return orders.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
            }
        }
        public static List<Order> Select1(int pageindex, int pagesize, out int pagecount, string a, string b)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var orders = db.Order.Include("HotelManag").Include("RoomMessage").Include("UserMessage");


                if (a == "订单编号")
                {
                    var students = orders.Where(t => t.OrderNumber.ToString().Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderByDescending(s => s.ArrivalDate).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }
                else if (a == "订单状态")
                {
                    var students = orders.Where(t => t.OrderState.Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderByDescending(s => s.ArrivalDate).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }

                //else if (a == "入住日期")
                //{
                //    var ti= (DateTime)b.t;
                //    var students = orders.Where(t => t.ArrivalDate>=(DateTime)(b)).ToList();
                //    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                //    return students.OrderByDescending(s => s.ArrivalDate).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                //}
                else if (a == "入住电话")
                {
                    var students = orders.Where(t => t.ClientPhone.Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderByDescending(s => s.ArrivalDate).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }
                else if (a == "房间名")
                {

                    var students = orders.Where(t => t.RoomMessage.RoomName.Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);

                    return students.OrderByDescending(s => s.ArrivalDate).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }

                pagecount = (int)Math.Ceiling(orders.Count() * 1.0 / pagesize);//获取总数量
                return orders.OrderBy(s => s.OrderID).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();//分页数据
            }
            //using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            //{
            //    var orders = db.Order.Include("HotelManag").Include("RoomMessage").Include("UserMessage");


            //    if (a == "订单编号")
            //    {
            //        var students = orders.Where(t => t.OrderNumber.ToString().Contains(b)).ToList();
            //        pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
            //        return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
            //    }
            //    else if (a == "订单状态")
            //    {
            //        var students = orders.Where(t => t.OrderState.Contains(b)).ToList();
            //        pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
            //        return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
            //    }
            //    else if (a == "酒店")
            //    {
            //        var students = orders.Where(t => t.HotelManag.HotelName.Contains(b)).ToList();
            //        pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
            //        return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
            //    }

            //    //else if (a == "入住日期")
            //    //{
            //    //    var students = orders.Where(t => t.ArrivalDate.ToString().Contains(b)).ToList();
            //    //    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
            //    //    return students.OrderBy(s => s.OrderID).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
            //    //}
            //    else if (a == "入住电话")
            //    {
            //        var students = orders.Where(t => t.ClientPhone.Contains(b)).ToList();
            //        pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
            //        return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
            //    }
            //    else if (a == "房间名")
            //    {
            //        var students = orders.Where(t => t.RoomMessage.RoomType.RoomType1.Contains(b)).ToList();
            //        pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
            //        return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
            //    }

            //    pagecount = (int)Math.Ceiling(orders.Count() * 1.0 / pagesize);
            //    return orders.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
        }
    }
}