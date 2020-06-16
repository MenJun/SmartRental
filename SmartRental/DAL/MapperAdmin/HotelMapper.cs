using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRental.Models;

namespace SmartRental.DAL.MapperAdmin
{
    public class HotelMapper
    {
        public static SmartRentalSystemEntities db = new SmartRentalSystemEntities();
        public static List<HotelManag> GetHotel()
        {
            return db.HotelManag.ToList();
        }
    }
}