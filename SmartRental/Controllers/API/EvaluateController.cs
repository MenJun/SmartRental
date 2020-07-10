using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using SmartRental.Models;
namespace SmartRental.Controllers.API
{
    [EnableCors(headers: "*", methods: "*", origins: "*")]
    public class EvaluateController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetEvaluate()
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            { 
               return  Ok(db.UserEvaluate.Include("UserMessage").ToList());
            }
           
        }

        /// <summary>
        /// 添加预览记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Evaluate"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult Evaluate(int  userId, int Evaluate)
        {
            using (SmartRentalSystemEntities db =new SmartRentalSystemEntities())
            {
                UserBrowse browse = new UserBrowse();
                browse.HotelID = Evaluate;
                browse.UserID = userId;
                browse.BrowseTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                db.UserBrowse.Add(browse);
                db.SaveChanges();

            }
            return Ok(200);
        }

        [HttpGet]
        public IHttpActionResult setEvaluate(int userId)
        {
            using (SmartRentalSystemEntities db =new SmartRentalSystemEntities())
            {
                var liu = db.View_UserBrHishory.OrderByDescending(s=>s.BrowseID).Where(t => t.UserID == userId).Take(3).ToList();
                db.SaveChanges();
                if (liu.Count == 0)
                {
                    return Ok(0);
                }
                else
                {
                    return Ok(liu);
                }
               
            }
        }
    }
}
