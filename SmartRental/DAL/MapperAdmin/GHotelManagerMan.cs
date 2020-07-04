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
                db.HotelManag.Add(manager);
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
                return orders.OrderBy(s => s.OrderID).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
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
                    return students.OrderBy(s => s.OrderID).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }
                else if (a == "订单状态")
                {
                    var students = orders.Where(t => t.OrderState.Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderBy(s => s.OrderID).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }
                else if (a == "房间")
                {
                    var students = orders.Where(t => t.RoomMessage.RoomName.Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderBy(s => s.OrderID).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }
               
                else if (a == "入住日期")
                {
                    var students = orders.Where(t => t.ArrivalDate.ToString().Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderBy(s => s.OrderID).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }
                else if (a == "电话")
                {
                    var students = orders.Where(t => t.ClientPhone.Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderBy(s => s.OrderID).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }
                else if (a == "房间")
                {
                    var students = orders.Where(t => t.RoomMessage.RoomType.RoomType1.Contains(b)).ToList();
                    pagecount = (int)Math.Ceiling(students.Count() * 1.0 / pagesize);
                    return students.OrderBy(s => s.OrderID).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
                }

                pagecount = (int)Math.Ceiling(orders.Count() * 1.0 / pagesize);
                return orders.OrderBy(s => s.OrderID).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList();
            }
        }

       
            
        
    }
}