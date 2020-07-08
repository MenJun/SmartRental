using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SmartRental.Models;

namespace SmartRental.DAL.MapperAdmin
{
    public class HotelMapper
    {
        public static SmartRentalSystemEntities db = new SmartRentalSystemEntities();
        /// <summary>
        /// 查询所有酒店
        /// </summary>
        /// <returns></returns>
        public static List<HotelManag> GetHotel()
        {
            return db.HotelManag.ToList();
        }
         public static bool UpdataAudit(HotelManag hotel)
        {
     
           var re= db.HotelManag.Find(hotel.HotelID);
            re.HotelRatify = hotel.HotelRatify;
         
            return  db.SaveChanges() > 0;
        }
    }
}