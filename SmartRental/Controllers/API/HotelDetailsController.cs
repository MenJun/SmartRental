using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using SmartRental.BLL.ServiceAPI;

namespace SmartRental.Controllers.API
{
    [EnableCors(headers: "*", methods: "*", origins: "*")]
    public class HotelDetailsController : ApiController
    {
        /// <summary>
        /// 酒店下面的所有房间信息
        /// </summary>
        /// <param name="HotelId">酒店ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Details(int HotelId)
        {
           
                return Ok(HotelDetailsService.Details(HotelId));
        }

        /// <summary>
        /// 查询某个酒店
        /// </summary>
        /// <param name="HotelId">酒店ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult allhotel(int HotelId)
        {

            return Ok(HotelDetailsService.Allhotel(HotelId));
        }

        /// <summary>
        /// 单个房间信息
        /// </summary>
        /// <param name="RoomId">房间ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Room(int RoomId)
        {

            return Ok(HotelDetailsService.AllRoom(RoomId));
        }
    }
}
