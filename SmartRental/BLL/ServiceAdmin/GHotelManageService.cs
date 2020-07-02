using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRental.BLL.ServiceAdmin
{
    public class GHotelManageService
    {
        /// <summary>
        /// 添加酒店信息
        /// </summary>
        /// <param name="manag"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public static bool AddHotelManager(HotelManag manag, string[] photo)
        {

            return DAL.MapperAdmin.GHotelManagerMan.AddHotelManager(manag, photo) > 0;
        }
       /// <summary>
       /// 添加酒店房间信息
       /// </summary>
       /// <returns></returns>
        public static List<Mattres> GetAll1()
        {
            return DAL.MapperAdmin.GHotelManagerMan.Select1();
        }
        public static List<RoomType> GetAll2()
        {
            return DAL.MapperAdmin.GHotelManagerMan.Select2();
        }
        public static List<HotelManag> GetAll3()
        {
            return DAL.MapperAdmin.GHotelManagerMan.Select3();
        }

        public static List<RoomMessage> HotelRoom()
        {
            return DAL.MapperAdmin.GHotelManagerMan.HotelRoom();
        }

        public static List<Order> GetStudentByPaging(int pageindex, int pagesize, out int pagecount, int HotelID)
        {
            return DAL.MapperAdmin.GHotelManagerMan.Select(pageindex, pagesize, out pagecount, HotelID);
        }
        public static List<Order> GetStudentByPaging1(int pageindex, int pagesize, out int pagecount, string a, string b, int HotelID)
        {
            return DAL.MapperAdmin.GHotelManagerMan.Select1(pageindex, pagesize, out pagecount, a, b,  HotelID);
        }

    }
}