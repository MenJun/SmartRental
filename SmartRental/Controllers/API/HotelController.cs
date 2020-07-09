using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using Aliyun.Acs.Core.Http;
using SmartRental.BLL.ServiceAPI;
using SmartRental.Models;
namespace SmartRental.Controllers.API
{
    /// <summary>
    /// 前端首页
    /// </summary>
    [EnableCors(headers: "*", methods: "*", origins: "*")]
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

        /// <summary>
        /// 搜索模糊查询酒店
        /// </summary>
        /// <param name="Search">搜索关键字</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult HotelSearch(string Search)
        {

            return Ok(HotelService.SearchHotel(Search));
        }

        [HttpGet]
        public IHttpActionResult HotelselectName(string selectName)
        {
            using (SmartRentalSystemEntities db =new SmartRentalSystemEntities())
            {
                var Hotel = db.HotelManag.Where(t => t.HotelName == selectName).ToList().FirstOrDefault();
                return Ok(Hotel);
            }
            
        }

        [HttpGet]
        public IHttpActionResult select()
        {
            string ip = "118.250.120.180";
            try
            {
                string url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=118.250.120.180" + ip;
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = MyWebClient.DownloadData(url); //从指定网站下载数据
                string stt = Encoding.GetEncoding("GBK").GetString(pageData).Trim();
                return Ok(stt.Substring(stt.Length - 2, 2));
            }
            catch
            {
                 return Ok(404);
            }
          
        }
    }
}
