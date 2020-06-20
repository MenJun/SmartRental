using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aliyun.Acs.Core.Http;
using SmartRental.Models;
namespace SmartRental.Controllers.API
{
    public class HotelController : ApiController
    {
        SmartRentalSystemEntities db = new SmartRentalSystemEntities();
        [HttpGet]
        public IHttpActionResult PopularHotel(int HotelNum)
        {
            //以地址推荐的前4个酒店 
            ArrayList list = new ArrayList();
            int num = 0;
            db.Configuration.ProxyCreationEnabled = false;
            var dbApplicationList = db.HotelManag.GroupBy(x => x.HotelCity).Select(x=>new {key = x.Key });
            
            foreach (var item in dbApplicationList)
            {
               var   _dbApplicationList = item.key;
               var distinctPeople = db.HotelManag.Include("HotelPhoto").Where(t => t.HotelCity == _dbApplicationList && t.HotelRatify ==true)
                    .GroupBy(p => new { p.HotelCity, p.HotelName }).Select(g => g.FirstOrDefault()).ToList();
                list.Add(distinctPeople.Take(1));
                if (num >= HotelNum)
                {
                    break;
                }
              
            }
            
            return Ok(list);
        }
        //public IHttpActionResult PopularDate(int HotelNum)
        //{
        //    var tui = db.HotelManag.Where(s => s.Hotelrecommen > 0 && s.Hotelrecommen != null).OrderBy(s=>s.Hotelrecommen);
        //    return Ok(200);
        //}
    }
}
