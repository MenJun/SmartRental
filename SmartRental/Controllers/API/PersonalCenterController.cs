using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using SmartRental.BLL.ServiceAPI;
using SmartRental.Models;

namespace SmartRental.Controllers.API
{
    [EnableCors(headers: "*", methods: "*", origins: "*")]
    public class PersonalCenterController : ApiController
    {
        [HttpGet] 
        public IHttpActionResult information(int UserId)
        {
           var user =  PersonalCenterService.getPersonal(UserId);
            return Ok(user);
        }
        [HttpPost]
        public IHttpActionResult informationUpdata(dynamic data)
        {
            var user = PersonalCenterService.UpdataPersonal(data);
            return Ok(user);
        }
        [HttpGet]
        public IHttpActionResult informationOrder(int UserId)
        {
            using (SmartRentalSystemEntities db =new SmartRentalSystemEntities())
            {
                var count = db.Order.Where(t => t.UserID == UserId).Count();
                return Ok(count);
            }
            
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Order(int UserId)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
               
                var Order = db.Order.Include("HotelManag").Where(t => t.UserID == UserId).ToList();
                return Ok(Order);
            }

        }

    }
}
