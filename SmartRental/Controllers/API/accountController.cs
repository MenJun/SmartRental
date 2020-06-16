using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using SmartRental.BLL.ServiceAPI;
using SmartRental.Models;

namespace SmartRental.Controllers.API
{

    [EnableCors(headers: "http://localhost:8081", methods: "*", origins: "*")]
    public class accountController : ApiController
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="UserPhone">账号</param>
        /// <param name="UserPwd">密码</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult index(string UserPhone, string UserPwd)
        {
            
            UserMessage user = new UserMessage();
            user.UserPhone = UserPhone;
            user.UserPwd = UserPwd;
            user.User_status = true;
            user.UserGrade = "用户";
            user.Last_landing_time = System.DateTime.Now ;
            user.Registration_time = System.DateTime.Now;
          //  UserLoginService.registration(user); 
            if (UserLoginService.registration(user))//注册
            {
                return Ok(new { Code = "200", SUCCESS = "成功" });
            }
            return Ok(new { Code = "404", SUCCESS = "失败"});
        }

        /// <summary>
        /// 用户信息查询判断是否存在
        /// </summary>
        /// <param name="UserPhone">号码</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetUser(string UserPhone)
        {
            var user = UserLoginService.QueryAccount(UserPhone);
            if (user != null)
            {
                return Ok(new { Code = "200", SUCCESS = "成功", details = user });
            }
            return Ok(new { Code = "404", SUCCESS = "失败", details = user });
        }

        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="UserPhone">账号</param>
        /// <param name="UserPwd">修改的密码</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult ChangePwd(string UserPhone,string UserPwd)
        {
            UserMessage user = new UserMessage();
            user.UserPhone = UserPhone;
            user.UserPwd = UserPwd;
            if (UserLoginService.UpdataPwd(user))
            {
                return Ok(new { Code = "200", SUCCESS = "成功"});
            }
            return Ok(new { Code = "404", SUCCESS = "失败"});
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="UserPhone"></param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public object Landing(string UserPhone, string UserPwd)
        {
            var user = UserLoginService.getLogin(UserPhone, UserPwd);
            if (user != null)
            {
                return Ok(new { Code = "200", SUCCESS = "成功", details = user });
            }
            return Ok(new { Code = "404", SUCCESS = "失败", details = user });
        }
        public string Options()
        {
            return null;
        }
    }
}
