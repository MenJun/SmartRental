using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.Mvc;
using SmartRental.BLL.ServiceAPI;
using SmartRental.Models;

namespace SmartRental.Controllers.API
{
    [EnableCors(headers: "*", methods: "*", origins: "*")]
    public class PersonalCenterController : ApiController
    {
        [System.Web.Http.HttpGet] 
        public IHttpActionResult information(int UserId)
        {
           var user =  PersonalCenterService.getPersonal(UserId);
            return Ok(user);
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult informationUpdata(dynamic data)
        {
            var user = PersonalCenterService.UpdataPersonal(data);
            return Ok(user);
        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult informationOrder(int UserId)
        {
            using (SmartRentalSystemEntities db =new SmartRentalSystemEntities())
            {
                var count = db.Order.Where(t => t.UserID == UserId).Count();
                return Ok(count);
            }
            
        }

        /// <summary>
        /// 所有订单查询
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult Order(int UserId)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
               
                var Order = db.view_OrderHotelPhotos.OrderByDescending(t => t.Ordertime).Where(t => t.UserID == UserId).ToList();
                return Ok(Order);
            }

        }

        /// <summary>
        /// 有效订单查询
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult EffectiveOrder(int UserId)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {

                var aa = DateTime.Now.Date;
                var Order = db.view_OrderHotelPhotos.OrderByDescending(t=>t.Ordertime).Where(t => t.UserID == UserId && t.LeaveTime >= aa && t.OrderState =="已支付").ToList();
                return Ok(Order);
            }

        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult EffectiveOrders()
        {
          
            var files=HttpContext.Current.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                files[i].SaveAs(System.AppDomain.CurrentDomain.BaseDirectory+"/images/"+files[i].FileName);
            }
            return Ok(200);
        }

        /// <summary>
        /// 已退款的订单
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult Orderreimburse(int UserId)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {

                var aa = DateTime.Now.Date;
                var Order = db.view_OrderHotelPhotos.OrderByDescending(t => t.Ordertime).Where(t => t.UserID == UserId && t.OrderState == "已退款").ToList();
                return Ok(Order);
            }

        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult PictureUpload()
        {

            var files = HttpContext.Current.Request.Files[0];
            var Url = System.AppDomain.CurrentDomain.BaseDirectory + "/images/" + files.FileName;
            files.SaveAs(Url);
            return Ok(200);
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult message(dynamic data)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var Url ="/images/" + data.data.imgURL;
                var id = data.data.id;
                var user = db.UserMessage.Find(id.Value);
                user.HeadPhoto = Url;
                db.Entry(user).State = EntityState.Modified;
                return Ok(db.SaveChanges() > 0);
            }
        }
    }
}
