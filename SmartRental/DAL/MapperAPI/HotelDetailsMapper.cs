using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRental.Models;

namespace SmartRental.DAL.MapperAPI
{
    public class HotelDetailsMapper
    {
        /// <summary>
        /// 所有房间信息
        /// </summary>
        /// <param name="id">酒店Id</param>
        /// <returns></returns>
        public static List<RoomMessage> GllDetails(int id)
        {
            using (SmartRentalSystemEntities db =new SmartRentalSystemEntities())
            {
              
              var aa= db.RoomMessage.Include("RoomType").Include("RoomPhoto").Where(t => t.HotelID == id &&t.Roomstate!=false).ToList();
                return aa;
            }
            
        }

        internal static object Allroom(int roomId)
        {
            using(SmartRentalSystemEntities db =new SmartRentalSystemEntities())
            {
                var aa = db.RoomMessage.Include("RoomType").Include("RoomPhoto").Where(t => t.RoomID == roomId).ToList().FirstOrDefault();
                return aa;
            }
        }

        /// <summary>
        /// 查询某个酒店
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static HotelManag All(int id)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {

                var aa = db.HotelManag.Include("HotelPhoto").Where(t => t.HotelID == id).FirstOrDefault();
                return aa;
            }

        }
    }
}