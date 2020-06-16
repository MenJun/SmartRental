using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRental.DAL.MapperAPI;

namespace SmartRental.BLL.ServiceAPI
{
    public class UserLoginService
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal static bool registration(UserMessage user)
        {
          return UserLoginMapper.Registr(user);
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userPhone"></param>
        /// <returns></returns>
        internal static UserMessage QueryAccount(string userPhone)
        {
            return UserLoginMapper.getAccount(userPhone);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal static bool UpdataPwd(UserMessage user)
        {
            return UserLoginMapper.Updata(user);
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userPhone"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        internal static UserMessage getLogin(string userPhone, string userPwd)
        {
           var  user = UserLoginMapper.getAccount(userPhone);
            if (user.UserPwd == userPwd)
            {
                if (user.UserGrade =="用户")
                {
                    return user;
                }
            }
            return null;
        }
    }
    
}