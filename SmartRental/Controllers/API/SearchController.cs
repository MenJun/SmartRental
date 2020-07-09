using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using SmartRental.Helpers;
using SmartRental.Models;

namespace SmartRental.Controllers.API
{
    [EnableCors(headers: "*", methods: "*", origins: "*")]
    public class SearchController : ApiController
    {
        /// <summary>
        /// 查询地方酒店结果
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult resultSearch(string name)
        {
            using (SmartRentalSystemEntities db =new SmartRentalSystemEntities())
            {
                var Hotelname = db.HotelManag.Include("HotelPhoto").Where(t => t.HotelCity.Contains(name) && t.HotelRatify ==true).ToList();
                return Ok(Hotelname);
            }
           
        }

      
    }
}
