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
            HotelManag hotelManag = db.HotelManag.Where(t => t.HotelID == hotel.HotelID).FirstOrDefault();
            hotelManag.HotelRatify = hotel.HotelRatify;
            db.Entry(hotelManag).State = EntityState.Modified;
            return  db.SaveChanges() > 0;
        }
    }
}