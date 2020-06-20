using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
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
        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="UserPhone">号码</param>
        /// <returns></returns>
        [HttpGet]
        public  IHttpActionResult Code(string PhoneNum)
        {
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", "LTAI4GKmekoxPjwRHHFtqsT1", "nvWCJdN8VvjC5TjPXiXIxqHTHVVGXA");
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            // request.Protocol = ProtocolType.HTTP;
            request.AddQueryParameters("PhoneNumbers", PhoneNum);
            request.AddQueryParameters("SignName", "美食美客网");
            request.AddQueryParameters("TemplateCode", "SMS_193242675");
            string vc = "";
            Random rNum = new Random();//随机生成类
            int num1 = rNum.Next(0, 9);//返回指定范围内的随机数
            int num2 = rNum.Next(0, 9);
            int num3 = rNum.Next(0, 9);
            int num4 = rNum.Next(0, 9);
            int num5 = rNum.Next(0, 9);
            int num6 = rNum.Next(0, 9);
            int[] nums = new int[6] { num1, num2, num3, num4, num5, num6 };
            for (int i = 0; i < nums.Length; i++)//循环添加四个随机生成数
            {
                vc += nums[i].ToString();
            }
            request.AddQueryParameters("TemplateParam", "{\"code\":\"" + vc + "\"}");
            try
            {
                CommonResponse response = client.GetCommonResponse(request);
                return Ok(vc);
            }
            catch (Aliyun.Acs.Core.Exceptions.ServerException e)
            {
                return Ok(e);
            }
            catch (ClientException e)
            {
                return Ok(e);
            }
            
        }
        public string Options()
        {
            return null;
        }
    }
}
