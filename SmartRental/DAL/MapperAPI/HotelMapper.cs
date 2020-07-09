using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRental.Models;

namespace SmartRental.DAL.MapperAPI
{
    public class HotelMapper
    {
        /// <summary>
        /// 查询上线后以地址随机推荐的酒店
        /// </summary>
        /// <param name="HotelNum"></param>
        /// <returns></returns>
        public static  List<HotelManag> GetALL(int HotelNum)
        {
            List<HotelManag> list = new List<HotelManag>();
            //ArrayList list = new ArrayList();
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
               
                int num = 0;
                // db.Configuration.ProxyCreationEnabled = false;
                var dbApplicationList = db.HotelManag.GroupBy(x => x.HotelCity).Select(x => new { key = x.Key }).ToList();
             
                foreach (var item in dbApplicationList)
                {
                    var _dbApplicationList = item.key;


                    var distinctPeople = db.HotelManag.Include("HotelPhoto")
                        .Where(t => t.HotelCity == _dbApplicationList && t.HotelRatify == true).ToList().FirstOrDefault();
                    if (distinctPeople == null)
                    {
                        continue;
                    }
                    list.Add(distinctPeople);
                    if (num >= HotelNum)
                    {
                        break;
                    }
                    num++;
                }
            }
            return list;
        }

        /// <summary>
        /// 搜索栏模糊查询
        /// </summary>
        /// <param name="search"></param>
        internal static object Hunt(string search)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var aa = db.HotelManag.Where(t => t.HotelName.Contains(search) && t.HotelRatify == true).ToList();
                return aa;
            }
        }

        /// <summary>
        /// 查询上线后推荐的的酒店
        /// </summary>
        /// <returns></returns>
        public static List<HotelManag> Getrecommend()
        {
            
            
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var aa = db.HotelManag.Include("HotelPhoto").Where(t => t.HotelRatify == true).OrderBy(t => t.Hotelrecommen).ToList();
                return aa;
            }
        }
    }
}
