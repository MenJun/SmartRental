using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRental.DAL.MapperAPI;
using SmartRental.Models;

namespace SmartRental.BLL.ServiceAPI
{
    public class HotelDetailsService
    {
        public static List<RoomMessage> Details(int id)
        {
            return HotelDetailsMapper.GllDetails(id);
        }

        /// <summary>
        /// 某个酒店查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static HotelManag Allhotel(int id)
        {
            return HotelDetailsMapper.All(id);
        }
    }
}