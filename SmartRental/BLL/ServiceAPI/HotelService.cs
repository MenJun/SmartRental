using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRental.DAL.MapperAPI;
using SmartRental.Models;

namespace SmartRental.BLL.ServiceAPI
{
    public class HotelService
    {
        /// <summary>
        /// 地址筛选前4个
        /// </summary>
        /// <param name="HotelNum"></param>
        /// <returns></returns>
        public static List<HotelManag> GetHotelsAll(int HotelNum)
        {
            return HotelMapper.GetALL(HotelNum);
        }

        /// <summary>
        /// 筛选前7个
        /// </summary>
        /// <param name="HotelNum">数量</param>
        /// <returns></returns>
        public static List<HotelManag> GetHotelsrecommend(int HotelNum)
        {
            int i = 0;
            List<HotelManag> list = new List<HotelManag>();
            var Hotrl = HotelMapper.Getrecommend();
            foreach (var item in Hotrl)
            {
               
                if (HotelNum > i)
                {
                    list.Add(item);
                    i++;
                    continue;
                }
                break;
            }
            return list;
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        internal static object SearchHotel(string search)
        {
           return HotelMapper.Hunt(search);
        }
    }
}