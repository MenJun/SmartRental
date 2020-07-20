using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRental.DAL.MapperAdmin
{
    public class GHotelManagerMan
    {
        /// <summary>
        /// 添加酒店信息到酒店信息表和酒店图片表
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public static int AddHotelManager(HotelManag manager, string[] photo)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                HotelPhoto hotel = new HotelPhoto();
                for (int i = 0; i < photo.Length; i++)
                {

                    if (i == 0)
                        hotel.Hotelphoto1 = photo[i].ToString();
                    if (i == 1)
                        hotel.Hotelphoto2 = photo[i].ToString();
                    if (i == 2)
                        hotel.Hotelphoto3 = photo[i].ToString();
                    if (i == 3)
                        hotel.Hotelphoto4 = photo[i].ToString();
                    if (i == 4)
                        hotel.Hotelphoto5 = photo[i].ToString();
                    if (i == 5)
                        hotel.Hotelphoto6 = photo[i].ToString();
                    if (i == 6)
                        hotel.Hotelphoto7 = photo[i].ToString();

                }
              var bb=db.HotelPhoto.Add(hotel);
                db.SaveChanges();
                int photoID = bb.HotelPhotoID;
                manager.HotelPhotoID = photoID;
                manager.Hotelrecommen = 10000;
                db.HotelManag.Add(manager);
                return db.SaveChanges();
            }
        }


        public static int AddHotelManager(HotelManag manager, string[] photo,UserMessage usms)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                HotelPhoto hotel = new HotelPhoto();
                for (int i = 0; i < photo.Length; i++)
                {

                    if (i == 0)
                        hotel.Hotelphoto1 = photo[i].ToString();
                    if (i == 1)
                        hotel.Hotelphoto2 = photo[i].ToString();
                    if (i == 2)
                        hotel.Hotelphoto3 = photo[i].ToString();
                    if (i == 3)
                        hotel.Hotelphoto4 = photo[i].ToString();
                    if (i == 4)
                        hotel.Hotelphoto5 = photo[i].ToString();
                    if (i == 5)
                        hotel.Hotelphoto6 = photo[i].ToString();
                    if (i == 6)
                        hotel.Hotelphoto7 = photo[i].ToString();

                }
                var bb = db.HotelPhoto.Add(hotel);
                db.SaveChanges();
                int photoID = bb.HotelPhotoID;
                manager.HotelPhotoID = photoID;
                manager.Hotelrecommen = 10000;
                db.SaveChanges();
                var hh=db.HotelManag.Add(manager);
                var usermess = db.UserMessage.Find(usms.UserID);
                usermess.HotelID = hh.HotelID;

                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 查询所有的床型
        /// </summary>
        /// <returns></returns>
        public static List<Mattres> Select1()
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                return db.Mattres.ToList();
            }
        }

        /// <summary>
        /// 查询所有的房型
        /// </summary>
        /// <returns></returns>
        public static List<RoomType> Select2()
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                return db.RoomType.ToList();
            }
        }
        /// <summary>
        /// 查询所有的酒店
        /// </summary>
        /// <returns></returns>
        public static List<HotelManag> Select3()
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                return db.HotelManag.ToList();
            }
        }
        /// <summary>
        /// 查询酒店的所有房间
        /// </summary>
        /// <returns></returns>
        public static List<RoomMessage> HotelRoom()
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                return db.RoomMessage.ToList();
            }
        }



        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">一页的数量</param>
        /// <returns></returns>
        public static List<Order> Select(int pageindex, int pagesize, out int pagecount,int HotelID)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var orders = db.Order.Include("HotelManag").Include("RoomMessage").Where(s => s.HotelID == HotelID) ;

                pagecount = (int)Math.Ceiling(orders.Count() * 1.0 / pagesize);
                return orders.OrderByDescending(s => s.ArrivalDate).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
            }
        }
        public static List<Order> Select1(int pageindex, int pagesize, out int pagecount, string a, string b, int HotelID)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var orders = db.Order.Include("HotelManag").Include("RoomMessage").Where(s => s.HotelID == HotelID);

                
                if (a == "订单编号")
                {
                    var students = orders.Where(t => t.OrderNumber.ToString().Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }
                else if (a == "订单状态")
                {
                    var students = orders.Where(t => t.OrderState.Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
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
                    return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }
                else if (a == "房间名")
                {
               
                    var students = orders.Where(t => t.RoomMessage.RoomName.Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                 
                    return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }

                pagecount = (int)Math.Ceiling(orders.Count() * 1.0 / pagesize);//获取总数量
                return orders.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();//分页数据
            }
        }



        public static List<Order> Selectorderdate(int pageindex, int pagesize, out int pagecount, string b, int HotelID)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var orders = db.Order.Include("HotelManag").Include("RoomMessage").Where(s => s.HotelID == HotelID);
                    var df = DateTime.Now.Date;

                if (b=="今日订单")
                {
                   
                    DateTime bd = DateTime.Parse( df.ToString());
                    var students = orders.Where(s => s.Ordertime> bd).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }
                else if (b == "今日到店")
                {
                
                    var students = orders.Where(t => t.ArrivalDate == df ).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }

                
                else if (b == "待办事项")
                {
                    var students = orders.Where(t => t.OrderState=="退款").ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderByDescending(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }
              

                pagecount = (int)Math.Ceiling(orders.Count() * 1.0 / pagesize);//获取总数量
                return orders.OrderBy(s => s.Ordertime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();//分页数据
            }
        }
    }
}