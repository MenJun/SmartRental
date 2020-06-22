using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aliyun.Acs.Core.Http;
using SmartRental.BLL.ServiceAPI;
using SmartRental.Models;
namespace SmartRental.Controllers.API
{
    /// <summary>
    /// 前端首页
    /// </summary>
    public class HotelController : ApiController
    {
        /// <summary>
        /// 酒店以地址查询
        /// </summary>
        /// <param name="HotelNum">显示数量的个数</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult PopularHotel(int HotelNum)
        {

            return Ok(HotelService.GetHotelsAll(HotelNum));
        }


        /// <summary>
        /// 酒店以推荐查询
        /// </summary>
        /// <param name="HotelNum">显示数量的个数</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult PopularDate(int HotelNum)
        {
           
            return Ok(HotelService.GetHotelsrecommend(HotelNum));
        }


    }
}
