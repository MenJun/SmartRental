using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRental.DAL.MapperAdmin;
using SmartRental.Models;

namespace SmartRental.BLL.ServiceAdmin
{
    public class HotelService
    {
        public static List<HotelManag> Hotel()
        {
          return  HotelMapper.GetHotel();
        }

        internal static bool SetHotel(HotelManag ratify)
        {

            if (HotelMapper.UpdataAudit(ratify))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}