using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRental.BLL.ServiceAdmin
{
    public class GHotelManageService
    {
        public static bool AddHotelManager(HotelManag manag, string[] photo)
        {

            return DAL.MapperAdmin.GHotelManagerMan.AddHotelManager(manag, photo) > 0;
        }
    }
}